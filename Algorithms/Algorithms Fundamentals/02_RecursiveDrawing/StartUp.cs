using System;

namespace _02_RecursiveDrawing
{
    public class StartUp
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            Draw(n);
        }

        private static void Draw(int n)
        {
            if (n == 0)
            {
                return;
            }

            Console.WriteLine(new string('*', n));

            Draw(n - 1);

            Console.WriteLine(new string('#', n));
        }
    }
}
