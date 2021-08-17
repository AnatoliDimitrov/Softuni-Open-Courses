using System;
using System.Linq;

namespace _04_InsertionSort
{
    class StartUp
    {
        static void Main()
        {
            int[] arr = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            InsertionSort(arr);

            Console.WriteLine(string.Join(" ", arr));
        }

        private static void InsertionSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                var currentIndex = i;

                while (currentIndex > 0 && arr[currentIndex] < arr[currentIndex - 1])
                {
                    Swap(arr, currentIndex, currentIndex - 1);
                    currentIndex--;
                }
            }
        }

        private static void Swap(int[] arr, int first, int second)
        {
            var temp = arr[first];
            arr[first] = arr[second];
            arr[second] = temp;
        }
    }
}
