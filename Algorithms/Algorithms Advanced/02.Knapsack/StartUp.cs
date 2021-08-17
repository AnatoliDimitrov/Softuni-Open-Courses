using System;
using System.Collections.Generic;
using System.Linq;

namespace _02.Knapsack
{
    public class Item
    {
        public Item(string name, int weight, int value)
        {
            this.Name = name;
            this.Weight = weight;
            this.Value = value;
        }

        public string Name { get; set; }

        public int Weight { get; set; }

        public int Value { get; set; }
    }

    class StartUp
    {
        private static int[,] matrix;
        private static bool[,] memo;
        private static int totalWeight = 0;

        static void Main()
        {
            var capacity = int.Parse(Console.ReadLine());

            var items = ReadItems();

            int totalValue = GetTotalValue(items, capacity);
            var set = new SortedSet<string>();

            for (int i = matrix.GetLength(0) - 1; i >= 0; i--)
            {
                if (memo[i, capacity])
                {
                    set.Add(items[i - 1].Name);
                    capacity -= items[i - 1].Weight;
                    totalWeight += items[i - 1].Weight;
                }
            }

            Console.WriteLine($"Total Weight: {totalWeight}");
            Console.WriteLine($"Total Value: {totalValue}");

            foreach (var name in set)
            {
                Console.WriteLine(name);
            }
        }

        private static int GetTotalValue(List<Item> items, int capacity)
        {
            matrix = new int[items.Count + 1, capacity + 1];
            memo = new bool[items.Count + 1, capacity + 1];

            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                var currentItem = items[i - 1];
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    var skip = matrix[i - 1, j];

                    if (currentItem.Weight > j)
                    {
                        matrix[i, j] = skip;
                    }
                    else
                    {
                        var take = matrix[i - 1, j - currentItem.Weight] + currentItem.Value;

                        if (take > skip)
                        {
                            matrix[i, j] = take;
                            memo[i, j] = true;
                        }
                        else
                        {
                            matrix[i, j] = skip;
                        }
                    }
                }
            }

            //for (int i = 0; i < matrix.GetLength(0); i++)
            //{
            //    for (int j = 0; j < matrix.GetLength(1); j++)
            //    {
            //        Console.Write(memo[i, j] + " ");
            //    }

            //    Console.WriteLine();
            //}

            return matrix[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
        }

        private static List<Item> ReadItems()
        {
            var result = new List<Item>();
            var input = "";

            while ((input = Console.ReadLine()) != "end")
            {
                var item = input
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                result.Add(new Item(item[0], int.Parse(item[1]), int.Parse(item[2])));
            }

            return result;
        }
    }
}
