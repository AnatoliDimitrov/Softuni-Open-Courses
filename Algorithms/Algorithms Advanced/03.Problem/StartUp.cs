using System;
using System.Linq;
using System.Collections.Generic;

namespace _03.Problem
{
    class StartUp
    {
        static void Main()
        {
            var boxesCount = int.Parse(Console.ReadLine());

            var sequence = new string[boxesCount];

            for (int i = 0; i < boxesCount; i++)
            {
                sequence[i] = Console.ReadLine();
            }

            var lengths = new int[sequence.Length];
            var prev = new int[sequence.Length];

            var longest = 0;
            var longestIndex = 0;

            for (int i = 0; i < sequence.Length; i++)
            {
                var demenssions = sequence[i]
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray(); 

                var width = int.Parse(demenssions[0]);
                var depth = int.Parse(demenssions[1]);
                var height = int.Parse(demenssions[2]);

                var bestLen = 1;
                prev[i] = -1;
                for (int j = i - 1; j >= 0; j--)
                {

                    var currentDemenssions = sequence[j]
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .ToArray();

                    var curwidth = int.Parse(currentDemenssions[0]);
                    var curdepth = int.Parse(currentDemenssions[1]);
                    var curheight = int.Parse(currentDemenssions[2]);

                    if ((width > curwidth && depth > curdepth && height > curheight) && lengths[j] + 1 >= bestLen)
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

            foreach (var box in stack)
            {
                    Console.WriteLine(box);
            }
        }
    }
}

