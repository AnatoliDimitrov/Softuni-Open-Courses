using System;
using System.Collections.Generic;
using System.Linq;

namespace _01.RodCutting
{
    class StartUp
    {
        private static int[] bestPrices;
        private static int[] from;

        static void Main()
        {
            var rodPrices = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var length = int.Parse(Console.ReadLine());

            bestPrices = new int[length + 1];
            from = new int[length + 1];

            int bestPrice = FindBestPrice(rodPrices, length);

            Console.WriteLine(bestPrices[length]);

            var list = GetLengths(length);

            Console.WriteLine(string.Join(" ", list));
        }

        private static List<int> GetLengths(int length)
        {
            var result = new List<int>();

            while (length > 0)
            {
                result.Add(from[length]);
                length -= from[length];
            }

            return result;
        }

        private static int FindBestPrice(int[] rodPrices, int length)
        {
            if (length <= 0)
            {
                return 0;
            }

            if (bestPrices[length] > 0)
            {
                return bestPrices[length];
            }

            var bestPrice = rodPrices[length];
            bestPrices[length] = rodPrices[length];
            from[length] = length;

            for (int i = 1; i <= length; i++)
            {
                bestPrice = bestPrices[i] + FindBestPrice(rodPrices, length - i);

                if (bestPrice > bestPrices[length])
                {
                    bestPrices[length] = bestPrice;
                    from[length] = i;
                }
            }

            return bestPrices[length];
        }
    }
}
