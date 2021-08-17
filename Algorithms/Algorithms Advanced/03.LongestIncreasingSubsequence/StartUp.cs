using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.LongestIncreasingSubsequence
{
    class StartUp
    {
        static void Main()
        {
            var s = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var lengths = new int[s.Length];
            var prev = new int[s.Length];
            var longest = 0;
            var lastIndex = 0;

            for (int i = 0; i < s.Length; i++)
            {
                var bestLength = 1;
                prev[i] = -1;

                for (int j = i - 1; j >= 0; j--)
                {
                    if (s[i] > s[j] && bestLength <= lengths[j] + 1)
                    {
                        bestLength = lengths[j] + 1;
                        prev[i] = j;
                    }
                }

                if (bestLength > longest)
                {
                    longest = bestLength;
                    lastIndex = i;
                }

                lengths[i] = bestLength;
            }

            var lis = GetLis(prev, s, lastIndex);
            Console.WriteLine(string.Join(" ", lis));
        }

        private static Stack<int> GetLis(int[] prev, int[] s, int node)
        {
            var stack = new Stack<int>();

            while (node != -1)
            {
                stack.Push(s[node]);
                node = prev[node];
            }

            return stack;
        }
    }
}
