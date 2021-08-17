using System;
using System.Collections.Generic;
using System.Linq;

namespace _06_CyclesInGraph
{
    class StartUp
    {
        private static Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
        private static bool cyclic = false;
        private static HashSet<string> visited = new HashSet<string>();

        static void Main()
        {
            var input = "";

            while ((input = Console.ReadLine()) != "End")
            {
                var nodes = input
                    .Split("-", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var from = nodes[0];
                var to = nodes[1];

                if (!graph.ContainsKey(from))
                {
                    graph[from] = new List<string>();
                }

                graph[from].Add(to);
            }

            foreach (var kvp in graph)
            {
                Dfs(kvp.Key);
            }

            if (!cyclic)
            {
                Console.WriteLine("Acyclic: Yes");
            }
            else
            {
                Console.WriteLine("Acyclic: No");
            }
        }

        private static void Dfs(string node)
        {
            if (visited.Contains(node))
            {
                cyclic = true;
                return;
            }

            visited.Add(node);

            if (graph.ContainsKey(node))
            {
                foreach (var child in graph[node])
                {
                    Dfs(child);
                    visited.Remove(child);
                }
            }
        }
    }
}
