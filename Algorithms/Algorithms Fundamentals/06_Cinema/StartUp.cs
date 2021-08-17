using System;
using System.Collections.Generic;
using System.Linq;

namespace _06_Cinema
{
    class StartUp
    {
        private static bool[] taken;

        static void Main()
        {
            List<string> friends = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList();

            var result = new string[friends.Count];
            taken = new bool[friends.Count];

            string input = "";

            while ((input = Console.ReadLine()) != "generate")
            {
                string[] data = input.Split(" - ");

                string name = data[0];
                int place = int.Parse(data[1]) - 1;

                taken[place] = true;
                result[place] = name;

                friends.Remove(name);
            }

            GeneratePermutations(friends, result, 0);
        }

        private static void GeneratePermutations(List<string> friends, string[] result, int index)
        {
            if (index >= friends.Count)
            {
                int counter = 0;

                for (int i = 0; i < result.Length; i++)
                {
                    if (!taken[i])
                    {
                        result[i] = friends[counter];
                        counter++;
                    }
                }

                Console.WriteLine(string.Join(" ", result));
                return;
            }

            GeneratePermutations(friends, result, index + 1);

            var swapped = new HashSet<string>() { friends[index] };

            for (int i = index; i < friends.Count; i++)
            {
                if (!swapped.Contains(friends[i]))
                {
                    Swap(friends, index, i);
                    GeneratePermutations(friends, result, index + 1);
                    Swap(friends, index, i);
                    swapped.Add(friends[i]);
                }
            }
        }

        private static void Swap(List<string> friends, int first, int second)
        {
            var temp = friends[first];
            friends[first] = friends[second];
            friends[second] = temp;
        }
    }
}
