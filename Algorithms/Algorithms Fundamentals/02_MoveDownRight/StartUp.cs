using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_MoveDownRight
{
    class StartUp
    {
        private static int[,] matrix;
        private static int[,] result;

        static void Main()
        {
            int height = int.Parse(Console.ReadLine());
            int width = int.Parse(Console.ReadLine());

            matrix = new int[height, width];
            result = new int[height, width];

            for (int i = 0; i < height; i++)
            {
                int[] line = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int j = 0; j < width; j++)
                {
                    matrix[i, j] = line[j];
                }
            }

            FindPathWithHighestSum();
        }

        private static void FindPathWithHighestSum()
        {
            result[0, 0] = matrix[0, 0];

            for (int i = 1; i < matrix.GetLength(1); i++)
            {
                result[0, i] = result[0, i - 1] + matrix[0, i];
            }

            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                result[i, 0] = result[i - 1, 0] + matrix[i, 0];
            }

            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    result[i, j] = Math.Max(result[i, j - 1], result[i - 1, j]) + matrix[i, j];
                }
            }

            int row = matrix.GetLength(0) - 1;
            int col = matrix.GetLength(1) - 1;

            Stack<string> path = new Stack<string>();
            path.Push($"[{row}, {col}]");
            ;
            while (row > 0 && col > 0)
            {
                if (result[row - 1, col] > result[row, col - 1])
                {
                    path.Push($"[{row - 1}, {col}]");
                    row -= 1;
                }
                else
                {
                    path.Push($"[{row}, {col - 1}]");
                    col -= 1;
                }
            }

            while (row > 0)
            {
                path.Push($"[{row - 1}, {col}]");
                row -= 1;
            }

            while (col > 0)
            {
                path.Push($"[{row}, {col - 1}]");
                col -= 1;
            }

            Print(path);
        }

        private static void Print(Stack<string> path)
        {
            Console.WriteLine(string.Join(" ", path));
        }
    }
}
