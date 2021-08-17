using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace CorrectMostReliablePath
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
        private static Dictionary<int, List<Edge>> graph = new Dictionary<int, List<Edge>>();
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
                distances[i] = double.NegativeInfinity;
                parents[i] = -1;
            }

            ReadEdges(edgesCount);

            var start = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            GetMostReliablePath(start, destination);

            var path = GetPath(destination);

            Console.WriteLine($"Most reliable path reliability: {distances[destination]:F2}%");

            Console.WriteLine(string.Join(" -> ", path));
        }

        private static Stack<int> GetPath(int node)
        {
            var result = new Stack<int>();

            while (node != -1)
            {
                result.Push(node);
                node = parents[node];
            }

            return result;
        }

        private static void GetMostReliablePath(int start, int destination)
        {
            var queue = new OrderedBag<int>(
                Comparer<int>.Create((f, s) => (int)distances[s] - (int)distances[f]));

            distances[start] = 100.0;

            queue.Add(start);

            while (queue.Count != 0)
            {
                var node = queue.RemoveFirst();
                if (node == destination)
                {
                    break;
                }

                foreach (var edge in graph[node])
                {
                    int child = -1;

                    if (edge.First == node && edge.Second != node)
                    {
                        child = edge.Second;
                    }
                    else if (edge.First != node && edge.Second == node)
                    {
                        child = edge.First;
                    }

                    if (child == -1)
                    {
                        continue;
                    }

                    if (distances[child] == double.NegativeInfinity)
                    {
                        queue.Add(child);
                    }

                    double distance = distances[node] * edge.Weight / 100;
                    if (distances[child] < distance)
                    {
                        distances[child] = distance;
                        parents[child] = node;

                        queue = new OrderedBag<int>(
                            queue,
                            Comparer<int>.Create((f, s) => (int)distances[s] - (int)distances[f]));
                    }
                }
            }
        }

        private static void ReadEdges(int edgesCount)
        {
            for (int i = 0; i < edgesCount; i++)
            {
                var edgeInfo = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var first = edgeInfo[0];
                var second = edgeInfo[1];
                var weight = edgeInfo[2];

                if (!graph.ContainsKey(first))
                {
                    graph[first] = new List<Edge>();
                }

                if (!graph.ContainsKey(second))
                {
                    graph[second] = new List<Edge>();
                }

                var edge = new Edge(first, second, weight);

                graph[first].Add(edge);
                graph[second].Add(edge);
            }
        }
    }
}