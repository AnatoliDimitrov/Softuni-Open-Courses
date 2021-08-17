using System;
using System.Linq;
using System.Text;

namespace _02_HiddenValues
{
    class StartUp
    {
        private static StringBuilder result = new StringBuilder();
        private static StringBuilder sub = new StringBuilder();

        public static void Main()
        {
            int[] a = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            int max_so_far = int.MinValue,
                max_ending_here = 0;



            for (int i = 0; i < a.Length; i++)
            {
                max_ending_here = max_ending_here + a[i];
                sub.Append(a[i] + " ");

                if (max_so_far < max_ending_here)
                {
                    result = new StringBuilder();
                    result.Append(sub);
                    max_so_far = max_ending_here;
                }

                if (max_ending_here < 0)
                {
                    sub.Clear();
                    max_ending_here = 0;
                }
            }

            Console.WriteLine(max_so_far);
            Console.WriteLine(result);
        }
    }
}
