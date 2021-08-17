using System;
using System.Threading.Channels;

namespace _02_NestedLoops
{
    class StartUp
    {
        private static int n;
        private static int[] result;
        static void Main()
        {
            n = int.Parse(Console.ReadLine());

            result = new int[n];

            Permutations(1);
        }

        private static void Permutations(int count)
        {
            if (count > n)
            {
                Console.WriteLine(string.Join(" ", result));
                return;
            }

            for (int i = 1; i <= n; i++)
            {
                result[count - 1] = i;
                Permutations(count + 1);
            }
        }
    }
}
