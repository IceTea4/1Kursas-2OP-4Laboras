namespace U4H_24_Pasikartojantys_zdz
{
    /// <summary>
    /// Constructor class
    /// </summary>
	public class Word
	{
        public string word { get; set; }
        public int count { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="word"></param>
        public Word(string word)
		{
            this.word = word;
            this.count = 0;
        }

        /// <summary>
        /// Increases the count
        /// </summary>
        public void Increase()
        {
            count++;
        }

        /// <summary>
        /// Compares first the count than by the alphabet
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Word other)
        {
            int bigger = this.count.CompareTo(other.count);
            if (bigger != 0)
            {
                return bigger;
            }
            else
            {
                return other.word.CompareTo(this.word);
            }
        }
    }
}
