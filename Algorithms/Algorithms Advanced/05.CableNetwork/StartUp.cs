using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _05.CableNetwork
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
        private static HashSet<int> forest = new HashSet<int>();
        private static int budget;
        private static int usedBudget;

        static void Main()
        {
            budget = int.Parse(Console.ReadLine());
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            usedBudget = 0;

            ReadGraph(edgesCount);

            Prim();

            Console.WriteLine($"Budget used: {usedBudget}");
        }

        private static void Prim()
        {
            var priorityQueue = new OrderedBag<Edge>(
                Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));

            foreach (var node in forest)
            {
                priorityQueue.AddMany(graph[node]);
            }

            while (priorityQueue.Count > 0)
            {
                var edge = priorityQueue.RemoveFirst();

                var fisrt = edge.First;
                var second = edge.Second;
                var weight = edge.Weight;

                var nonTreeNode = GetNonTreeNode(fisrt, second);

                if (nonTreeNode == -1)
                {
                    continue;
                }

                if (budget - weight < 0)
                {
                    break;
                }

                budget -= weight;
                usedBudget += weight;
                forest.Add(nonTreeNode);

                priorityQueue.AddMany(graph[nonTreeNode]);
            }
        }

        private static int GetNonTreeNode(int first, int second)
        {
            var nonTreeNode = -1;

            if (forest.Contains(first) && !forest.Contains(second))
            {
                nonTreeNode = second;
            }
            else if (!forest.Contains(first) && forest.Contains(second))
            {
                nonTreeNode = first;
            }

            return nonTreeNode;
        }

        private static void ReadGraph(int edgesCount)
        {
            for (int i = 0; i < edgesCount; i++)
            {
                var edgeInfo = Console.ReadLine()
                    .Split()
                    .ToArray();

                var first = int.Parse(edgeInfo[0]);
                var second = int.Parse(edgeInfo[1]);
                var weight = int.Parse(edgeInfo[2]);

                if (edgeInfo.Length == 4)
                {

                    forest.Add(first);
                    forest.Add(second);
                    continue;
                }

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
