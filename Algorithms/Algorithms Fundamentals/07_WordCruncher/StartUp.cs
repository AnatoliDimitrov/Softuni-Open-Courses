using System;
using System.Collections.Generic;

namespace _07_WordCruncher
{
    class StartUp
    {
        private static string[] words;
        private static string target;
        private static Dictionary<int, List<string>> indexes = new Dictionary<int, List<string>>();
        private static Dictionary<string, int> counters = new Dictionary<string, int>();
        private static SortedSet<string> result = new SortedSet<string>();

        static void Main()
        {
            words = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries);
            target = Console.ReadLine();

            int index = 0;

            while (index < target.Length)
            {
                foreach (string word in words)
                {
                    if (word[0] == target[index])
                    {
                        if (!indexes.ContainsKey(index))
                        {
                            indexes[index] = new List<string>();
                        }

                        indexes[index].Add(word);
                    }
                }

                index++;
            }

            foreach (var word in words)
            {
                if (!counters.ContainsKey(word))
                {
                    counters[word] = 0;
                }

                counters[word]++;
            }

            GenerateCombinations(new List<string>(), 0);

            foreach (var word in result)
            {
                Console.WriteLine(word);
            }
        }

        private static void GenerateCombinations(List<string> tempResult, int index)
        {
            if (string.Join("", tempResult) == target)
            {
                result.Add(string.Join(" ", tempResult));
                return;
            }

            if (indexes.ContainsKey(index))
            {
                foreach (var word in indexes[index])
                {
                    if (counters[word] > 0)
                    {
                        tempResult.Add(word);
                        counters[word]--;
                        GenerateCombinations(tempResult, index + word.Length);
                        counters[word]++;
                        tempResult.RemoveAt(tempResult.Count - 1);
                    }
                }
            }
        }
    }
}
