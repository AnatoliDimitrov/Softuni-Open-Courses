using System;
using System.Linq;

namespace _11_ConnectingCables
{
    class StartUp
    {
        private static int[,] matrix;
        static void Main()
        {
            var a = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var b = a
                .OrderBy(x => x)
                .ToArray();

            matrix = new int[a.Length + 1, b.Length + 1];

            int count = GetCount(a, b);

            Console.WriteLine($"Maximum pairs connected: {count}");
        }

        private static int GetCount(int[] a, int[] b)
        {

            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    if (a[i - 1] == b[j - 1])
                    {
                        matrix[i, j] = matrix[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        matrix[i, j] = Math.Max(matrix[i - 1, j], matrix[i, j - 1]);
                    }
                }
            }

            return matrix[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
        }
    }
}
