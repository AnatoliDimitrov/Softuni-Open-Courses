using System;
using System.Linq;
using System.Text;

namespace Problem02
{
    class StartUp02
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            for (int i = n; i > 0; i--)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write(j);
                }

                for (int k = i - 1; k > 0; k--)
                {
                    Console.Write(k);
                }

                Console.WriteLine();
            }
        }
    }
}
