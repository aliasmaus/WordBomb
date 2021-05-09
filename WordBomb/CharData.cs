using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBomb
{
    static class CharData
    {
        /// <summary>
        /// letters in the alphabet as string for index reference
        /// </summary>
        private static readonly string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        /// <summary>
        /// letter frequency lookup array
        /// </summary>
        private static readonly double[] normalisedLetterFrequencies = new double[]
        {
                0.0812, //A
                0.0149, //B
                0.0271, //C
                0.0432, //D
                0.1202, //E
                0.0230, //F
                0.0203, //G
                0.0592, //H
                0.0731, //I
                0.0010, //J
                0.0069, //K
                0.0398, //L
                0.0261, //M
                0.0695, //N
                0.0768, //O
                0.0182, //P
                0.0011, //Q
                0.0602, //R
                0.0628, //S
                0.0910, //T
                0.0288, //U
                0.0111, //V
                0.0209, //W
                0.0017, //X
                0.0211, //Y
                0.0007  //Z
        };
        /// <summary>
        /// get letter char by index
        /// </summary>
        /// <param name="letterIndex">index to retrieve</param>
        /// <returns>letter at index</returns>
        public static char Letter(int letterIndex) => letters[letterIndex];
        /// <summary>
        /// get alphabet index by letter char
        /// </summary>
        /// <param name="letter">letter to find index of</param>
        /// <returns>index of letter in alphabet</returns>
        public static int LetterIndex(char letter) => letters.IndexOf(letter);
        /// <summary>
        /// get letter frequency by index
        /// </summary>
        /// <param name="letterIndex">index of letter</param>
        /// <returns>frequency of letter</returns>
        public static double Frequency(int letterIndex) => normalisedLetterFrequencies[letterIndex];
        /// <summary>
        /// get frequency by letter char
        /// </summary>
        /// <param name="letter">letter to lookup</param>
        /// <returns>frequency of letter</returns>
        public static double Frequency(char letter) => normalisedLetterFrequencies[letters.IndexOf(letter)];
        /// <summary>
        /// get char array of upper and lower letters for input validation
        /// </summary>
        /// <returns>char array of upper and lower letters: alphabetical upper then lower</returns>
        public static char[] LetterCharArray() => (letters + letters.ToLower()).ToCharArray();
        
    }
}
