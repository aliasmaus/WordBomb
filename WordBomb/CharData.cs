using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBomb
{
    static class CharData
    {
        private static readonly string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
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
        public static char Letter(int letterIndex) => letters[letterIndex];
        public static int LetterIndex(char letter) => letters.IndexOf(letter);
        public static double Frequency(int letterIndex) => normalisedLetterFrequencies[letterIndex];
        public static double Frequency(char letter) => normalisedLetterFrequencies[letters.IndexOf(letter)];
        public static char[] LetterCharArray() => (letters + letters.ToLower()).ToCharArray();
        
    }
}
