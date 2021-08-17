using System;
using System.Collections.Generic;
using System.Linq;

namespace _09_RoadReconstruction
{
    public class Edge
    {
        public Edge(string from, string to)
        {
            this.From = from;
            this.To = to;
        }

        public string From { get; set; }

        public string To { get; set; }

        public string Reversed => $"{this.To} - {this.From}";

        public override string ToString()
        {

            if (this.From.CompareTo(this.To) < 0 || this.From.CompareTo(this.To) == 0)
            {
                return $"{this.From} {this.To}";
            }
            else
            {
                return $"{this.To} {this.From}";
            }
        }
    }

    class StartUp
    {
        private static List<Edge> edges = new List<Edge>();
        private static List<string> stringEdges = new List<string>();
        private static Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
        private static bool isFound = false;

        static void Main()
        {
            int buildings = int.Parse(Console.ReadLine());
            int streets = int.Parse(Console.ReadLine());

            for (int i = 0; i < streets; i++)
            {
                var input = Console.ReadLine()
                    .Split(" - ", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var edge = new Edge(input[0], input[1]);
                edges.Add(edge);

                if (!graph.ContainsKey(edge.From))
                {
                    graph[edge.From] = new List<string>();
                }
                if (!graph.ContainsKey(edge.To))
                {
                    graph[edge.To] = new List<string>();
                }

                graph[edge.From].Add(edge.To);
                graph[edge.To].Add(edge.From);
            }

            List<string> removedEdges = new List<string>();

            foreach (var edge in edges)
            {
                graph[edge.From].Remove(edge.To);
                graph[edge.To].Remove(edge.From);

                if (DfsPath(edge.From, edge.To, new HashSet<string>()))
                {
                    graph[edge.From].Add(edge.To);
                    graph[edge.To].Add(edge.From);
                    isFound = false;
                }
                else
                {
                    removedEdges.Add(edge.ToString());

                    graph[edge.From].Add(edge.To);
                    graph[edge.To].Add(edge.From);
                }
            }

            Console.WriteLine("Important streets:");

            //removedEdges = removedEdges
            //    .OrderByDescending(s => s)
            //    .ToList();

            foreach (var removedEdge in removedEdges)
            {
                Console.WriteLine(removedEdge);
            }
        }

        private static bool DfsPath(string node, string destination, HashSet<string> visited)
        {
            if (node == destination)
            {
                isFound = true;
                return true;
            }

            visited.Add(node);

            foreach (var child in graph[node])
            {
                if (visited.Contains(child))
                {
                    continue;
                }
                DfsPath(child, destination, visited);
            }

            if (isFound)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
