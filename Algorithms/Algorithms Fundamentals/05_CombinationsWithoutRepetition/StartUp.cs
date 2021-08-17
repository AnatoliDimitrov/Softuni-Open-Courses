using System;

namespace _05_CombinationsWithoutRepetition
{
    class StartUp
    {
        private static string[] elements;
        private static int count;
        private static string[] result;

        static void Main()
        {
            elements = Console.ReadLine().Split();

            count = int.Parse(Console.ReadLine());

            result = new string[count];

            GenerateCombinations(0, 0);
        }

        private static void GenerateCombinations(int index, int iter)
        {

            if (index == result.Length)
            {
                Console.WriteLine(string.Join(" ", result));
                return;
            }

            for (int i = iter; i < elements.Length; i++)
            {
                result[index] = elements[i];
                GenerateCombinations(index + 1, i + 1);
            }
        }
    }
}
