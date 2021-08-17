using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace _02_PermutationsWithRepetition
{
    class StartUp
    {
        private static string[] elements;
        static void Main()
        {
            elements = Console.ReadLine().Split();

            CreatePermutations(0);
        }

        private static void CreatePermutations(int index)
        {
            if (index >= elements.Length)
            {
                Console.WriteLine(string.Join(" ", elements));
                return;
            }

            for (int i = index; i < elements.Length; i++)
            {
                Swap(index, i);
                CreatePermutations(index + 1);
                Swap(index, i);
            }
        }

        private static void Swap(int first, int second)
        {
           var temp =  elements[first];
           elements[first] = elements[second];
           elements[second] = temp;
        }
    }
}