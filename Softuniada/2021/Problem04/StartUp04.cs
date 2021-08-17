using System;
using System.Linq;
using System.Text;

namespace Problem04
{
    class StartUp04
    {
        static void Main()
        {
            int[] coordinates = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            int rows = coordinates[0];
            int cols = coordinates[1];

            var matrix = new string[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                string[] line = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = line[j].ToString();
                }
            }

            string egg = Console.ReadLine();

            var eggCoords = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            int row = eggCoords[0];
            int col = eggCoords[1];

            string current = matrix[row, col];
                       
            FindAllEggs(matrix, row, col, egg, current);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        private static void FindAllEggs(string[,] matrix, int row, int col, string egg, string current)
        {
            if (!IsInBounds(matrix, row, col))
            {
                return;
            }

            if (matrix[row, col] != current)
            {
                return;
            }

            if (matrix[row, col] == current)
            {
                matrix[row, col] = egg;
            }

            FindAllEggs(matrix, row - 1, col, egg, current);
            FindAllEggs(matrix, row + 1, col, egg, current);
            FindAllEggs(matrix, row, col - 1, egg, current);
            FindAllEggs(matrix, row, col + 1, egg, current);

        }

        private static bool IsInBounds(string[,] matrix, int row, int col)
        {
            return row >= 0 && row < matrix.GetLength(0) && col >= 0 && col < matrix.GetLength(1);
        }
    }
}
