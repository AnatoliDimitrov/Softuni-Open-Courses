using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _02.ChainLightning
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
        private static HashSet<int> visited;
        private static int[] parents;
        private static int[] nodesDamage;



        static void Main()
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());
            var strikes = int.Parse(Console.ReadLine());

            nodesDamage = new int[nodesCount];

            ReadGraph(edgesCount, nodesCount);

            for (int i = 0; i < strikes; i++)
            {
                visited = new HashSet<int>();
                parents = new int[nodesCount];


                var lightningInfo = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var node = lightningInfo[0];
                var damage = lightningInfo[1];

                parents[node] = -1;

                Prim(node);

                foreach (var currentNode in visited)
                {
                    var hits = GetHits(currentNode) - 1;

                    var currentDamage = damage;

                    for (int j = 0; j < hits; j++)
                    {
                        currentDamage = currentDamage / 2;
                    }

                    nodesDamage[currentNode] += currentDamage;
                }
            }

            Console.WriteLine(nodesDamage.Max());
        }

        private static int GetHits(int node)
        {
            var result = 0;

            while (node != -1)
            {
                result++;
                node = parents[node];
            }

            return result;
        }

        private static void Prim(int node)
        {
            var result = 0;

            visited.Add(node);

            var queue = new OrderedBag<Edge>(Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));
            queue.AddMany(graph[node]);

            while (queue.Count > 0)
            {
                var edge = queue.RemoveFirst();

                var notInTreeNode = -1;
                var parent = -1;

                if (visited.Contains(edge.First) && !visited.Contains(edge.Second))
                {
                    notInTreeNode = edge.Second;
                    parent = edge.First;
                }
                else if (!visited.Contains(edge.First) && visited.Contains(edge.Second))
                {
                    notInTreeNode = edge.First;
                    parent = edge.Second;
                }

                if (notInTreeNode == -1)
                {
                    continue;
                }

                parents[notInTreeNode] = parent;
                visited.Add(notInTreeNode);
                queue.AddMany(graph[notInTreeNode]);
            }
        }

        private static void ReadGraph(int edgesCount, int nodesCount)
        {
            for (int i = 0; i < nodesCount; i++)
            {
                graph[i] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var edgeInfo = Console.ReadLine()
                    .Split(new[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var first = edgeInfo[0];
                var second = edgeInfo[1];
                var weight = edgeInfo[2];

                var edge = new Edge(first, second, weight);

                graph[first].Add(edge);
                graph[second].Add(edge);
            }
        }
    }
}
