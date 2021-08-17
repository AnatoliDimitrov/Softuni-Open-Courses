using System;
using System.Linq;

namespace _01_BinarySearch
{
    class StartUp
    {
        static void Main()
        {
            int[] arr = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            int target = int.Parse(Console.ReadLine());

            Console.WriteLine(BinarySearch(arr, target));
        }

        private static int BinarySearch(int[] arr, int target)
        {
            int leftIndex = 0;
            int rightIndex = arr.Length - 1;

            while (leftIndex <= rightIndex)
            {
                int searchIndex = (leftIndex + rightIndex) / 2;

                if (arr[searchIndex] == target)
                {
                    return searchIndex;
                }
                else if (arr[searchIndex] > target)
                {
                    rightIndex = searchIndex - 1;
                }
                else
                {
                    leftIndex = searchIndex + 1;
                }
            }

            return -1;
        }
    }
}
