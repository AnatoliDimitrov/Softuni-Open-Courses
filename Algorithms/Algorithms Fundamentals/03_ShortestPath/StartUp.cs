using System;
using System.Collections.Generic;
using System.Linq;

namespace _03_ShortestPath
{
    class StartUp
    {
        private static List<int>[] graph;
        private static bool[] visited;
        private static int[] parents;

        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            int count = int.Parse(Console.ReadLine());

            graph = ReadGraph(n, count);
            visited = new bool[graph.Length];
            parents = new int[graph.Length];
            Array.Fill(parents, -1);

            int start = int.Parse(Console.ReadLine());
            int end = int.Parse(Console.ReadLine());

            BfsFindPath(start, end);
        }

        private static void BfsFindPath(int start, int end)
        {
            var queue = new Queue<int>();
            queue.Enqueue(start);

            visited[start] = true;

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();

                if (current == end)
                {
                    ReconstructPath(end);
                    return;
                }

                foreach (var child in graph[current])
                {
                    if (visited[child])
                    {
                        continue;
                    }
                    visited[child] = true;
                    parents[child] = current;
                    queue.Enqueue(child);
                }
            }
        }

        private static void ReconstructPath(int end)
        {
            Stack<int> result = new Stack<int>();
            var index = end;

            while (index != -1)
            {
                result.Push(index);

                index = parents[index];
            }

            Console.WriteLine($"Shortest path length is: {result.Count - 1}");
            Console.WriteLine(string.Join(" ", result));
        }

        private static List<int>[] ReadGraph(int n, int count)
        {
            var result = new List<int>[n + 1];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new List<int>();
            }

            for (int i = 0; i < count; i++)
            {
                int[] edge = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                result[edge[0]].Add(edge[1]);
            }

            return result;
        }
    }
}
