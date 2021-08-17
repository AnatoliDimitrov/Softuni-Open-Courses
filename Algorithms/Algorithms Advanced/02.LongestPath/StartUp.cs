using System;
using System.Collections.Generic;
using System.Linq;

namespace _02.LongestPath
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
        private static int[] prev;

        static void Main()
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            ReadGraph(edgesCount);

            int start = int.Parse(Console.ReadLine());
            int end = int.Parse(Console.ReadLine());

            int totalWeight = FindLongestPath(start, end);

            Console.WriteLine(totalWeight);
            Console.WriteLine(string.Join(" ", GetPath(end)));
        }

        private static Stack<int> GetPath(int node)
        {
            var result = new Stack<int>();

            while (node != 0)
            {
                result.Push(node);
                node = prev[node];
            }

            return result;
        }

        private static int FindLongestPath(int start, int end)
        {
            var distances = new double[graph.Length];
            Array.Fill(distances, double.NegativeInfinity);
            distances[start] = 0;
            prev = new int[graph.Length];
            Array.Fill(prev, -1);
            prev[start] = 0;

            Stack<int> sorted = TopologicalSort();

            foreach (var node in sorted)
            {
                foreach (var edge in graph[node])
                {

                    var distance = distances[edge.From] + edge.Weight;
                    if (distances[edge.To] < distance)
                    {
                        prev[edge.To] = node; 
                        distances[edge.To] = distance;
                    }
                }
            }

            return (int)distances[end];
        }

        private static Stack<int> TopologicalSort()
        {
            var result = new Stack<int>();
            var visited = new HashSet<int>();

            for (int i = 1; i < graph.Length; i++)
            {
                Dfs(i, result, visited);
            }

            return result;
        }

        private static void Dfs(int node, Stack<int> result, HashSet<int> visited)
        {
            if (visited.Contains(node))
            {
                return;
            }

            visited.Add(node);

            foreach (var edge in graph[node])
            {
                Dfs(edge.To, result, visited); 
            }

            result.Push(node);
        }

        private static void ReadGraph(in int count)
        {
            graph = new List<Edge>[count + 1];

            for (int i = 1; i < graph.Length; i++)
            {
                graph[i] = new List<Edge>();
            }

            for (int i = 0; i < count; i++)
            {
                var edgeInfo = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                graph[edgeInfo[0]].Add(new Edge(edgeInfo[0], edgeInfo[1], edgeInfo[2]));
            }
        }
    }
}
