using System;
using System.Linq;

namespace RecursionAndBacktracking
{
    public class StartUp
    {
        static void Main()
        {
            int[] input = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            long result = GetArraySum(input, 0);

            Console.WriteLine(result);
        }

        private static long GetArraySum(int[] array, int index)
        {
            if (index == array.Length)
            {
                return 0;
            }

            return array[index] + GetArraySum(array, index + 1);
        }
    }
}
