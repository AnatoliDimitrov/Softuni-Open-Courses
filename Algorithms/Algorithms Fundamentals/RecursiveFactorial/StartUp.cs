using System;

namespace RecursiveFactorial
{
    class StartUp
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            long result = CalcFactorial(n);

            Console.WriteLine(result);
        }

        private static long CalcFactorial(int n)
        {
            if (n == 1)
            {
                return 1;
            }

            return n * CalcFactorial(n - 1);
        }
    }
}
