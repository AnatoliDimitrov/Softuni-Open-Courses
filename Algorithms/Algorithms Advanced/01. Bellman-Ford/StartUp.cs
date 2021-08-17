using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Bellman_Ford
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
        private static List<Edge> edges = new List<Edge>();
        private static double[] distances;
        private static int[] prev;

        static void Main()
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            ReadEdges(edgesCount);

            var start = int.Parse(Console.ReadLine());
            var end = int.Parse(Console.ReadLine());

            distances = new double[nodesCount + 1];
            Array.Fill(distances, double.PositiveInfinity);

            distances[start] = 0;

            prev = new int[distances.Length];

            for (int i = 0; i < nodesCount - 1; i++)
            {
                var isChanged = false;

                foreach (var edge in edges)
                {
                    if (distances[edge.From] == double.PositiveInfinity)
                    {
                        continue;
                    }

                    var destination = distances[edge.From] + edge.Weight;
                    if (destination < distances[edge.To])
                    {
                        isChanged = true;
                        distances[edge.To] = destination;
                        prev[edge.To] = edge.From;
                    }
                }

                if (!isChanged)
                {
                    break;
                }
            }

            foreach (var edge in edges)
            {
                if (edge.From == double.PositiveInfinity)
                {
                    continue;
                }

                var newDestination = distances[edge.From] + edge.Weight;
                if (newDestination < distances[edge.To])
                {
                    Console.WriteLine("Negative Cycle Detected");
                    return;
                }
            }

            Console.WriteLine(string.Join(" ", GetPath(end)));
            Console.WriteLine(distances[end]);
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

        private static void ReadEdges(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var edgeInfo = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                edges.Add(new Edge(edgeInfo[0], edgeInfo[1], edgeInfo[2]));
            }
        }
    }
}
