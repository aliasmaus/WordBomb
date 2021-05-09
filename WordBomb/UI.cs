using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBomb
{
    /// <summary>
    /// For drawing the various screens to the console
    /// </summary>
    static class UI
    {
        // Screen strings
        private static readonly string gameOverScreen = File.ReadAllText("ascii_art\\gameover.txt") + "\n\n" + File.ReadAllText("ascii_art\\gameovertext.txt");
        private static readonly string winScreen = File.ReadAllText("ascii_art\\winnerText.txt");
        private static readonly string title = File.ReadAllText("ascii_art\\title.txt");
        private static readonly string divider = "--------------------------------------------------------------------------------------------------------";
        // Credits
        private static readonly string[] credits = new string[] 
        {
            "Game design: Louise Amas",
            "Code: Louise Amas",
            "Ascii pictures: asciiart.eu",
            "Ascii text art: patorjk.com/software/taag/",
            "Bomb icon: OpenClipart-Vectors via pixabay.com\t https://pixabay.com/service/license/"
        };
        // Center point of console for centered stuff
        private static readonly int center = Console.WindowWidth / 2;

        // SCREEN METHODS
        /// <summary>
        /// Writes the current in play status to the console then prompts the user for another guess
        /// </summary>
        /// <param name="game">The in-play game to display</param>
        /// <param name="debug">true to show debug</param>
        /// <returns>Next guess letter</returns>
        public static string DrawGameScreen(Game game, bool debug)
        {
            //Make display strings
            string roundString = "Round: " + game.GetRound;
            string guessedString = "Guessed letters: " + game.GetGuessedLetters;
            string wrongGuessesString = "Wrong guesses remaining: " + game.GetGuessesLeft;
            string wordStateString = "WORD: " + game.GetCurrentWordState;

            //Draw display
            DrawTitle();
            Debug.DebugMessage(roundString + " " + guessedString + " " + wordStateString,2);
            Console.WriteLine(roundString);
            Console.WriteLine(guessedString);
            Console.WriteLine(wrongGuessesString);
            Console.WriteLine("\n\n" + wordStateString + "\n\n");
            DrawCenteredText(game.GetPicture);
            if (debug)
            {
                Console.WriteLine("DEBUGGING:\nCurrent word family size: " + game.CurrentWordListLength + "\nSample word: " + game.GetRandomWordFromFamily + "\n");
            }
            if (Convert.ToInt32(game.GetRound)==0)
            {
                return InputHandler.GetValidStringInput("Guess a letter!", validWords: CharData.LetterCharArray(), upper: true);
            }
            else
            {
                return InputHandler.GetValidStringInput("Guess another letter!", validWords: CharData.LetterCharArray(), upper: true);
            }          
        }
        /// <summary>
        /// Writes the win screen to the console then prompts the user if they want to play again
        /// </summary>
        /// <param name="game">The in-play game that has been won</param>
        /// <returns>true for play again, false for end game</returns>
        public static bool DrawGameOverScreen(Game game)
        {
            Debug.DebugMessage("Game over - LOSE state achieved", 2);
            Console.Clear();
            Console.WriteLine(divider);
            Console.ForegroundColor = ConsoleColor.Red;
            DrawCenteredText(gameOverScreen);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\nThe answer was " + game.GetRandomWordFromFamily);
            Console.WriteLine(divider);
            string playAgain = InputHandler.GetValidStringInput("Play again? (yes/no)", new string[2] { "yes", "no" });
            if(playAgain == "yes")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Writes the start screen to the console then prompts the user for game parameters
        /// </summary>
        /// <returns>Game parameters 0: Number of guesses, 1: level to play</returns>
        public static int[] DrawStartScreen()
        {
            Console.Clear();
            Debug.DebugMessage("Starting new game------------------------------------------------------------------------------------",2);
            int[] configuration = new int[4];

            DrawTitle();
            configuration[0] = InputHandler.GetValidIntInput("\nChoose a number of guesses (between 1 and 15)", 1, 15);

            DrawTitle();
            configuration[1] = InputHandler.GetValidIntInput("\nSelect a level:\n1. Short words - 2-5 letters\n2. Medium words - 6-8 letters\n3. Long words - 9+ letters\n4. Custom word length - 2-29 letters", 1, 4);

            if (configuration[1] == 4)
            {
                bool valid = false;
                do
                {
                    configuration[2] = InputHandler.GetValidIntInput("Enter a word length", 2, 29);
                    valid = File.Exists("dictionaries\\" + configuration[2] + "letterwords.txt");
                    if (!valid)
                    {
                        Console.WriteLine("No words of that length, please try again!");
                    }
                } while (!valid);
                
            }

            DrawTitle();
            configuration[3] = InputHandler.GetValidIntInput("\nSelect cheat mode:\n1. Largest word family\n2. Lowest word frequency score",1,2);

            return configuration;
        }
        /// <summary>
        /// Writes the lose screen to the console then prompts the user if they want to play again
        /// </summary>
        /// <param name="game">The in-play game that has been lost</param>
        /// <returns>true for play again, false for end game</returns>
        public static bool DrawWinScreen(Game game)
        {
            Debug.DebugMessage("Game over - WIN state achieved",2);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            DrawCenteredText(winScreen);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\nThe answer was " + game.GetCurrentWordState);
            Console.WriteLine(divider);
            string playAgain = InputHandler.GetValidStringInput("Play again? (yes/no)", new string[2] { "yes", "no" });
            if (playAgain == "yes")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Writes the credits to the console
        /// </summary>
        public static void DrawCreditsScreen()
        {
            DrawTitle();
            foreach(string credit in credits)
            {
                Console.WriteLine("\n");
                Console.WriteLine(credit);
            }
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        /// <summary>
        /// Draws the formatted WordBomb Title ascii text on a clean console
        /// </summary>
        public static void DrawTitle()
        {
            Console.Clear();
            
            Console.WriteLine(divider);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(title);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(divider);
            
        }

        /// <summary>
        /// Draws centered text to the console (handles multi-line)
        /// </summary>
        /// <param name="text">text to be drawn</param>
        private static void DrawCenteredText(string text)
        {
            string[] lines = text.Split('\n');
            foreach (string line in lines)
            {
                int leftOffset = center - (line.Length / 2);
                string whitespace = "";
                for (int i = 0; i < leftOffset; i++)
                {
                    whitespace += " ";
                }
                Console.WriteLine(whitespace + line);
            }
        }
    }
}
