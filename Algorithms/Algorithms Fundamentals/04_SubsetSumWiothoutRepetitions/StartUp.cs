using System;
using System.Collections.Generic;
using System.Linq;

namespace _04_SubsetSumWiothoutRepetitions
{
    class StartUp
    {
        static void Main()
        {
            var nums = new int[] {3, 5, 1, 4, 2};
            var target = 6;

            Dictionary<int, int> result = GetSums(nums, target);

            if (!result.ContainsKey(target))
            {
                Console.WriteLine("Can not generate target");
                return;
            }

            List<int> path = new List<int>();

            while (target > 0)
            {
                path.Add(result[target]);
                target -= result[target];
            }

            Console.WriteLine(string.Join(" ", path));
        }

        private static Dictionary<int, int> GetSums(int[] nums, int target)
        {
            var result = new Dictionary<int, int>();
            result.Add(0, 0);

            foreach (var num in nums)
            {
                var keys = result.Keys.ToArray();
                foreach (var key in keys)
                {
                    var newSum = key + num;
                    if (!result.ContainsKey(newSum))
                    {
                        result[newSum] = num;
                    }
                }
            }

            return result;
        }
    }
}
