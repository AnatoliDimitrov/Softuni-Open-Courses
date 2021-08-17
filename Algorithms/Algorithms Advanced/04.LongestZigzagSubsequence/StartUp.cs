using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace _04.LongestZigzagSubsequence
{
    class StartUp
    {
        private static int[,] matrix;
        private static int[,] parent;
        private static int longestSubSequence;
        private static int rowIndex;
        private static int colIndex;
        static void Main()
        {
            var s = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            matrix = new int[2, s.Length];
            matrix[0, 0] = 1;
            matrix[1, 0] = 1;

            parent = new int[2, s.Length];
            parent[0, 0] = -1;
            parent[1, 0] = -1;

            GetLongestSubsSequence(s);
            //Console.WriteLine(longestSubSequence);

            //for (int i = 0; i < matrix.GetLength(0); i++)
            //{
            //    for (int j = 0; j < matrix.GetLength(1); j++)
            //    {
            //        Console.Write(matrix[i, j] + " ");
            //    }

            //    Console.WriteLine();
            //}

            //Console.WriteLine();

            //for (int i = 0; i < matrix.GetLength(0); i++)
            //{
            //    for (int j = 0; j < matrix.GetLength(1); j++)
            //    {
            //        Console.Write(parent[i, j] + " ");
            //    }

            //    Console.WriteLine();
            //}

            var result = GetSubsSequence(s);
            Console.WriteLine(string.Join(" ", result));
        }

        private static Stack<int> GetSubsSequence(int[] s)
        {
            var stack = new Stack<int>();

            while (colIndex != -1)
            {
                stack.Push(s[colIndex]);

                colIndex = parent[rowIndex, colIndex];

                rowIndex = Math.Abs(rowIndex - 1);
            }

            return stack;
        }

        private static void GetLongestSubsSequence(int[] s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                var current = s[i];

                for (int j = i - 1; j >= 0; j--)
                {
                    var prev = s[j];

                    if (current > prev && matrix[1, j] + 1 >= matrix[0, i])
                    {
                        matrix[0, i] = matrix[1, j] + 1;
                        parent[0, i] = j;
                    }

                    if (prev > current && matrix[0, j] + 1 >= matrix[1, i])
                    {
                        matrix[1, i] = matrix[0, j] + 1;
                        parent[1, i] = j;
                    }
                }

                if (matrix[0, i] > longestSubSequence)
                {
                    longestSubSequence = matrix[0, i];
                    rowIndex = 0;
                    colIndex = i;
                }

                if (matrix[1, i] > longestSubSequence)
                {
                    longestSubSequence = matrix[1, i];
                    rowIndex = 1;
                    colIndex = i;
                }
            }
        }
    }
}
