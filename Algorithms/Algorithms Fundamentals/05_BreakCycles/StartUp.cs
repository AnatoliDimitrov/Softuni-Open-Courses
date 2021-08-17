using System;
using System.Collections.Generic;
using System.Linq;

namespace _05_BreakCycles
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
            return $"{this.From} - {this.To}";
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
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine()
                    .Split("->", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var node = input[0].TrimEnd();

                graph[node] = new List<string>();

                if (input.Length > 1)
                {
                    var children = input[1]
                        .Trim()
                        .Split()
                        .ToArray();

                    for (int j = 0; j < children.Length; j++)
                    {
                        graph[node].Add(children[j]);

                        var edge = new Edge(node, children[j]);

                        edges.Add(edge);
                    }
                }
            }

            edges = edges
                .OrderBy(e => e.From)
                .ThenBy(e => e.To)
                .ToList();

            List<string> removedEdges = new List<string>();

            foreach (var edge in edges)
            {
                graph[edge.From].Remove(edge.To);
                graph[edge.To].Remove(edge.From);

                if (DfsPath(edge.From, edge.To, new HashSet<string>()))
                {
                    if (!removedEdges.Contains(edge.Reversed))
                    {

                        removedEdges.Add(edge.ToString());
                    }
                    isFound = false;
                }
                else
                {
                    graph[edge.From].Add(edge.To);
                    graph[edge.To].Add(edge.From);
                }
            }

            Console.WriteLine($"Edges to remove: {removedEdges.Count}");

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
