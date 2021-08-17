using System;

namespace Problem0
{
    class StartUp06
    {
        static void Main()
        {
            var first = Console.ReadLine();

            var second = Console.ReadLine();

            int result = FindPairs(first, second);

            if (result < 0)
            {

                Console.WriteLine("The name cannot be transformed!");
            }
            else
            {
                Console.WriteLine($"The minimum operations required to convert \"{first}\" to \"{ second}\" are {result}");
            }
        }

        private static int FindPairs(string A, string B)
        {
            if (A.Length != B.Length)
            {
                return -1;
            }

            int i, j, res = 0;
            int[] count = new int[256];

            for (i = 0; i < A.Length; i++)
            {
                count[A[i]]++;
                count[B[i]]--;
            }

            for (i = 0; i < 256; i++)
            {
                if (count[i] != 0)
                {
                    return -1;
                }
            }

            i = A.Length - 1;
            j = B.Length - 1;

            while (i >= 0)
            {
                if (A[i] != B[j])
                {
                    res++;
                }
                else
                {
                    j--;
                }
                i--;
            }
            return res;
        }
    }
}
