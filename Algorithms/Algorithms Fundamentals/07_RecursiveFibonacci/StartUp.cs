using System;
using System.Collections.Generic;

namespace _07_RecursiveFibonacci
{
    class StartUp
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            long result = CalcFibonacci(new List<int>(), n, 0);

            Console.WriteLine(Math.Abs(result));
        }

        private static long CalcFibonacci(List<int> list, int n, int counter)
        {
            if (counter <= 2)
            {
                list.Add(1);

                CalcFibonacci(list, n, counter + 1);
            }

            else if (counter < n)
            {
                list.Add(list[list.Count - 1] + list[list.Count - 2]);

                CalcFibonacci(list, n, counter + 1);
            }
            else
            {
                return 0;
            }

            return list[list.Count - 1] + list[list.Count - 2];
        }
    }
}