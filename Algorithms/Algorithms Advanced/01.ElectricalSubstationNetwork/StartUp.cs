using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace _01.ElectricalSubstationNetwork
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

            ReadGraphs(linesCount, nodesCount);

            var sorted = TopologicalSort();

            visited = new HashSet<int>();

            while (sorted.Count > 0)
            {
                var node = sorted.Pop();

                if (visited.Contains(node))
                {
                    continue;
                }

                var component = new Stack<int>();
                Dfs(reversedGraph, component, node);

                Console.WriteLine(string.Join(", ", component));
            }
        }

        private static Stack<int> TopologicalSort()
        {
            var result = new Stack<int>();

            for (int i = 0; i < graph.Length; i++)
            {
                Dfs(graph, result, i);
            }

            return result;
        }

        private static void Dfs(List<int>[] currentGraph, Stack<int> result, int node)
        {
            if (visited.Contains(node))
            {
                return;
            }

            visited.Add(node);

            foreach (var child in currentGraph[node])
            {
                if (visited.Contains(child))
                {
                    continue;
                }

                Dfs(currentGraph, result, child);
            }

            result.Push(node);
        }

        private static void ReadGraphs(int linesCount, int nodesCount)
        {
            graph = new List<int>[nodesCount];
            reversedGraph = new List<int>[nodesCount];

            for (int i = 0; i < nodesCount; i++)
            {
                graph[i] = new List<int>();
                reversedGraph[i] = new List<int>();
            }

            for (int i = 0; i < linesCount; i++)
            {
                var line = Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();

                var node = line[0];

                for (int j = 1; j < line.Length; j++)
                {
                    graph[node].Add(line[j]);
                    reversedGraph[line[j]].Add(node);
                }
            }
        }
    }
}
