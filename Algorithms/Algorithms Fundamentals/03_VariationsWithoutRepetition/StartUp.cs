using System;

namespace _03_VariationsWithoutRepetition
{
    class StartUp
    {
        private static string[] elements;
        private static bool[] used;
        private static string[] result;
        private static int count;

        static void Main()
        {
            elements = Console.ReadLine().Split();

            used = new bool[elements.Length];

            count = int.Parse(Console.ReadLine());

            result = new string[count];

            GenerateVariations(0);
        }

        private static void GenerateVariations(int index)
        {
            if (index >= result.Length)
            {
                Console.WriteLine(string.Join(" ", result));
                return;
            }

            for (int i = 0; i < elements.Length; i++)
            {
                if (!used[i])
                {
                    used[i] = true;
                    result[index] = elements[i];
                    GenerateVariations(index + 1);
                    used[i] = false;
                }
            }
        }
    }
}
