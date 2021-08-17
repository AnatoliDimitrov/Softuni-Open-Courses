using System;
using System.Linq;

namespace _03_BubbleSort
{
    class StartUp
    {
        static void Main()
        {
            int[] arr = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            BubbleSort(arr);

            Console.WriteLine(string.Join(" ", arr));
        }

        private static void BubbleSort(int[] arr)
        {
            bool IsSorted = false;
            int firstIndex = 0;

            while (!IsSorted)
            {
                IsSorted = true;

                for (int i = firstIndex + 1; i < arr.Length; i++)
                {
                    if (arr[i - 1] > arr[i])
                    {
                        Swap(arr, i - 1, i);
                        IsSorted = false;
                    }
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
