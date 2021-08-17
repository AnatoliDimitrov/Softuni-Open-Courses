using System;
using System.Collections.Generic;

namespace Problem05
{
    class StartUp05
    {
        static void printAllSubsetsRec(int[] arr, int n,
                                    List<int> v, int sum)
        {
            if (sum == 0)
            {
                for (int i = 0; i < v.Count; i++)
                    Console.Write(v[i] + " ");
                Console.WriteLine();
                return;
            }

            if (n == 0)
            { 
                return;
            }

            printAllSubsetsRec(arr, n - 1, v, sum);
            List<int> v1 = new List<int>(v);
            v1.Add(arr[n - 1]);
            printAllSubsetsRec(arr, n - 1, v1, sum - arr[n - 1]);
        }

        static void printAllSubsets(int[] arr, int n, int sum)
        {
            List<int> v = new List<int>();
            printAllSubsetsRec(arr, n, v, sum);
        }
        public static void Main()
        {
            int[] arr = { 2, 5, 8, 4, 6, 11 };
            int sum = 13;
            int n = arr.Length;
            printAllSubsets(arr, n, sum);
        }
    }
}
