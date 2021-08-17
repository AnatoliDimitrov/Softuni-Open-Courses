namespace _4.Problem
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System.Numerics;
    class NewConis
    {
        static void Main()
        {
            BigInteger p = BigInteger.Parse(Console.ReadLine());
            int coins = 0;
            while (p > 0)
            {
                if (p > 25000000)
                {
                    p -= 25000000;
                    coins++;
                    continue;
                }
                else if (p > 10000000)
                {
                    p -= 10000000;
                    coins++;
                    continue;
                }
                else if (p > 1000000)
                {
                    p -= 1000000;
                    coins++;
                    continue;
                }
                else if (p > 250000)
                {
                    p -= 250000;
                    coins++;
                    continue;
                }
                else if (p > 100000)
                {
                    p -= 100000;
                    coins++;
                    continue;
                }
                else if (p > 10000)
                {
                    p -= 10000;
                    coins++;
                    continue;
                }
                else if (p > 2500)
                {
                    p -= 2500;
                    coins++;
                    continue;
                }
                else if (p > 1000)
                {
                    p -= 1000;
                    coins++;
                    continue;
                }
                else if (p > 100)
                {
                    p -= 100;
                    coins++;
                    continue;
                }
                else if (p > 25)
                {
                    p -= 25;
                    coins++;
                    continue;
                }
                else if (p > 10)
                {
                    p -= 10;
                    coins++;
                    continue;
                }
                else if (p >= 1)
                {
                    p -= 1;
                    coins++;
                    continue;
                }
            }
            Console.WriteLine(coins);
        }
    }
}
