using System;
using System.Linq;
using System.Collections.Generic;

namespace Problem01
{
    class StartUp
    {
        private static string[,] matrix;
        private static bool[,] visited;

        static void Main()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            matrix = new string[rows, cols];
            visited = new bool[rows, cols];

            ReadMatrix(rows, cols);

            int tunnelsCount = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] == "t" && visited[i, j] == false)
                    {
                        tunnelsCount++;
                        GetTunnel(i, j);
                    }
                }
            }

            Console.WriteLine(tunnelsCount);
        }

        private static void GetTunnel(int row, int col)
        {
            if (IsOutOfMatrix(row, col) || matrix[row, col] == "d" || visited[row, col])
            {
                return;
            }

            visited[row, col] = true;


            GetTunnel(row - 1, col);
            GetTunnel(row + 1, col);
            GetTunnel(row, col + 1);
            GetTunnel(row, col - 1);
            GetTunnel(row - 1, col - 1);
            GetTunnel(row - 1, col + 1);
            GetTunnel(row + 1, col - 1);
            GetTunnel(row + 1, col + 1);
        }

        private static bool IsOutOfMatrix(int row, int col)
        {
            return row < 0 ||
                   row >= matrix.GetLength(0) ||
                   col < 0 ||
                   col >= matrix.GetLength(1);
        }

        private static void ReadMatrix(in int rows, in int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                string line = Console.ReadLine();

                for (int j = 0; j < line.Length; j++)
                {
                    matrix[i, j] = line[j].ToString();
                }
            }
        }
    }
}
