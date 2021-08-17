using System;
using System.Collections.Generic;

namespace _05_PathsInLabyrinth
{
    class StartUp
    {
        private static char[,] matrix;

        static void Main()
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());

            matrix = new char[rows,cols];

            for (int i = 0; i < rows; i++)
            {
                char[] temp = Console.ReadLine().ToCharArray();

                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = temp[j];
                }
            }

            FindPaths(0, 0, '\0', "");
        }

        private static void FindPaths(int row, int col, char c, string path)
        {

            path += c;

            if (IsOut(matrix, row, col))
            {
                path.Remove(path.Length - 1);
                return;
            }

            if (IsWallOrVisited(matrix, row, col))
            {
                path.Remove(path.Length - 1);
                return;
            }

            if (IsExit(matrix, row, col))
            {
                Console.WriteLine(path);
                path.Remove(path.Length - 1);
                return;
            }

            matrix[row, col] = 'v';

            FindPaths(row + 1, col, 'D', path);
            FindPaths(row, col + 1, 'R', path);
            FindPaths(row - 1, col, 'U', path);
            FindPaths(row, col - 1, 'L', path);

            matrix[row, col] = '-';
        }

        private static bool IsExit(char[,] matrix, in int row, in int col)
        {
            return matrix[row, col] == 'e';
        }

        private static bool IsWallOrVisited(char[,] matrix, in int row, in int col)
        {
            return matrix[row, col] == '*' || matrix[row, col] == 'v';
        }

        private static bool IsOut(char[,] matrix, in int row, in int col)
        {
            return row < 0 || row >= matrix.GetLength(0) ||
                   col < 0 || col >= matrix.GetLength(1);
        }
    }
}
