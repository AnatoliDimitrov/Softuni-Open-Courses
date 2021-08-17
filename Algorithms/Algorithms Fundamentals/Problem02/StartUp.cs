using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Dynamic;

namespace Problem02
{
    class StartUp
    {
        static void Main()
        {
            int[] firstSock = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int[] secondSock = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            LCS(firstSock, secondSock);
        }

        private static void LCS(int[] firstSock, int[] secondSock)
        {
            int[,] matrix = new int[firstSock.Length + 1, secondSock.Length +1];

            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    if (firstSock[i - 1] == secondSock[j - 1])
                    {
                        matrix[i, j] = matrix[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        matrix[i, j] = Math.Max(matrix[i - 1, j], matrix[i, j - 1]);
                    }
                }
            }

            Console.WriteLine(matrix[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1]);
        }
    }
}
