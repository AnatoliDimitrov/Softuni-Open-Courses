namespace _1.Problem
{
    using System;
    class DreamCar
    {
        static void Main()
        {
            decimal check = decimal.Parse(Console.ReadLine());
            decimal expences = decimal.Parse(Console.ReadLine());
            decimal increase = decimal.Parse(Console.ReadLine());
            decimal carPrice = decimal.Parse(Console.ReadLine());
            decimal months = decimal.Parse(Console.ReadLine());

            decimal savings = 0;
            decimal totalExpenses = expences * months;

            for (int i = 0; i < months; i++)
            {
                savings += check;
                check += increase;
            }
            savings -= totalExpenses;
            if (savings >= carPrice)
            {
                Console.WriteLine("Have a nice ride!");
            }
            else
            {
                Console.WriteLine("Work harder!");
            }
        }
    }
}
