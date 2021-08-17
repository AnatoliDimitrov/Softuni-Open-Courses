using System;

namespace _04_VariationsWithRepetition
{
    class StartUp
    {
        private static string[] elements;
        private static string[] result;
        private static int count;

        static void Main()
        {
            elements = Console.ReadLine().Split();

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
                result[index] = elements[i];
                GenerateVariations(index + 1);
            }
        }
    }
}
