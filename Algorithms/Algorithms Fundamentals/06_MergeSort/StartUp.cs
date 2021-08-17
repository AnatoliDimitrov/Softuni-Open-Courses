using System;
using System.Linq;

namespace _06_MergeSort
{
    class StartUp
    {
        static void Main()
        {
            int[] arr = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var sorted = MergeSort(arr);

            Console.WriteLine(string.Join(" ", sorted));
        }

        private static int[] MergeSort(int[] arr)
        {
            if (arr.Length == 1)
            {
               return arr;
            }

            return Merge(MergeSort(arr.Take(arr.Length / 2).ToArray()), MergeSort(arr.Skip(arr.Length / 2).ToArray()));
        }

        private static int[] Merge(int[] first, int[] second)
        {
            int[] current = new int[first.Length + second.Length];

            int currentIndex = 0;
            int firstIndex = 0;
            int secondIndex = 0;

            while (firstIndex < first.Length && secondIndex < second.Length)
            {
                if (first[firstIndex] < second[secondIndex])
                {
                    current[currentIndex] = first[firstIndex];
                    firstIndex++;
                }
                else
                {
                    current[currentIndex] = second[secondIndex];
                    secondIndex++;
                }

                currentIndex++;
            }

            while (firstIndex < first.Length)
            {
                current[currentIndex] = first[firstIndex];
                firstIndex++;
                currentIndex++;
            }

            while (secondIndex < second.Length)
            {
                current[currentIndex] = second[secondIndex];
                secondIndex++;
                currentIndex++;
            }

            return current;
        }
    }
}
