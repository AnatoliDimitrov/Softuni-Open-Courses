using System;

namespace _03_CombinationsWithRepetition
{
    class StartUp
    {
        private static int n;
        private static int holders;
        private static int[] result;
        static void Main()
        {
            n = int.Parse(Console.ReadLine());

            holders = int.Parse(Console.ReadLine());

            result = new int[holders];

            Permutations(1, 1);
        }

        private static void Permutations(int count, int iter)
        {
            if (count > holders)
            {
                Console.WriteLine(string.Join(" ", result));
                return;
            }

            for (int i = iter; i <= n; i++)
            {
                result[count - 1] = i;
                Permutations(count + 1, i);
            }
        }
    }
}
