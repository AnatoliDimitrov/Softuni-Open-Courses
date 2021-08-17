using System;
using System.Collections.Generic;
using System.Linq;

namespace _04_Time
{
    class StartUp
    {
        private static int[,] matrix;

        static void Main()
        {
            var firstLine = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var secondLine = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            matrix = new int[firstLine.Length + 1, secondLine.Length + 1];

            GetTimeLine(firstLine, secondLine);
        }

        private static void GetTimeLine(int[] firstLine, int[] secondLine)
        {
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    if (firstLine[i - 1] == secondLine[j - 1])
                    {
                        matrix[i,j] = matrix[i - 1, j - 1] + 1;
                    }
                    else if (matrix[i - 1, j] > matrix[i, j - 1])
                    {
                        matrix[i, j] = matrix[i - 1, j];
                    }
                    else
                    {
                        matrix[i, j] = matrix[i, j - 1];
                    }
                }
            }

            var result = new Stack<int>();

            int row = matrix.GetLength(0) - 1;
            int col = matrix.GetLength(1) - 1;

            while (row != 0 && col != 0)
            {
                if (firstLine[row - 1] == secondLine[col - 1])
                {
                    result.Push(firstLine[row - 1]);
                    row--;
                    col--;
                    continue;
                }
                if (matrix[row - 1, col] > matrix[row, col - 1])
                {
                    row--;
                }
                else
                {
                    col--;
                }
            }

            Console.WriteLine(string.Join(" ", result));
            Console.WriteLine(matrix[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1]);
        }
    }
}
