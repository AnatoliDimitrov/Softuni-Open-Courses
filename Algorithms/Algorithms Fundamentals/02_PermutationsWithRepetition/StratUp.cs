using System;
using System.Collections.Generic;

namespace _02_PermutationsWithRepetition
{
    class StartUp
    {
        private static string[] elements;
        static void Main()
        {
            elements = //new[] {"A", "B", "B"};
                       Console.ReadLine().Split();

            CreatePermutations(0);
        }

        private static void CreatePermutations(int index)
        {
            if (index >= elements.Length)
            {
                Console.WriteLine(string.Join(" ", elements));
                return;
            }

            CreatePermutations(index + 1);

            var swapped = new HashSet<string>(){elements[index]};

            for (int i = index; i < elements.Length; i++)
            {
                if (!swapped.Contains(elements[i]))
                {
                    Swap(index, i);
                    CreatePermutations(index + 1);
                    Swap(index, i);
                    swapped.Add(elements[i]);
                }
            }
        }

        private static void Swap(int first, int second)
        {
            var temp = elements[first];
            elements[first] = elements[second];
            elements[second] = temp;
        }
    }
}
