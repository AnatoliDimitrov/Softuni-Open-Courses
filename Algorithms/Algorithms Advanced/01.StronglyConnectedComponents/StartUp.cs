using System;
using System.Collections.Generic;
using System.Linq;

namespace _01.StronglyConnectedComponents
{
    class StartUp
    {
        private static List<int>[] graph;
        private static List<int>[] reversedGraph;
        private static HashSet<int> visited = new HashSet<int>();

        static void Main()
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int linesCount = int.Parse(Console.ReadLine());

            graph = new List<int>[nodesCount];
            reversedGraph = new List<int>[nodesCount];

            ReadGraph(linesCount);

            var sorted = TopologicalSort();
            visited = new HashSet<int>();

            Console.WriteLine("Strongly Connected Components:");

            foreach (var node in sorted)
            {
                if (visited.Contains(node))
                {
                    continue;
                }

                var component = new Stack<int>(); 
                component = Dfs(reversedGraph, component, node);

                Console.WriteLine($"{{{string.Join(", ", component)}}}");
            }
        }

        private static Stack<int> TopologicalSort()
        {
            var result = new Stack<int>();

            visited = new HashSet<int>();

            for (int i = 0; i < graph.Length; i++)
            {
                Dfs(graph, result, i);
            }

            return result;
        }

        private static Stack<int> Dfs(List<int>[] currentGraph, Stack<int> result, int node)
        {
            if (visited.Contains(node))
            {
                return result;
            }

            visited.Add(node);

            foreach (var child in currentGraph[node])
            {
                Dfs(currentGraph, result, child);
            }

            result.Push(node);

            return result;
        }

        private static void ReadGraph(int linesCount)
        {
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
                reversedGraph[i] = new List<int>();
            }

            for (int i = 0; i < linesCount; i++)
            {
                var line = Console.ReadLine()
                    .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int j = 1; j < line.Length; j++)
                {
                    graph[line[0]].Add(line[j]);
                    reversedGraph[line[j]].Add(line[0]);
                }
            }
        }
    }
}
