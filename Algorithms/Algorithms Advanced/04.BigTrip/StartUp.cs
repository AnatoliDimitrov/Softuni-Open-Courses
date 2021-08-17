using System;
using System.Collections.Generic;
using System.Linq;

namespace _04.BigTrip
{
    public class Edge
    {
        public Edge(int from, int to, int weight)
        {
            this.From = from;
            this.To = to;
            this.Weight = weight;
        }

        public int From { get; set; }

        public int To { get; set; }

        public int Weight { get; set; }
    }

    class StartUp
    {
        private static List<Edge>[] graph;
        private static double[] distances;
        private static int[] parents;
        private static Stack<int> sorted = new Stack<int>();

        static void Main()
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            distances = new double[nodesCount + 1];
            parents = new int[nodesCount + 1];

            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = double.NegativeInfinity;
                parents[i] = -1;
            }

            ReadGraph(nodesCount, edgesCount);

            int start = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());

            distances[start] = 0;

            var longestPath = FindLongestPath(start, destination);

            Console.WriteLine(longestPath);

            var path = ReconstructPath(destination);

            Console.WriteLine(string.Join(" ", path));
        }

        private static Stack<int> ReconstructPath(int node)
        {
            var result = new Stack<int>();

            while (node != -1)
            {
                result.Push(node);
                node = parents[node];
            }

            return result;
        }

        private static int FindLongestPath(int start, int destination)
        {
            foreach (var node in sorted)
            {
                foreach (var edge in graph[node])
                {
                    var distance = distances[edge.From] + edge.Weight;

                    if (distances[edge.To] < distance)
                    {
                        distances[edge.To] = distance;
                        parents[edge.To] = node;
                    }
                }
            }

            return (int)distances[destination];
        }

        private static void ReadGraph(int nodesCount, int edgesCount)
        {
            graph = new List<Edge>[nodesCount + 1];

            for (int i = 1; i < graph.Length; i++)
            {
                graph[i] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var edgeInfo = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var from = edgeInfo[0];
                var to = edgeInfo[1];
                var weight = edgeInfo[2];

                graph[from].Add(new Edge(from, to, weight));
            }

            TopologicalSort();
        }

        private static void TopologicalSort()
        {
            var visited = new HashSet<int>();

            for (int i = 1; i < graph.Length; i++)
            {
                Dfs(i, visited);
            }
        }

        private static void Dfs(int node, HashSet<int> visited)
        {
            if (visited.Contains(node))
            {
                return;
            }

            visited.Add(node);

            foreach (var edge in graph[node])
            {
                Dfs(edge.To, visited);
            }

            sorted.Push(node);
        }
    }
}
