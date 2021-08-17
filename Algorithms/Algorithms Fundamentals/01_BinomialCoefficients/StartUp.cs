using System;
using System.Collections.Generic;

namespace _01_BinomialCoefficients
{
    class StartUp
    {
        private static Dictionary<string, long> memo = new Dictionary<string, long>(); 
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());

            long result = GetBinom(n, k);

            Console.WriteLine(result);
        }

        private static long GetBinom(int row, int col)
        {
            var str = $"{row}|{col}";
            if (memo.ContainsKey(str))
            {
                return memo[str];
            }
            if (row <= 1 || col == row || col < 1)
            {
                return 1;
            }

            var current =  GetBinom(row - 1, col - 1) + GetBinom(row - 1, col);

            memo.Add(str, current);

            return current;
        }
    }
}
