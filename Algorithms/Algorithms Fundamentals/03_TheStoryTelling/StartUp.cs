using System;
using System.Collections.Generic;
using System.Linq;

namespace _03_TheStoryTelling
{
    class StartUp
    {
        private static Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
        private static Stack<string> result = new Stack<string>();
        private static HashSet<string> visited = new HashSet<string>();

        static void Main()
        {
            var input = "";

            while ((input = Console.ReadLine()) != "End")
            {
                var line = input
                    .Split("->", StringSplitOptions.None)
                    .ToArray();

                string node = line[0]
                    .TrimEnd();
                var children = new List<string>();
                if (line.Length > 1)
                {
                    children = line[1]
                        .TrimStart()
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .ToList();
                }

                if (!graph.ContainsKey(node))
                {
                    graph[node] = new List<string>();
                }

                graph[node].AddRange(children);
            }

            foreach (var graphKey in graph.Keys)
            {
                if (visited.Contains(graphKey))
                {
                    continue;
                }

                Dfs(graphKey);
            }

            Console.WriteLine(string.Join(" ", result));
        }

        private static void Dfs(string node)
        {
            if (visited.Contains(node))
            {
                return;
            }

            foreach (var child in graph[node])
            {
                if (visited.Contains(child))
                {
                    continue;
                }
                Dfs(child);
            }

            visited.Add(node);
            result.Push(node);
        }
    }
}