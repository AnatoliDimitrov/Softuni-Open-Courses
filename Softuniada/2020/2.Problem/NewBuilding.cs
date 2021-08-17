namespace _2.Problem
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    class NewBuilding
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            int bigcounter = 0;
            int counter = 0;
            for (int i = 0; i < n + n / 2; i++)
            {
                counter = bigcounter;
                for (int j = 0; j < n; j++)
                {
                    if (counter == 0)
                    {
                        Console.Write("#");
                        counter++;
                    }
                    else
                    {
                        Console.Write(".");
                        counter++;
                    }
                    if (counter == 4)
                    {
                        counter = 0;
                    }
                }
                bigcounter++;
                if (bigcounter == 4)
                {
                    bigcounter = 0;
                }
                Console.WriteLine();
            }
        }
    }
}
