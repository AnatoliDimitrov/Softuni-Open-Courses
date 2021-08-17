using System;
using System.Collections.Generic;
using System.Linq;

namespace _08_SetCover
{
    class StartUp
    {
        static void Main()
        {
            HashSet<int> universe = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToHashSet();

            var sets = new List<HashSet<int>>();

            int count = int.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                var set = Console.ReadLine()
                    .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToHashSet();

                sets.Add(set);
            }

            var sorted = sets
                .OrderByDescending(s => s.Count(i => universe.Contains(i)))
                .ToList();

            var result = new List<HashSet<int>>();

            while (universe.Any())
            {
                var currentSet = sets
                    .OrderByDescending(s => s.Count(i => universe.Contains(i)))
                    .FirstOrDefault();

                universe.ExceptWith(currentSet);

                result.Add(currentSet);

                sorted.RemoveAt(0);
            }

            Console.WriteLine($"Sets to take ({result.Count}):");

            foreach (var set in result)
            {
                Console.WriteLine(string.Join(", ",set));
            }
        }
    }
}
