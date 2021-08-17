using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.LongestStringChain
{
    class StartUp
    {
        static void Main()
        {
            var sequence = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            var lengths = new int[sequence.Length];
            var prev = new int[sequence.Length];

            var longest = 0;
            var longestIndex = 0;

            for (int i = 0; i < sequence.Length; i++)
            {
                var length = sequence[i].Length;
                var bestLen = 1;
                prev[i] = -1;
                for (int j = i - 1; j >= 0; j--)
                {
                    var currentLength = sequence[j].Length;
                    if (length > currentLength && lengths[j] + 1 >= bestLen)
                    {
                        bestLen = lengths[j] + 1;
                        prev[i] = j;
                    }
                }

                if (bestLen > longest)
                {
                    longest = bestLen;
                    longestIndex = i;
                }

                lengths[i] = bestLen;
            }

            var stack = new Stack<string>();

            while (longestIndex != -1)
            {
                stack.Push(sequence[longestIndex]);
                longestIndex = prev[longestIndex];
            }

            Console.WriteLine(string.Join(" ", stack));
        }
    }
}
