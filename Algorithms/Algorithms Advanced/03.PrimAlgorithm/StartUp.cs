using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _03.PrimAlgorithm
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
        private static Dictionary<int, List<Edge>> graph = new Dictionary<int, List<Edge>>();
        private static HashSet<int> visited = new HashSet<int>();
        private static List<int> totalWeight = new List<int>();

        

        static void Main()
        {
            var edgesCount = int.Parse(Console.ReadLine());

            ReadGraph(edgesCount);

            foreach (var node in graph.Keys)
            {
                if (!visited.Contains(node))
                {
                    int currentWeght = Prim(node);

                    Console.WriteLine($"Weight Of Current Tree: {currentWeght}");

                    totalWeight.Add(currentWeght);
                }
            }

            Console.WriteLine($"Total Weight: {totalWeight.Sum()}");
        }

        private static int Prim(int node)
        {
            var result = 0;

            visited.Add(node);

            var queue = new OrderedBag<Edge>(Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));
            queue.AddMany(graph[node]);

            while (queue.Count > 0)
            {
                var edge = queue.RemoveFirst();

                var notInTreeNode = -1;

                if (visited.Contains(edge.First) && !visited.Contains(edge.Second))
                {
                    notInTreeNode = edge.Second;
                }
                else if (!visited.Contains(edge.First) && visited.Contains(edge.Second))
                {
                    notInTreeNode = edge.First;
                }

                if (notInTreeNode == -1)
                {
                    continue;
                }

                visited.Add(notInTreeNode);
                queue.AddMany(graph[notInTreeNode]);

                Console.WriteLine($"{edge.First} - {edge.Second}");

                result += edge.Weight;
            }

            return result;
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

                var edge = new Edge(first, second, weight);

                graph[first].Add(edge);
                graph[second].Add(edge);
            }
        }
    }
}
