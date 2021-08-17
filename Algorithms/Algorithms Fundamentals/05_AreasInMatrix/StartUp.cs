using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading;

namespace _05_AreasInMatrix
{
    public class Child
    {
        public Child(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public int Row { get; set; }

        public int Col { get; set; }
    }


    class StartUp
    {
        private static string[,] matrix;
        private static bool[,] visited;

        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());

            visited = new bool[n, m];
            matrix = ReadMatrix(n, m);

            Dictionary<string, int> areas = new Dictionary<string, int>();

            var aresCount = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {

                    if (!visited[i, j])
                    {
                        Dfs(i, j);

                        var element = matrix[i, j];
                        if (!areas.ContainsKey(element))
                        {
                            areas[element] = 0;
                        }

                        areas[element]++;
                        aresCount++;
                    }
                }
            }

            var sorted = areas
                .OrderBy(k => k.Key);

            Console.WriteLine($"Areas: {aresCount}");

            foreach (var kvp in sorted)
            {
                Console.WriteLine($"Letter '{kvp.Key}' -> {kvp.Value}");
            }
        }

        private static void Dfs(int row, int col)
        {
            visited[row, col] = true;

            List<Child> children = FindAllValidChildren(row, col);

            foreach (var child in children)
            {
                Dfs(child.Row, child.Col);
            }
        }

        private static List<Child> FindAllValidChildren(int row, int col)
        {
            var list = new List<Child>();

            if (IsInMatrix(row + 1, col) && matrix[row, col] == matrix[row + 1, col] && !visited[row + 1, col])
            {
                list.Add(new Child(row + 1, col));
            }

            if (IsInMatrix(row - 1, col) && matrix[row, col] == matrix[row - 1, col] && !visited[row - 1, col])
            {
                list.Add(new Child(row - 1, col));
            }

            if (IsInMatrix(row, col + 1) && matrix[row, col] == matrix[row, col + 1] && !visited[row, col + 1])
            {
                list.Add(new Child(row, col + 1));
            }

            if (IsInMatrix(row, col - 1) && matrix[row, col] == matrix[row, col - 1] && !visited[row, col - 1])
            {
                list.Add(new Child(row, col - 1));
            }

            return list;
        }

        private static bool IsInMatrix(int row, int col)
        {
            return row >= 0 &&
                   row < matrix.GetLength(0) &&
                   col >= 0 &&
                   col < matrix.GetLength(1);
        }

        private static string[,] ReadMatrix(int n, int m)
        {
            var result = new string[n, m];

            for (int i = 0; i < n; i++)
            {
                string line = Console.ReadLine();

                for (int j = 0; j < m; j++)
                {
                    result[i, j] = line[j].ToString();
                }
            }

            return result;
        }
    }
}
