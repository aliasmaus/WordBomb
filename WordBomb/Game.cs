using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBomb
{
    /// <summary>
    /// A game of guess the word where the computer can cheat!
    /// Contains everything you need to:
    /// 1. Start a new game
    /// 2. Evaluate a guess (progress the game)
    /// 3. Check for win/lose condition
    /// 4. Get current game state data
    /// </summary>
    class Game
    {
        private int round = 0;
        private string guessedLetters = "";
        private WordFamily currentWordFamily;
        private string[] pictures;
        private char lastGuess;
        private int guessesLeft;
        public int mode = 1;

        // CONSTRUCTORS
        /// <summary>
        /// Starts a Human vs AI game with the parameters input (no console prompt)
        /// </summary>
        /// <param name="guesses">Number of wrong guesses permitted</param>
        /// <param name="level">Word length setting (see LoadLevel() for key)</param>
        public Game(int guesses, int level, int custom)
        {
            // set game parameters and load
            guessesLeft = guesses;
            LoadLevel(Convert.ToInt32(level), custom);
            LoadPictures(guessesLeft);
            
        }
        /// <summary>
        /// Starts a new AI game if AI set to true, otherwise does nothing
        /// </summary>
        /// <param name="guesses">Number of guesses allowed</param>
        /// <param name="level">Level to play game</param>
        /// <param name="AI">true for AI game, false does nothing</param>
        public Game(int guesses, int level, int custom, bool AI)
        {
            if(AI)
            {
                // set game parameters and load
                guessesLeft = guesses;
                LoadLevel(Convert.ToInt32(level), custom);
            }
        }
        /// <summary>
        /// Starts a Human vs AI game and prompts user in the console for game parameters
        /// </summary>
        //public Game()
        //{
        //    // Get level
        //    try
        //    {
        //        Console.WriteLine("Select a level:\n1. Short words - 2-5 letters\n2. Medium words - 6-8 letters\n3. Long words - 9+ letters\n4. Custom word length - 2-27 letters");
        //        LoadLevel(Convert.ToInt32(Console.ReadLine()));
        //    }
        //    catch
        //    {
        //        Console.WriteLine("Invalid input! Requires an integer between 1 and 4\nPress any key to continue...");
        //        Console.ReadKey();
        //    }
        //    // Get number of guesses
        //    try
        //    {
        //        Console.WriteLine("How many wrong guesses do you want? (max 16)");
        //        guessesLeft = Convert.ToInt32(Console.ReadLine());
        //    }
        //    catch
        //    {
        //        Console.WriteLine("Invalid input! Requires an integer between 1 and 16\nPress any key to continue...");
        //        Console.ReadKey();
        //    }

        //}


        // GETTERS
        public string GetPicture => pictures[guessesLeft - 1];
        public string GetGuessesLeft => guessesLeft.ToString();
        public string GetGuessedLetters => guessedLetters;
        public string GetRound => round.ToString();
        public string GetCurrentWordState => currentWordFamily.GetWordState();
        public string CurrentWordListLength => currentWordFamily.GetSize.ToString();
        public string GetRandomWordFromFamily => currentWordFamily.GetRandomWord();


        // GAMEPLAY
        /// <summary>
        /// Loads pictures from ascii art text files (named bomb#.txt starting with bomb0.txt)
        /// Currently there are 16 such files
        /// </summary>
        /// <param name="guesses">number of guesses determines length of wick and therefore how many pictures to load</param>
        private void LoadPictures(int guesses)
        {
            pictures = new string[guesses];

            Console.WriteLine("Loading ascii art...");
            for (int i = 0; i < guesses; i++)
            {
                pictures[i] = File.ReadAllText("ascii_art\\bomb" + i.ToString() + ".txt");
            }
        }
        /// <summary>
        /// Loads the word list for the selected level:
        /// 1. 2-5 letters
        /// 2. 6-8 letters
        /// 3. 9+ letters
        /// 4. prompt for custom length (max 27)
        /// </summary>
        /// <param name="level">Integer between 1 and 4 corresponding to the selected level</param>
        private void LoadLevel(int level, int custom)
        {
            Random rand = new Random();
            List<string> levelWordList; 
            switch(level)
            {
                case 1:
                    int lengthOfWord = rand.Next(2, 6);
                    levelWordList = File.ReadAllLines("dictionaries\\" + lengthOfWord + "letterwords.txt").ToList();
                    break;

                case 2:
                    lengthOfWord = rand.Next(6, 9);
                    levelWordList = File.ReadAllLines("dictionaries\\" + lengthOfWord + "letterwords.txt").ToList();
                    break;

                case 3:
                    lengthOfWord = rand.Next(9, 16);
                    levelWordList = File.ReadAllLines("dictionaries\\" + lengthOfWord + "letterwords.txt").ToList();
                    break;
                case 4:
                    levelWordList = File.ReadAllLines("dictionaries\\" + custom + "letterwords.txt").ToList();
                    break;
                default:
                    throw new NotImplementedException();
            }
            currentWordFamily = new WordFamily(levelWordList.ToArray(), guessedLetters, true);
        }

        /// <summary>
        /// Moves the gameplay forawrd and evaluates the supplied guess against the word list including trying to cheat
        /// </summary>
        /// <param name="guess">A single character guess</param>
        public void EvaluateUserGuess(char guess)
        {
            Debug.DebugMessage("Evaluating guess", 2);
            round += 1;
            lastGuess = guess;
            guessedLetters += guess + " ";
            TryToCheat(guess);
            if(!currentWordFamily.CommonLetters.Contains(guess))
            {
                guessesLeft -= 1;
            }
        }
        /// <summary>
        /// Try to cheat by using the largest word family available that matches the current word state
        /// </summary>
        /// <param name="guess">The user supplied guess that is being evaluated</param>
        private void TryToCheat(char guess)
        {
            WordFamily newFamily = currentWordFamily.GetNewOptimumWordFamily(guess, guessedLetters, mode);
            Debug.DebugMessage("Trying to cheat... current top word - " + currentWordFamily.words[0] + " new top word " + newFamily.words[0], 4);
            currentWordFamily = newFamily;
            
        }
        /// <summary>
        /// Check for win/lose conditions
        /// </summary>
        /// <returns>0 for lose, 1 for game in play, 2 for winner</returns>
        public int EvaluateGameState()
        {
            if (!currentWordFamily.GetWordState().Contains('_'))
            {
                return 2;
            }
            else if (guessesLeft == 0)
            {
                return 0;
            }
            return 1;            
        }  
    }
}
