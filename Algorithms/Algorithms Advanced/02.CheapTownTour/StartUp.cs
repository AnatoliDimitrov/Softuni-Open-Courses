using System;
using System.Collections.Generic;
using System.Linq;

namespace _02.CheapTownTour
{
    public class Edge
    {
        public Edge(int from, int to, int weight)
        {
            this.First = from;
            this.Second = to;
            this.Weight = weight;
        }

        public int First { get; set; }

        public int Second { get; set; }

        public int Weight { get; set; }
    }

    class StartUp
    {
        private static List<Edge> edges = new List<Edge>();
        private static int[] roots;

        static void Main()
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            ReadEdges(edgesCount);

            roots = new int[nodesCount]; // Check

            for (int i = 0; i < roots.Length; i++)
            {
                    roots[i] = i;
            }

            var sortedEdges = edges
                .OrderBy(e => e.Weight)
                .ToList();

            var result = 0;

            foreach (var edge in sortedEdges)
            {
                var firstRoot = GetRoot(edge.First);
                var secondRoot = GetRoot(edge.Second);

                if (firstRoot == secondRoot)
                {
                    continue;
                }

                result += edge.Weight;
                roots[firstRoot] = secondRoot;

            }

            Console.WriteLine($"Total cost: {result}");
        }

        private static int GetRoot(int node)
        {
            while (roots[node] != node)
            {
                node = roots[node];
            }

            return node;
        }

        private static void ReadEdges(int edgesCount)
        {
            for (int i = 0; i < edgesCount; i++)
            {
                var edgeInfo = Console.ReadLine()
                    .Split(" - ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var first = edgeInfo[0];
                var second = edgeInfo[1];
                var weight = edgeInfo[2];

                var edge = new Edge(first, second, weight);
                edges.Add(edge);
            }
        }
    }
}
