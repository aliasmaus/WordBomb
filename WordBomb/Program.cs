using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBomb
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
            //  Console set-up
            Console.WindowWidth = 105;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Title = "WORDBOMB";

            // Game Start
            bool debugMode = false;
            bool quit = false;
            do
            {
                // Main menu
                Console.Clear();
                UI.DrawTitle();
                int menuChoice = InputHandler.GetValidIntInput("Start menu:\n1. Play Game\n2. Credits\n3. Set Debug on/off \n4. Quit", 1, 4);
                switch (menuChoice)
                {
                    // Play Game
                    case 1:
                        bool continueGame = true;
                        do
                        {
                            // Get game configuration options
                            int[] configuration = UI.DrawStartScreen();

                            // Start game
                            Game game = new Game(configuration[0], configuration[1], configuration[2])
                            {
                                mode = configuration[3]
                            };

                            // While game not complete, progress game
                            do
                            {
                                string guess = UI.DrawGameScreen(game, debugMode);
                                game.EvaluateUserGuess(guess[0]);

                            }
                            while (game.EvaluateGameState() == 1);

                            // When game complete and lose state achieved show lose screen (prompts to play again)
                            if (game.EvaluateGameState() == 0)
                            {
                                continueGame = UI.DrawGameOverScreen(game);
                            }
                            // Else game must be won so display win screen (prompts to play again)
                            else
                            {
                                continueGame = UI.DrawWinScreen(game);
                            }

                        } while (continueGame == true);
                        break;

                    // AI vs AI mode (Not implemented in final version)
                    //case 2:
                    //    bool runAgain = true;
                    //    do
                    //    {
                    //        Console.Clear();
                    //        Console.WriteLine("How many sample games should the computer play?");
                    //        int samples = Convert.ToInt32(Console.ReadLine());
                    //        Console.Clear();
                    //        Console.WriteLine("What level?\n1. 2-5 letter words\n2. 6-8 letter words\n 3. 9+ letter words");
                    //        int level = Convert.ToInt32(Console.ReadLine());
                    //        Console.Clear();
                    //        Console.WriteLine("How many wrong guesses are allowed?");
                    //        int guesses = Convert.ToInt32(Console.ReadLine());
                    //        Console.Clear();
                    //        Console.WriteLine("Running");
                    //        AIvsAI.RunAIvsAIRandomGuess(samples, level, guesses);
                    //        Console.WriteLine("Run again? (yes/no)");
                    //        if (Console.ReadLine() == "no")
                    //        {
                    //            runAgain = false;
                    //        }
                    //    } while (runAgain);
                    //    break;

                    // Display credits
                    case 2:
                        UI.DrawCreditsScreen();
                        break;
                    // Turn Debug mode on/off
                    case 3:
                        UI.DrawTitle();
                        string debugON = InputHandler.GetValidStringInput("Turn debug mode on? (yes/no) ", new string[]{ "YES", "yes", "NO" , "no"});
                        if (debugON.ToLower() == "yes")
                        {
                            debugMode = true;
                        }
                        else
                        {
                            debugMode = false;
                        }
                        break;
                    // Quit Application
                    case 4:
                        quit = true;
                        break;
                    // Should never be reached because of input validation
                    default:
                        throw new NotImplementedException();
                }
            } while (!quit);
            
        }
    }
}
