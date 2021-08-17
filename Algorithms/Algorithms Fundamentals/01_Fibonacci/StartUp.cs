using System;
using System.Collections.Generic;

namespace _01_Fibonacci
{
    class StartUp
    {
        private static long[] memo;

        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            memo = new long[n + 1];

            long result = Fibonacci(n);

            Console.WriteLine(result);
        }

        private static long Fibonacci(int number)
        {
            if (memo[number] > 0)
            {
                return memo[number];
            }

            if (number <= 2)
            {
                return 1;
            }

            memo[number] = Fibonacci(number - 1) + Fibonacci(number - 2);

            return memo[number];
        }
    }
}
