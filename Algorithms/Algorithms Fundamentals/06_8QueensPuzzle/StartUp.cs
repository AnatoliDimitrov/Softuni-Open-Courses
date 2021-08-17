using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace _06_8QueensPuzzle
{
    class StartUp
    {
        private static HashSet<int> rows = new HashSet<int>();
        private static HashSet<int> cols = new HashSet<int>();
        private static HashSet<int> leftDiagonals = new HashSet<int>();
        private static HashSet<int> rightDiagonals = new HashSet<int>();
        static void Main()
        {
            bool[,] board = new bool[8, 8];

            АrrangeQueens(board, 0);
        }

        private static void АrrangeQueens(bool[,] board, int row)
        {
            if (row == board.GetLength(0))
            {
                Print(board);
                return;
            }

            for (int i = 0; i < board.GetLength(1); i++)
            {
                if (CanNotPlace(row, i))
                {
                    continue;
                }

                board[row, i] = true;

                rows.Add(row);
                cols.Add(i);
                leftDiagonals.Add((row - i));
                rightDiagonals.Add(row + i);

                АrrangeQueens(board, row + 1);

                board[row, i] = false;

                rows.Remove(row);
                cols.Remove(i);
                leftDiagonals.Remove((row - i));
                rightDiagonals.Remove(row + i);
            }
        }

        private static bool CanNotPlace(in int row, in int col)
        {
            return rows.Contains(row) ||
                   cols.Contains(col) ||
                   leftDiagonals.Contains((row - col)) ||
                   rightDiagonals.Contains((row + col));
        }

        private static void Print(bool[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == false)
                    {
                        Console.Write("-" + " ");
                    }
                    else
                    {
                        Console.Write("*" + " ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
