using System;
using System.Collections.Generic;
using System.Linq;

namespace _01.CableMerchant
{
    class StartUp
    {
        private static int[] bestPrices;
        private static List<int> prices = new List<int>();

        static void Main()
        { 
            prices.Add(0);

            prices.AddRange(Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList());

            bestPrices = new int[prices.Count];

            var connectorPrice = int.Parse(Console.ReadLine());

            for (int i = 1; i < prices.Count; i++)
            {
                var bestPrice = FindBestPrices(connectorPrice, i);

                Console.Write($"{bestPrice} ");
            }

        }

        private static int FindBestPrices(int connectorPrice, int length)
        {
            if (length <= 0)
            {
                return 0;
            }

            if (bestPrices[length] > 0)
            {
                return bestPrices[length];
            }

            var bestPrice = prices[length];
            bestPrices[length] = prices[length];

            for (int i = 1; i <= length; i++)
            {
                bestPrice = (bestPrices[i] + FindBestPrices(connectorPrice, length - i)) - 2 * connectorPrice;

                if (bestPrice > bestPrices[length])
                {
                    bestPrices[length] = bestPrice;
                }
            }

            return bestPrices[length];
        }
    }
}