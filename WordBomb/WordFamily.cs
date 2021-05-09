using System;
using System.Collections.Generic;
using System.Linq;

namespace WordBomb
{
    /// <summary>
    /// A family of words with common elements
    /// </summary>
    public class WordFamily
    {
        // List of words in family
        public string[] words;
        // Formatted string containing the families common letters and "_" for wildcards
        private readonly string commonLetters;

        // GETTERS
        public string CommonLetters => commonLetters;        
        /// <summary>
        /// Constructs the formatted word guess string for display
        /// </summary>
        /// <returns>Formatted guess string (eg "G U _ S S")</returns>
        public string GetWordState()
        {
            string state = "";
            foreach(char letter in commonLetters)
            {
                state += letter + " ";
            }
            return state;
        }
        /// <summary>
        /// Gets the number of words in the word family
        /// </summary>
        internal int GetSize => words.Length;
        /// <summary>
        /// Gets a random word from the word family
        /// - Spawns a random object
        /// - Generates a random number between 0 and number of words in word family
        /// </summary>
        /// <returns>Word from word list at index of random number generated</returns>
        public string GetRandomWord()
        {
            Random rand = new Random();
            return words[rand.Next(0, words.Length)];
        }

        // CONSTRUCTORS
        /// <summary>
        /// Constructs a word family with the given parameters.
        /// </summary>
        /// <param name="wordList">List of words in the family</param>
        /// <param name="guessedLetters">List of letters that have been guessed</param>
        /// <param name="initialList">true: no previous guessed letters, false: previous guessed letters</param>
        public WordFamily(string[] wordList,string guessedLetters, bool initialList)
        {
            words = wordList;
            commonLetters = "";
            if (!initialList)
            {
                commonLetters = GenerateCommonLetters(guessedLetters, wordList[0]);
            }
            else
            {
                foreach (char letter in wordList[0])
                {
                    commonLetters += "_";
                }
            }
        }

        
        //METHODS
        /// <summary>
        /// Generates the common letters string by comparing to letters guessed so far.
        /// </summary>
        /// <param name="guessedLetters">guessed letters string (including last guessed character)</param>
        /// <param name="sampleWord">sample word from new word family</param>
        /// <returns></returns>
        private string GenerateCommonLetters(string guessedLetters, string sampleWord)
        {
            string newCommonLetters = "";
            sampleWord = sampleWord.ToUpper();
            for (int i = 0; i < sampleWord.Length; i+=1)
            {
                
                if (guessedLetters.Contains(sampleWord[i]))
                {
                    newCommonLetters += sampleWord[i];
                }
                else
                {
                    newCommonLetters += "_";
                }
            }
            Debug.DebugMessage("Common letter generation - Sample word is " + sampleWord + " common letters are " + newCommonLetters, 4);
            return newCommonLetters;
        }
        /// <summary>
        /// Uses default (largest word family) method to get the next optimum word family
        /// </summary>
        /// <param name="guess">The new guess letter</param>
        /// <param name="guessedLetters">Letters already guessed</param>
        /// <returns></returns>
        public WordFamily GetNewOptimumWordFamily(char guess, string guessedLetters)
        {

            WordFamily newFamily = new WordFamily(this.ComputeLargestWordFamily(this.ComputeSubFamilies(guess)).ToArray(), guessedLetters, false);
            return newFamily;
        }
        /// <summary>
        /// Gets the new optimum word family (choice of mode)
        /// 1 - Largest word family
        /// 2 - Letter frequency score
        /// 3 - Decision tree (not implemented)
        /// </summary>
        /// <param name="guess">Guessed letter this round</param>
        /// <param name="guessedLetters">All guessed letters so far</param>
        /// <param name="mode">Algorithm to use (see method summary)</param>
        /// <returns></returns>
        public WordFamily GetNewOptimumWordFamily(char guess, string guessedLetters, int mode)
        {
            switch(mode)
            {
                case 1:
                    WordFamily newFamily = new WordFamily(this.ComputeLargestWordFamily(ComputeSubFamilies(guess)).ToArray(), guessedLetters, false);
                    return newFamily;
                case 2:
                    newFamily = new WordFamily(ComputeOptimumByFrequency(ComputeSubFamilies(guess)), guessedLetters, false);
                    return newFamily;
                default:
                    throw new NotImplementedException();
            }
            
        }
        /// <summary>
        /// Method for computing the optimum word family using the word frequency score algorithm
        /// </summary>
        /// <param name="wordLists">Wordlists to analyze</param>
        /// <returns></returns>
        private string[] ComputeOptimumByFrequency(List<string>[] wordLists)
        {
            List<string> best = new List<string>();
            double bestScore = 100;
            foreach(List<string> list in wordLists)
            {
                if (list.Count > 0)
                {
                    double score = ComputeMeanWordFrequencyScore(list);
                    if (score < bestScore)
                    {
                        best = list;
                    }
                }
            }
            return best.ToArray();
        }
        /// <summary>
        /// Computes sub families based upon guess input
        /// </summary>
        /// <param name="guess">User supplied guess letter</param>
        /// <returns>Computed word sub families</returns>
        private List<string>[] ComputeSubFamilies(char guess)
        {
            Debug.DebugMessage("Computing families for guess " + guess);
            string lowerguess = guess.ToString().ToLower();
            guess = lowerguess[0];
            // TIER 1
            // Divide words by first occurence of guess letter
            List<string>[] wordLists = new List<string>[words[0].Length + 1];
            for (int i = 0; i < words[0].Length + 1; i++)
            {
                wordLists[i] = new List<string>();
            }
            foreach (string word in words)
            {
                wordLists[word.IndexOf(guess) + 1].Add(word);
            }
            foreach (List<string> list in wordLists)
            {
                Debug.DebugMessage("Word counts: " + list.Count, 4);
            }


            // TIER 2
            // Separate words with 2 or more occurences of guess letter
            int sizeOfWordlists2 = 0;
            for (int i = wordLists.Length-1; i >= 1; i--)
            {
                sizeOfWordlists2 += i;
            }
            Debug.DebugMessage("Tier 2 number of sub arrays is " + sizeOfWordlists2, 4);
            List<string>[] wordLists2 = new List<string>[sizeOfWordlists2];
            for (int i = 0; i < wordLists2.Length; i++)
            {
                wordLists2[i] = new List<string>();
            }
            for (int i = 1; i < wordLists.Length; i++)
            {
                int offset = 0;
                int offsetvalue = wordLists.Length;
                for (int j = 0; j < i; j++)
                {
                    if (!(j == 0) && !(i == 1))
                    {
                        offset += offsetvalue;
                    }

                    offsetvalue -= 1;
                }
                foreach (string word in wordLists[i])
                {
                    wordLists2[word.Substring(i).IndexOf(guess) + 1 + offset].Add(word);
                }
            }
            List<string>[] allWordLists = new List<string>[wordLists2.Length+1];
            wordLists2.CopyTo(allWordLists,0);
            allWordLists[wordLists2.Length]=wordLists[0];
            return allWordLists;

        }
        /// <summary>
        /// Computes the largest family of words from the supplied set
        /// </summary>
        /// <param name="wordLists">Array of word lists</param>
        /// <returns>Largest word list</returns>
        private List<string> ComputeLargestWordFamily(List<string>[] wordLists)
        {
            List<string> largest = wordLists[0];
            foreach (List<string> list in wordLists)
            {
                if (list.Count > largest.Count)
                {
                    largest = list;
                }
            }
            return largest;
        }
        /// <summary>
        /// Computes the mean word frequency score by:
        /// - Summing the letter frequencies for each letter of each word
        /// - Dividing the sum by the number of words in the list
        /// </summary>
        /// <returns>Mean word frequency score</returns>
        private double ComputeMeanWordFrequencyScore(List<string> wordList)
        {
            double count = 0;
            foreach(string word in wordList)
            {
                foreach(char letter in word.ToUpper())
                {
                    count += CharData.Frequency(letter);
                }
            }
            return count / words.Length;
        }
    }
}