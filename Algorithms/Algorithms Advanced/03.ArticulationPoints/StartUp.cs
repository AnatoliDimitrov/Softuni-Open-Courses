using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.ArticulationPoints
{
    class StartUp
    {
        private static List<int>[] graph;
        private static int[] depths;
        private static int[] lowestPoints;
        private static int[] parents;
        private static HashSet<int> visited = new HashSet<int>();
        private static List<int> result = new List<int>();

        static void Main()
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int linesCount = int.Parse(Console.ReadLine());

            graph = new List<int>[nodesCount];

            ReadGraph(linesCount);

            depths = new int[nodesCount];
            lowestPoints = new int[nodesCount];
            parents = new int[nodesCount];

            Array.Fill(parents, -1);

            for (int i = 0; i < graph.Length; i++)
            {
                if (visited.Contains(i))
                {
                    continue;
                }

                FindArticolationPoints(i, 1);
            }

            Console.WriteLine($"Articulation points: {string.Join(", ", result)}");
        }

        private static void FindArticolationPoints(int node, int depth)
        {
            visited.Add(node);
            lowestPoints[node] = depth;
            depths[node] = depth;
            var childCount = 0;
            var IsAP = false;

            foreach (var child in graph[node])
            {
                if (!visited.Contains(child))
                {
                    parents[child] = node;
                    FindArticolationPoints(child, depth + 1);

                    childCount += 1;

                    if (lowestPoints[child] >= depths[node])
                    {
                        IsAP = true;
                    }
                    lowestPoints[node] = Math.Min(lowestPoints[node], lowestPoints[child]);
                }
                else if (child != parents[node])
                {
                    lowestPoints[node] = Math.Min(lowestPoints[node], depths[child]);
                }
            }

            if ((parents[node] > -1 && IsAP) || (parents[node] == -1 && childCount > 1))
            {
                result.Add(node);
            }
        }

        private static void ReadGraph(int linesCount)
        {
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
            }

            for (int i = 0; i < linesCount; i++)
            {
                var line = Console.ReadLine()
                    .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int j = 1; j < line.Length; j++)
                {
                    graph[line[0]].Add(line[j]);
                    graph[line[j]].Add(line[0]);
                }
            }
        }
    }
}
