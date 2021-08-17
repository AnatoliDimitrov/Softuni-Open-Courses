using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_TopologicalSorting
{
    class StartUp
    {
        private static Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
        private static Dictionary<string, int> dependencies = new Dictionary<string, int>();

        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            ReadGraph(n);

            FindDependencies();

            Sort();
        }

        private static void Sort()
        {
            var sorted = new List<string>();

            while (dependencies.Count != 0)
            {
                var node = dependencies
                    .FirstOrDefault(k => k.Value == 0);

                if (node.Key == null)
                {
                    break;
                }

                foreach (var child in graph[node.Key])
                {
                    dependencies[child]--;
                }

                sorted.Add(node.Key);
                dependencies.Remove(node.Key);

            }

            if (dependencies.Count != 0)
            {
                Console.WriteLine("Invalid topological sorting");
            }
            else
            {
                Console.WriteLine($"Topological sorting: {string.Join(", ", sorted)}");
            }
        }

        private static void FindDependencies()
        {
            foreach (var node in graph)
            {

                var key = node.Key;
                var children = node.Value;

                if (!dependencies.ContainsKey(key))
                {
                    dependencies.Add(key, 0);
                }

                foreach (var child in children)
                {
                    if (!dependencies.ContainsKey(child))
                    {
                        dependencies.Add(child, 0);
                    }

                    dependencies[child] += 1;
                }
            }
        }

        private static void ReadGraph(int n)
        {
            for (int i = 0; i < n; i++)
            {
                var line = Console.ReadLine()
                    .Split("->", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var key = line[0].TrimEnd();
                if (line.Length == 1)
                {
                    graph.Add(key, new List<string>());
                }
                else
                {
                    var children = line[1]
                        .TrimStart()
                        .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                        .ToList();

                    graph.Add(key, children);
                }
            }
        }
    }
}
