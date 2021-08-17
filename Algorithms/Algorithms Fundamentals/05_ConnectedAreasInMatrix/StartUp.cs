using System;
using System.Collections.Generic;
using System.Linq;

namespace _05_ConnectedAreasInMatrix
{
    class StartUp
    {
        public class Area
        {
            public Area(int size, int row, int col)
            {
                this.Size = size;
                this.Row = row;
                this.Col = col;
            }

            public int Size { get; set; }

            public int Row { get; set; }

            public int Col { get; set; }
        }

        private static string[,] matrix;

        static void Main()
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());

            matrix = new string[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                string line = Console.ReadLine();

                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = line[j].ToString();
                }
            }

            var list = new List<Area>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] == "-")
                    {
                        int size = FindAreaSize(i, j);

                        list.Add(new Area(size, i, j));
                    }
                }
            }

            Console.WriteLine($"Total areas found: {list.Count}");

            var sorted = list
                .OrderByDescending(a => a.Size)
                .ThenBy(a => a.Row)
                .ThenBy(a => a.Col)
                .ToList();

            for (int i = 0; i < sorted.Count; i++)
            {
                var area = sorted[i];
                
                Console.WriteLine($"Area #{i + 1} at ({area.Row}, {area.Col}), size: {area.Size}");
            }

        }

        private static int FindAreaSize(int row, int col)
        {
            if (IsOutOfMatrix(row, col) || matrix[row, col] == "*" || matrix[row, col] == "|")
            {
                return 0;
            }

            matrix[row, col] = "|";

            return 1 +
                   FindAreaSize(row - 1, col) +
                   FindAreaSize(row + 1, col) +
                   FindAreaSize(row, col + 1) +
                   FindAreaSize(row, col - 1);

        }

        private static bool IsOutOfMatrix(int row, int col)
        {
            return row < 0 ||
                   row >= matrix.GetLength(0) ||
                   col < 0 ||
                   col >= matrix.GetLength(1);
        }
    }
}
