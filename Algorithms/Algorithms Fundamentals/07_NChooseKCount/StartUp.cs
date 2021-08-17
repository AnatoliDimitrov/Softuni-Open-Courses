using System;

namespace _07_NChooseKCount
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int row = int.Parse(Console.ReadLine());
            int col = int.Parse(Console.ReadLine());

            Console.WriteLine(Calculate(row, col));
            
        }

        private static int Calculate(int row, int col)
        {
            if (col == 0 || col == row || row == 0 || row == 1)
            {
                return 1;
            }

            return Calculate(row - 1, col - 1) + Calculate(row - 1, col);
        }
    }
}
