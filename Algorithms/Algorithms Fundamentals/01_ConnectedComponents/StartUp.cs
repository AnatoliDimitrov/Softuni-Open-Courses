using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_ConnectedComponents
{
    class StartUp
    {
        private static List<int>[] graph;
        private static bool[] visited;

        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            graph = ReadGraph(n);
            visited = new bool[n];

            for (int i = 0; i < graph.Length; i++)
            {
                if (!visited[i])
                {
                    var list = Dfs(i, new List<int>());
                    Console.WriteLine($"Connected component: {string.Join(" ", list)}");
                    visited[i] = true;
                }
            }
        }

        private static List<int> Dfs(int node, List<int> result)
        {
            if (visited[node])
            {
                return result;
            }

            visited[node] = true;

            foreach (var child in graph[node])
            {
                Dfs(child, result);
            }

            result.Add(node);

            return result;
        }

        private static List<int>[] ReadGraph(int count)
        {
            var result = new List<int>[count];

            for (int i = 0; i < count; i++)
            {
                var str = Console.ReadLine();

                if (!string.IsNullOrEmpty(str))
                {
                    result[i] = str
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToList();
                }
                else
                {
                    result[i] = new List<int>();
                }
            }

            return result;
        }
    }
}
