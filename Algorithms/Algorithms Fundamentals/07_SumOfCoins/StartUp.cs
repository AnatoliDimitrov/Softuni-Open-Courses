using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;

namespace _07_SumOfCoins
{
    public class Coins
    {
        public Coins(int value, int count)
        {
            this.Value = value;
            this.Count = count;
        }

        public int Value { get; set; }

        public int Count { get; set; }

        public override string ToString()
        {
            return $"{Count} coin(s) with value {Value}";
        }
    }

    class StartUp
    {
        static void Main()
        {
            List<int> coins = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            var sum = int.Parse(Console.ReadLine());

            coins.Sort();

            var result = new List<Coins>();

            while (coins.Count > 0)
            {
                int count = 0;
                int value = coins[coins.Count - 1];

                if (value <= sum)
                {
                    count = sum / value;
                    sum = sum % value;
                    result.Add(new Coins(value, count));
                }

                coins.RemoveAt(coins.Count - 1);
            }

            if (sum != 0)
            {
                Console.WriteLine("Error");
            }
            else
            {
                Console.WriteLine($"Number of coins to take: {result.Sum( v => v.Count)}");
                result.ForEach(Console.WriteLine);
            }
        }
    }
}
