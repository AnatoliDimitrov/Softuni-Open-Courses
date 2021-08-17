using System;
using System.Collections.Generic;
using System.Linq;

namespace _07_DividingPresents
{
    class Program
    {
        private static Dictionary<int, int> memo = new Dictionary<int, int>();

        static void Main()
        {
            var presents = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            GetSums(presents);

            var middle = (int)Math.Ceiling(presents.Sum() / 2.0);

            var bobPoints = -1;

            while (bobPoints < 0)
            {
                if (memo.ContainsKey(middle))
                {
                    bobPoints = middle;
                    break;
                }

                middle++;
            }

            var alanPoints = presents.Sum() - bobPoints;

            var alanPresents = new List<int>();

            Console.WriteLine($"Difference: {bobPoints - alanPoints}");
            Console.WriteLine($"Alan:{alanPoints} Bob:{bobPoints}");

            while (alanPoints > 0)
            {
                alanPresents.Add(alanPoints - memo[alanPoints]);
                alanPoints = memo[alanPoints];
            }

            Console.WriteLine($"Alan takes: {string.Join(" ", alanPresents)}");
            Console.WriteLine("Bob takes the rest.");
        }

        private static void GetSums(int[] presents)
        {
            memo.Add(0, 0);

            foreach (var present in presents)
            {
                var keys = memo.Keys.ToArray();
                foreach (var key in keys)
                {
                    var sum = key + present;
                    if (!memo.ContainsKey(sum))
                    {
                        memo.Add(sum, key);
                    }
                }
            }
        }
    }
}
