using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace _03.Undefined
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
        private static int[] parents;

        static void Main()
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            distances = new double[nodesCount + 1];
            parents = new int[nodesCount + 1];

            for (int i = 0; i < nodesCount + 1; i++)
            {
                distances[i] = double.PositiveInfinity;
                parents[i] = -1;
            }

            ReadEdges(edgesCount);

            var start = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            distances[start] = 0;

            var sorted = edges
                .OrderBy(e => e.Weight);

            for (int i = 0; i < edgesCount - 1; i++)
            {
                var isChanged = false;

                foreach (var edge in sorted)
                {
                    if (distances[edge.From] == double.PositiveInfinity)
                    {
                        continue;
                    }

                    var distance = distances[edge.From] + edge.Weight;
                    if (distances[edge.To] > distance)
                    {
                        distances[edge.To] = distance;
                        parents[edge.To] = edge.From;
                        isChanged = true;
                    }
                }

                if (!isChanged)
                {
                    break;
                }
            }

            foreach (var edge in edges)
            {
                if (distances[edge.From] == Double.NegativeInfinity)
                {
                    continue;
                }

                var distance = distances[edge.From] + edge.Weight;
                if (distances[edge.To] > distance)
                {
                    Console.WriteLine("Undefined");
                    return;
                }
            }

            var path = ReconstructPath(destination);

            Console.WriteLine(string.Join(" ", path));
            Console.WriteLine(distances[destination]);
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

        private static void ReadEdges(int edgesCount)
        {
            for (int i = 0; i < edgesCount; i++)
            {
                var edgeInfo = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                edges.Add( new Edge(edgeInfo[0], edgeInfo[1], edgeInfo[2]));
            }
        }
    }
}
