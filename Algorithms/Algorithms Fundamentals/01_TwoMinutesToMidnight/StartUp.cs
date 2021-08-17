using System;
using System.Numerics;

namespace _01_TwoMinutesToMidnight
{
    class StartUp
    {

        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());

            Console.WriteLine((Factouriel(n) / (Factouriel(n - k) * Factouriel(k))));
        }

        private static BigInteger Factouriel(int n)
        {
            if (n <= 1)
            {
                return 1;
            }

            return n * Factouriel(n - 1);
        }
    }
}
