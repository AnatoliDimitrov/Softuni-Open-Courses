using System;
using System.Collections.Generic;

namespace _03_LongestCommonSubsequence
{
    class StartUp
    {
        private static int[,] matrix;

        static void Main()
        {
            var firstString = Console.ReadLine();
            var secondString = Console.ReadLine();

            matrix = new int[firstString.Length + 1, secondString.Length + 1];

            int result = FindLongestCommonSubsequence(firstString, secondString);

            Console.WriteLine(result);
        }

        private static int FindLongestCommonSubsequence(string firstString, string secondString)
        {
            for (int i = 1; i < matrix.GetLength(0); i++)
                for (int j = 1; j < matrix.GetLength(1); j++)
                    matrix[i, j] = firstString[i - 1] == secondString[j - 1] ? matrix[i - 1, j - 1] + 1 : Math.Max(matrix[i - 1, j], matrix[i, j - 1]);

            return matrix[matrix.GetLength(0) - 1, matrix.GetLength(1) -1];
        }

        
    }
}
