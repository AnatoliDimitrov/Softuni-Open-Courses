using System;
using System.Linq;
using System.Text;

namespace Problem03
{
    class StartUp03
    {
        static void Main()
        {
            string name = "";

            var total = 0;

            while ((name = Console.ReadLine()) != "stop")
            {
                int[] tasks = Console.ReadLine()
                    .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                int sum = 0;

                for (int i = 0; i < tasks.Length; i++)
                {
                    int currentSum = 1;

                    for (int j = 0; j < tasks.Length; j++)
                    {
                        if (j==i)
                        {
                            continue;
                        }

                        currentSum *= tasks[j];
                    }

                    sum += currentSum;
                }

                total += sum;

                Console.WriteLine($"{name} has a bonus of {sum} lv.");
            }

            Console.WriteLine($"The amount of all bonuses is {total} lv.");
        }
    }
}
