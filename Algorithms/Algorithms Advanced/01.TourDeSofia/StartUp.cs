using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _01.TourDeSofia
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

    public class StartUp
    {
        private static Dictionary<int, List<Edge>> graph = new Dictionary<int, List<Edge>>();
        private static int[] distances;
        private static int[] parents;
        private static HashSet<int> visited = new HashSet<int>();

        static void Main()
        {
            var nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            var start = int.Parse(Console.ReadLine());
            var end = start;

            ReadGraph(edgesCount);

            distances = new int[nodesCount + 1];
            parents = new int[nodesCount + 1];

            for (int i = 0; i <= nodesCount; i++)
            {
                distances[i] = int.MaxValue;
                parents[i] = -1;
            }

            distances[start] = 0;

            int minimalCost = FindMinimalCost(start, end);

            if (parents[end] == -1)
            {
                Console.WriteLine(visited.Count);
            }
            else
            {
                Console.WriteLine(minimalCost);

            }
        }

        private static int FindMinimalCost(int start, int end)
        {
            var queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => distances[f] - distances[s]));
            var isStart = true;

            queue.Add(start);

            while (queue.Count != 0)
            {
                var minDistanceNode = queue.RemoveFirst();

                if (minDistanceNode == end && !isStart)
                {
                    parents[minDistanceNode] = minDistanceNode;
                    return distances[minDistanceNode];
                }

                visited.Add(minDistanceNode);

                foreach (var edge in graph[minDistanceNode])
                {
                    var child = edge.Second;
                    visited.Add(child);

                    if (child == int.MinValue)
                    {
                        continue;
                    }

                    if (distances[child] == int.MaxValue)
                    {
                        queue.Add(child);
                    }

                    var distance = edge.Weight + distances[minDistanceNode];


                    if (distances[child] > distance)
                    {
                        parents[child] = minDistanceNode;
                        distances[child] = distance;

                        if (minDistanceNode == end && isStart)
                        {
                            distances[end] = int.MaxValue;
                        }

                        queue = new OrderedBag<int>(queue, Comparer<int>.Create((f, s) => distances[f] - distances[s]));
                    }
                }

                isStart = false;
            }

            return distances[end];
        }

        private static void ReadGraph(int edgesCount)
        {
            for (int i = 0; i < edgesCount; i++)
            {
                var edgeInfo = Console.ReadLine()
                    .Split(new[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries)
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

                graph[first].Add(new Edge(first, second, weight));
            }
        }
    }
}