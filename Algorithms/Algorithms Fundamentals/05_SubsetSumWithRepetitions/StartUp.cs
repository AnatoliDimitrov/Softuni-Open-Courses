using System;

namespace _05_SubsetSumWithRepetitions
{
    class StartUp
    {
        static void Main()
        {
            var nums = new int[] { 3, 5, 2 };
            var target = 17;

            var sums = new bool[target + 1];
            sums[0] = true;

            for (int i = 0; i < sums.Length; i++)
            {
                if (sums[i])
                {
                    foreach (var num in nums)
                    {
                        var newSum = num + i;
                        if (newSum < sums.Length)
                        {
                            sums[newSum] = true;
                        }
                    } 
                }
            }

            Console.WriteLine(sums[target]);

            while (target > 0)
            {
                foreach (var num in nums)
                {
                    if (sums[target])
                    {
                        if (target - num < 0)
                        {
                            continue;
                        }
                        Console.WriteLine(num);
                        target -= num;
                    }
                    if (target < 0)
                    {
                        break;
                    }
                }
            }
        }
    }
}
