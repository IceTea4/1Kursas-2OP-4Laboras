using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using System.ComponentModel;

namespace U4H_24_Pasikartojantys_zdz
{
    /// <summary>
    /// Class that performs scans and prints
    /// </summary>
	public class InOut
	{
        /// <summary>
        /// Method reads from the file and prints the results
        /// </summary>
        /// <param name="fin"></param>
        /// <param name="fout"></param>
        /// <param name="punctuation"></param>
        public static void ProcessFirstTasks(string fin, string fout,
            char[] punctuation, string endOfSentence)
        {
            using (StreamReader reader = new StreamReader(fin, Encoding.UTF8))
            {
                string line;
                int numberOfLine = 0;
                string[] longestSentece = new string[2];

                longestSentece[0] = "";
                longestSentece[1] = "";

                List<Word> mostRepetitive = new List<Word>();

                // Properties array is used instead of a new class
                // In it the results are being saved
                int[] properties = new int[6];

                // Each properties element is set to zero
                SetPropertiesToZero(properties);

                using (var writer = File.CreateText(fout))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Finds words with their repetition
                        TaskUtils.FindMostRepetitive(line, mostRepetitive,
                            punctuation);

                        // Finds the longest sentence
                        TaskUtils.FindLongestSentence(line, punctuation,
                            properties, numberOfLine, endOfSentence,
                            longestSentece);

                        numberOfLine++;
                    }

                    // Sorts repetitive words
                    Sort(mostRepetitive);

                    // Prints repetitive words
                    PrintMostRepetitive(mostRepetitive, writer);

                    writer.WriteLine($"Ilgiausias sakinys:");
                    writer.WriteLine($"{longestSentece[0]}");
                    writer.WriteLine($"Zodziai: {properties[0]}; " +
                        $"Simboliai: {properties[1]}; Sakinio pradzia: " +
                        $"{properties[4]}");
                }
            }
        }

        /// <summary>
        /// Each properties element is set to zero
        /// </summary>
        /// <param name="properties"></param>
        private static void SetPropertiesToZero(int[] properties)
        {
            for (int i = 0; i < 6; i++)
            {
                properties[i] = 0;
            }
        }

        /// <summary>
        /// Sorting
        /// </summary>
        /// <param name="mostRepetitive"></param>
        private static void Sort(List<Word> mostRepetitive)
        {
            bool flag = true;

            while (flag)
            {
                flag = false;

                for (int i = 0; i < mostRepetitive.Count - 1; i++)
                {
                    Word one = mostRepetitive[i];
                    Word two = mostRepetitive[i + 1];

                    if (one.CompareTo(two) < 0)
                    {
                        mostRepetitive[i] = two;
                        mostRepetitive[i + 1] = one;
                        flag = true;
                    }
                }
            }
        }

        /// <summary>
        /// Printing most repetitive first 10 words
        /// </summary>
        /// <param name="mostRepetitive"></param>
        /// <param name="writer"></param>
        private static void PrintMostRepetitive(List<Word> mostRepetitive,
            StreamWriter writer)
        {
            writer.WriteLine("Dazniausiai pasikartojantys zodziai: ");

            for (int i = 0; i < Math.Min(10, mostRepetitive.Count); i++)
            {
                writer.WriteLine(mostRepetitive[i].word + " "
                    + mostRepetitive[i].count);
            }
        }

        /// <summary>
        /// Reads from the file and prints the results
        /// </summary>
        /// <param name="fin"></param>
        /// <param name="frout"></param>
        /// <param name="punct"></param>
        /// <param name="pattern"></param>
        public static void ProcessSecondTask(string fin, string frout,
            string pattern, string punct)
        {
            using (StreamReader reader = new StreamReader(fin, Encoding.UTF8))
            {
                string line;
                Word[] wordsInLine = new Word[40];

                // Each array elemnt is set to empty
                SetArrayToEmpty(wordsInLine);

                using (var writer = File.CreateText(frout))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Removes repetitive punctuation
                        line = Regex.Replace(line, pattern, "$1");

                        // Finds the longest word in the column
                        TaskUtils.FindBiggestWordInColumn(line, wordsInLine,
                            punct);
                    }

                    // Sets the start for the reader to zero so it would read
                    // it again from the start
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);

                    // Prints aligned text
                    PrintAlignedLines(reader, writer, pattern, wordsInLine,
                        punct);
                }
            }
        }

        /// <summary>
        /// Each array elemnt is set to empty
        /// </summary>
        /// <param name="wordsInLine"></param>
        private static void SetArrayToEmpty(Word[] wordsInLine)
        {
            for (int i = 0; i < 40; i++)
            {
                wordsInLine[i] = new Word("");
            }
        }

        /// <summary>
        /// Prints aligned text
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="pattern"></param>
        /// <param name="wordsInLine"></param>
        /// <param name="punct"></param>
        private static void PrintAlignedLines(StreamReader reader,
            StreamWriter writer, string pattern, Word[] wordsInLine,
            string punct)
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (line.Length > 0)
                {
                    // Removes repetitive punctuation
                    line = Regex.Replace(line, pattern, "$1");

                    string[] words = Regex.Split(line, "[" + punct + "]+");

                    // Aligning
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(words[i]))
                        {
                            //Mathces the word in line and adds to
                            //it punctuation
                            Match withPunctuation = Regex.Match(line,
                            words[i] + "([" + punct + "]+)?");

                            writer.Write(withPunctuation);

                            line = line.Substring(withPunctuation.Length);

                            //Adds spaces if needed
                            if (withPunctuation.Length
                                != wordsInLine[i].word.Length)
                            {
                                int temp = wordsInLine[i].word.Length
                                    - withPunctuation.Length + 1;

                                writer.Write(new string(' ', temp));
                            }
                            else
                            {
                                writer.Write(' ');
                            }
                        }
                    }

                    writer.WriteLine();
                }
            }
        }
    }
}
