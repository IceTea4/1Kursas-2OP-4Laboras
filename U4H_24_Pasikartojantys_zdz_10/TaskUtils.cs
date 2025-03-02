using System.Text;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace U4H_24_Pasikartojantys_zdz
{
    /// <summary>
    /// Class in which all the logic is done
    /// </summary>
	public class TaskUtils
	{
        /// <summary>
        /// Finds repetitive words and their frequency
        /// </summary>
        /// <param name="line"></param>
        /// <param name="mostRepetitive"></param>
        /// <param name="punctuation"></param>
		public static void FindMostRepetitive(string line,
            List<Word> mostRepetitive, char[] punctuation)
		{
			if (line.Length > 0)
			{
				string[] words = line.Split(punctuation,
                    StringSplitOptions.RemoveEmptyEntries);

				foreach (string word in words)
				{
                    string wordLower = word.ToLower();

                    Word w = mostRepetitive.Find(x => x.word == wordLower);

                    if (w == null)
                    {
                        w = new Word(wordLower);
                        mostRepetitive.Add(w);
                    }

                    w.Increase();
                }
			}
        }

        /// <summary>
        /// First finds how many there is sentence endings in the line and
        /// gets the longest information from another method
        /// </summary>
        /// <param name="line"></param>
        /// <param name="punctuation"></param>
        /// <param name="properties"></param>
        /// <param name="numberOfLine"></param>
		public static void FindLongestSentence(string line,
            char[] punctuation, int[] properties, int numberOfLine,
            string endOfSentence, string[] longestSentence)
		{
			int x = 0;

			for (int i = 0; i < line.Length; i++)
			{
                if (endOfSentence.Contains(line[i]))
				{
					x++;
				}
			}

            // Finds the longest sentence
			CollectLongestSentences(line, punctuation, properties, x,
                numberOfLine, endOfSentence, longestSentence);
        }

        /// <summary>
        /// Finds the longest sentece
        /// </summary>
        /// <param name="line"></param>
        /// <param name="punctuation"></param>
        /// <param name="properties"></param>
        /// <param name="x"></param>
        /// <param name="numberOfLine"></param>
		private static void CollectLongestSentences(string line,
            char[] punctuation, int[] properties, int x, int numberOfLine,
            string endOfSentence, string[] longestSentence)
		{
            int start = 0;

            for (int i = 0; i < line.Length; i++)
            {
                StringBuilder newLine = new StringBuilder();

                if (endOfSentence.Contains(line[i])
                    && x != 0)
                {
                    newLine.Append(line, start, i - start + 1);

                    int temp = newLine.Length;
                    string[] words = newLine.ToString().Split(punctuation,
                        StringSplitOptions.RemoveEmptyEntries);

					if (properties[0] < words.Length + properties[2]
                        && start == 0)
					{
						properties[0] = words.Length + properties[2];
						properties[1] = temp + properties[3];
                        longestSentence[0] = longestSentence[1] + " "
                            + newLine.ToString();

                        if (properties[2] != 0)
                        {
                            properties[4] = properties[5];
                        }
                        else
                        {
                            properties[4] = numberOfLine + 1;
                        }
                        properties[2] = 0;
                        properties[3] = 0;
                        longestSentence[1] = "";
					}
                    else if (properties[0] < words.Length)
					{
                        properties[0] = words.Length;
                        properties[1] = temp;
                        properties[4] = numberOfLine + 1;
                        longestSentence[0] = newLine.ToString();
                    }

                    start = i + 1;
                    x--;
                }
				else if (x == 0)
				{
                    newLine.Append(line.Substring(start));

                    int temp = newLine.Length;
                    string[] words = newLine.ToString().Split(punctuation,
                        StringSplitOptions.RemoveEmptyEntries);

                    if (properties[2] == 0)
                    {
                        properties[5] = numberOfLine + 1;
                    }

                    if (start == 0)
					{
                        properties[2] += words.Length;
						properties[3] += temp;
                        longestSentence[1] += newLine.ToString();
                    }
					else
					{
                        properties[2] = words.Length;
                        properties[3] = temp;
                        longestSentence[1] = newLine.ToString();
                    }

					break;
                }
            }
        }

        /// <summary>
        /// Finds the longest words in the column
        /// </summary>
        /// <param name="line"></param>
        /// <param name="wordsInLine"></param>
        public static void FindBiggestWordInColumn(string line,
            Word[] wordsInLine, string punct)
        {
            if (line.Length > 0)
            {
                //Splits line into words with no punctuation
                string[] words = Regex.Split(line, "[" + punct + "]+");
                int position = 0;

                for (int i = 0; i < words.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(words[i]))
                    {
                        //Adds punctuation if there was any to the
                        //matched word
                        Match withPunctuation = Regex.Match(line,
                            words[i] + "([" + punct + "]+)?");

                        line = line.Substring(withPunctuation.Length);

                        if (withPunctuation.Length
                            > wordsInLine[i].word.Length)
                        {
                            wordsInLine[i].word =
                                withPunctuation.ToString().TrimEnd(' ');
                        }
                    }
                }
            }
        }
    }
}

