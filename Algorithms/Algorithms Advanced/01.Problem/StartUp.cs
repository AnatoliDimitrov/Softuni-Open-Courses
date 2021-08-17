using System;
using System.Linq;
using System.Collections.Generic;
using Wintellect.PowerCollections;

namespace _01.Problem
{
    public class Edge
    {
        public Edge(int first, int second, int weight)
        {
            this.First = first;
            this.Second = second;
            this.Weight = weight;
        }

        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    class StartUp
    {
        private static List<Edge> edges = new List<Edge>();
        private static HashSet<int> nodes = new HashSet<int>();
        private static int[] parents;
        private static int weight = 0;

        static void Main()
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            ReadEdges(edgesCount);

            var sortedEdges = edges
                .OrderBy(e => e.Weight)
                .ToList();

            parents = new int[nodes.Max() + 1];

            foreach (var node in nodes)
            {
                parents[node] = node;
            }

            for (int i = 0; i < sortedEdges.Count; i++)
            {
                var edge = sortedEdges[i];

                int firstRoot = GetRoot(edge.First);
                int secondRoot = GetRoot(edge.Second);

                if (firstRoot == secondRoot)
                {
                    continue;
                }

                weight += edge.Weight;

                //Console.WriteLine($"{edge.First} - {edge.Second}");
                parents[firstRoot] = secondRoot;
            }

            Console.WriteLine(weight);
        }

        private static int GetRoot(int node)
        {
            while (parents[node] != node)
            {
                node = parents[node];
            }

            return node;
        }

        private static void ReadEdges(int edgesCount)
        {
            for (int i = 0; i < edgesCount; i++)
            {
                var edgeInfo = Console.ReadLine()
                    .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var first = edgeInfo[0];
                var second = edgeInfo[1];
                var weight = edgeInfo[2];

                nodes.Add(first);
                nodes.Add(second);

                edges.Add(new Edge(first, second, weight));
            }
        }
    }
}
