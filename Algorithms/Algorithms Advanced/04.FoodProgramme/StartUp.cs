using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _04.FoodProgramme
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

        static void Main()
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            var info = Console.ReadLine().Split().Select(int.Parse).ToArray();

            var start = info[0];
            var end = info[1];

            ReadGraph(edgesCount);

            var maxNode = graph.Keys.Max();

            distances = new int[maxNode + 1];
            parents = new int[maxNode + 1];

            for (int i = 0; i <= maxNode; i++)
            {
                distances[i] = int.MaxValue;
                parents[i] = -1;
            }

            distances[start] = 0;

            int minimalCost = FindMinimalCost(start, end);

            if (parents[end] == -1)
            {
                Console.WriteLine("There is no such path.");
            }
            else
            {
                Stack<int> path = GetPath(end);

                Console.WriteLine(string.Join(" ", path));
                Console.WriteLine(minimalCost);
            }
        }

        private static Stack<int> GetPath(int node)
        {
            var result = new Stack<int>();

            while (parents[node] != -1)
            {
                result.Push(node);
                node = parents[node];
            }

            result.Push(node);

            return result;
        }

        private static int FindMinimalCost(int start, int end)
        {
            var queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => distances[f] - distances[s]));

            queue.Add(start);

            while (queue.Count != 0)
            {
                var minDistanceNode = queue.RemoveFirst();

                foreach (var edge in graph[minDistanceNode])
                {
                    int child = int.MinValue;

                    if (edge.First == minDistanceNode)
                    {
                        child = edge.Second;
                    }
                    else
                    {
                        child = edge.First;
                    }

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

                        queue = new OrderedBag<int>(queue, Comparer<int>.Create((f, s) => distances[f] - distances[s]));
                    }
                }

                if (minDistanceNode == end)
                {
                    break;
                }
            }

            return distances[end];
        }

        private static void ReadGraph(int edgesCount)
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
