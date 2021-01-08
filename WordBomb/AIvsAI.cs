using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBomb
{
    static class AIvsAI
    {
        

        /// <summary>
        /// Runs sample games in which the AI player makes random guesses
        /// </summary>
        /// <param name="samples">number of samples to run</param>
        /// <param name="level">level for word selection</param>
        /// <param name="guesses">number of wrong guesses permitted</param>
        public static void RunAIvsAIRandomGuess(int samples, int level, int guesses, int mode)
        {
            Debug.DebugMessage("Starting AI vs AI game------------------------------------------------------------------------------------------", 2);
            Random rand = new Random();
            for(int i = 0; i < samples; i++)
            {
                Debug.DebugMessage("Starting sample game " + (i + 1).ToString(), 2);
                Console.WriteLine("Playing sample game " + (i + 1));
                Game game = new Game(guesses, level, -1, true)
                {
                    mode = mode
                };
                int success = 0;
                int fail = 0;
                do
                {
                    string guessed = "";
                    int guessLetter = rand.Next(0, 26);
                    Debug.DebugMessage("Guessing letter: " + CharData.Letter(guessLetter), 2);
                    int letterFound = 0;
                    if(!guessed.Contains(CharData.Letter(guessLetter)))
                    {
                        game.EvaluateUserGuess(CharData.Letter(guessLetter));
                        if (game.GetCurrentWordState.Contains(CharData.Letter(guessLetter)))
                        {
                            letterFound = 1;
                            Debug.DebugMessage("Letter found", 4);
                            success += 1;
                        }
                        else
                        {
                            fail += 1;
                        }
                        guessed += CharData.Letter(guessLetter);
                        Debug.DebugMessage("Appending data to csv file", 4);
                        File.AppendAllText("AIvsAIdata.csv", game.GetRound + "," + guessLetter + "," + game.GetCurrentWordState.Length / 2 + "," + letterFound + "\n");
                    }
                    else
                    {
                        Debug.DebugMessage("Letter already guessed... skipping", 4);
                    }
                    
                } while (game.EvaluateGameState() == 1);
                Console.WriteLine("Game complete! Successful guesses: " + success + " Unsuccessful guesses: " + fail + " Remaining words: " + game.CurrentWordListLength);
            }
        }
        public static void RunAIvsAIByLetterFrequency(int samples, int level, int guesses)
        {
            throw new NotImplementedException();
        }
        public static void RunAIvsAIByDecisionTree(int samples, int level, int guesses)
        {
            throw new NotImplementedException();
        }
    }
}
