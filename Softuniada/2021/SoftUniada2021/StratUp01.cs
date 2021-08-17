using System;
using System.Linq;
using System.Text;

namespace SoftUniada2021
{
    class StratUp01
    {
        static void Main()
        {
            int stno = int.Parse(Console.ReadLine());
            int enno = int.Parse(Console.ReadLine());

            int total = 0;

            for (int num = stno; num <= enno; num++)
            {
                int ctr = 0;

                for (int i = 2; i <= num / 2; i++)
                {
                    if (num % i == 0)
                    {
                        ctr++;
                        break;
                    }
                }

                if (ctr == 0 && num != 1)
                {
                    Console.Write("{0} ", num);
                    total++;
                }
            }
            Console.WriteLine();
            Console.Write($"The total number of prime numbers between {stno} to {enno} is {total}");
        }
    }
}
