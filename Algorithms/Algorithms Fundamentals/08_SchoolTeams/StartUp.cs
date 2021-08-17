using System;
using System.Collections.Generic;
using System.Linq;

namespace _08_SchoolTeams
{
    class StartUp
    {
        private static string[] girls;
        private static string[] boys;

        static void Main()
        {
            girls = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries);
            boys = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries);

            GenerateCombinations(new List<string>(), 0, 0);
        }

        private static void GenerateCombinations(List<string> result, int index, int iterator)
        {
            if (index >= 3)
            {
                GenerateCombinationsWithBoys(result, new List<string>(), 0, 0);
                return;
            }

            for (int i = iterator; i < girls.Length; i++)
            {
                result.Add(girls[i]);
                GenerateCombinations(result, index + 1, i + 1);
                result.Remove(girls[i]);
            }
        }

        private static void GenerateCombinationsWithBoys(List<string> girlsResult, List<string> boysResult ,int index, int iterator)
        {
            if (index >= 2)
            {
                Console.WriteLine(string.Join(", ", girlsResult) + ", " + string.Join(", ", boysResult));
                return;
            }

            for (int i = iterator; i < boys.Length; i++)
            {
                boysResult.Add(boys[i]);
                GenerateCombinationsWithBoys(girlsResult, boysResult, index + 1, i + 1);
                boysResult.Remove(boys[i]);
            }
        }
    }
}
