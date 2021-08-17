using System;
using System.Collections.Generic;
using System.Linq;

namespace _04_DistanceBetweenVertices
{
    class StartUp
    {
        private static Dictionary<int, List<int>> graph;

        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            int p = int.Parse(Console.ReadLine());

            graph = ReadGraph(n);

            for (int i = 0; i < p; i++)
            {
                var line = Console.ReadLine()
                    .Split("-", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                int from = line[0];
                int to = line[1];

                int distance = BfsDistance(from, to);

                Console.WriteLine($"{{{from}, {to}}} -> {distance}");

            }
        }

        private static int BfsDistance(int from, int to)
        {
            HashSet<int> visited = new HashSet<int>();
            var distances = new Dictionary<int, int>() { { from, 0 } };
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(from);

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();

                if (current == to)
                {
                    return distances[current];
                }

                foreach (var child in graph[current])
                {
                    if (visited.Contains(child))
                    {
                        continue;
                    }
                    queue.Enqueue(child);

                    distances[child] = distances[current] + 1;

                    visited.Add(child);
                }
            }

            return -1;
        }

        private static Dictionary<int, List<int>> ReadGraph(int n)
        {
            var result = new Dictionary<int, List<int>>();

            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine()
                    .Split(":", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var node = int.Parse(input[0]);
                if (input.Length > 1)
                {
                    var children = input[1]
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToList();

                    result[node] = children;
                }
                else
                {
                    result[node] = new List<int>();
                }
            }

            return result;
        }
    }
}
