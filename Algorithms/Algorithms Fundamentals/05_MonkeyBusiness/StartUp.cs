using System;
using System.Linq;

namespace _05_MonkeyBusiness
{
    class StartUp
    {
        private static string[] elements;
        private static int count;
        private static int[] result;
        private static int[] numbers;

        static void Main()
        {
            count = int.Parse(Console.ReadLine());

            result = new int[count];

            numbers = new int[count];

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = i + 1;
            }

            GenerateCombinations(0);
        }

        private static void GenerateCombinations(int index)
        {

            if (index == result.Length)
            {
                if (result.Sum() == 0)
                {
                    for (int i = 0; i < result.Length; i++)
                    {
                        if (result[i] > 0)
                        {
                            Console.Write("+" + result[i] + " ");
                        }
                        else
                        {
                            Console.Write(result[i] + " ");
                        }
                    }

                    Console.WriteLine();
                }
                return;
            }

                result[index] = numbers[index];
                GenerateCombinations(index + 1);
                result[index] = 0 - numbers[index];
                GenerateCombinations(index + 1);
        }
    }
}
