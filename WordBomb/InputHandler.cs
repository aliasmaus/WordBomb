using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBomb
{
    public static class InputHandler
    {
        /// <summary>
        /// Displays a message and recieves user input then varifies that the input is an integer within the INCLUSIVE range provided
        /// Until a valid input is recieved:
        /// - Write the message to the console
        /// - Read user input and attempt to convert to integer
        /// - When an integer is recieved, check that it is within the supplied range
        /// </summary>
        /// <param name="message">The message to output to the console</param>
        /// <param name="minValue">Minimum inclusive value of valid input</param>
        /// <param name="maxValue">Maximum inclusive value of valid input</param>
        /// <returns>The valid user supplied integer</returns>
        public static int GetValidIntInput(string message, int minValue, int maxValue)
        {
            bool valid = false;
            int input = -100;
            do
            {
                try
                {
                    Console.WriteLine(message);
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Invalid input! Requires an integer between {0} and {1}", minValue, maxValue);
                    continue;
                }
                if (input >= minValue && input <= maxValue)
                {
                    valid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input! Requires an integer between {0} and {1}", minValue, maxValue);
                }
            }
            while (!valid);

            return input;
        }
        /// <summary>
        /// Displays a message and recieves a used input which is converted to an integer
        /// Until a valid input is recieved:
        /// - Write the message to the console
        /// - Read user input and attempt to convert to integer
        /// </summary>
        /// <param name="message">The message to output to the console</param>
        /// <returns>The valid user supplied integer</returns>
        public static int GetValidIntInput(string message)
        {
            bool valid = false;
            int input = -100;
            do
            {
                try
                {
                    Console.WriteLine(message);
                    input = Convert.ToInt32(Console.ReadLine());
                    valid = true;
                }
                catch
                {
                    Console.WriteLine("Invalid input! Requires an integer number");
                }

            } while (!valid);
            return input;
        }
        /// <summary>
        /// Displays a message to the console and recieves user input
        /// </summary>
        /// <param name="message">The message to write to the console</param>
        /// <returns>The user supplied string</returns>
        public static string GetValidStringInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
        /// <summary>
        /// Displays a message to the console then prompts user for input, checks input is contained within list of valid words
        /// Until a valid input is recieved:
        /// - Write the message to the console
        /// - Check if the message is contained in the valid word list
        /// </summary>
        /// <param name="message">The message to write to the console</param>
        /// <param name="validWords">A list of valid user inputs</param>
        /// <returns>The valid user supplied string</returns>
        public static string GetValidStringInput(string message, string[] validWords)
        {
            Console.WriteLine(message);
            bool valid = false;

            string input = "";
            do
            {
                input = Console.ReadLine();
                if (validWords.Contains(input))
                {
                    valid = true;
                }
                else
                {
                    Console.WriteLine("Invalid input! valid inputs - " + validWords.ToString());
                }
            } while (!valid);

            return input;
        }
        /// <summary>
        /// Displays a message to the console then prompts user for input, checks input is contained within list of valid words then converts the input to upper or lower case
        /// Until a valid input is recieved:
        /// - Write the message to the console
        /// - Check if the message is contained in the valid word list
        /// - If valid, convert input to upper/lower case
        /// </summary>
        /// <param name="message">The message to write to the console</param>
        /// <param name="validWords">A list of valid user inputs</param>
        /// <param name="upper">True to return upper case, false to return lower case</param>
        /// <returns>The valid user supplied string converted to upper or lower case</returns>
        public static string GetValidStringInput(string message, string[] validWords, bool upper)
        {
            string input = GetValidStringInput(message, validWords);
            if (upper)
            {
                return input.ToUpper();
            }
            else
            {
                return input.ToLower();
            }
        }
        /// <summary>
        /// Displays a message to the console then prompts user for input, checks input is contained within list of valid characters then converts the input to upper or lower case string
        /// Until a valid input is recieved:
        /// - Write the message to the console
        /// - Check if the message is contained in the valid word list
        /// - If valid, convert input to upper/lower case
        /// </summary>
        /// <param name="message">The message to write to the console</param>
        /// <param name="validWords">A list of valid user inputs</param>
        /// <param name="upper">True to return upper case, false to return lower case</param>
        /// <returns>The valid user supplied string converted to upper or lower case</returns>
        public static string GetValidStringInput(string message, char[] validWords, bool upper)
        {
            List<string> validWordsStrings = new List<string>();
            foreach (char letter in validWords)
            {
                validWordsStrings.Add(Convert.ToString(letter));
            }
            return GetValidStringInput(message, validWordsStrings.ToArray(), true);
        }
    }
}
