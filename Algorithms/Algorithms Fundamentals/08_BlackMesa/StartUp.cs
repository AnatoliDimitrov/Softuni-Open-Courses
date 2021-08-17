using System;
using System.Collections.Generic;
using System.Linq;

namespace _08_BlackMesa
{
    class StartUp
    {
        private static Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();
        private static HashSet<int> all = new HashSet<int>();
        private static HashSet<int> visited = new HashSet<int>();

        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < edgesCount; i++)
            {
                int[] edge = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                int from = edge[0];
                int to = edge[1];

                if (!graph.ContainsKey(from))
                {
                    graph[from] = new List<int>();
                }
                graph[from].Add(to);
                all.Add(from);
                all.Add(to);
            }

            int start = int.Parse(Console.ReadLine());
            int target = int.Parse(Console.ReadLine());

            Bfs(start, target);

            Console.WriteLine(string.Join(" ", visited));

            all.ExceptWith(visited);

            all = all
                .OrderBy(x => x)
                .ToHashSet();

            Console.WriteLine(string.Join(" ", all));
        }

        private static void Bfs(int start, int target)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();

                visited.Add(current);

                if (current == target)
                {
                    visited.Add(current);
                    break;
                }

                foreach (var child in graph[current])
                {
                    if (visited.Contains(child))
                    {
                        continue;
                    }

                    if (current == target)
                    {
                        visited.Add(current);
                        break;
                    }

                    queue.Enqueue(child);
                }
            }
        }
    }
}
