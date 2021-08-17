using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.FindBi_ConnectedComponentsFORCOMPONENTS
{
    class StartUp
    {
        private static List<int>[] graph;
        private static int[] depths;
        private static int[] lowestPoints;
        private static int[] parents;
        private static HashSet<int> visited = new HashSet<int>();
        private static HashSet<int> result = new HashSet<int>();
        private static int children = 0;
        private static List<HashSet<int>> componentNodes = new List<HashSet<int>>();
        private static Stack<int> stack = new Stack<int>();

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

                var component = new HashSet<int>();

                while (stack.Count > 1)
                {
                    var stackChild = stack.Pop();
                    var stackNode = stack.Pop();

                    component.Add(stackNode);
                    component.Add(stackChild);
                }

                componentNodes.Add(component);
            }

            Console.WriteLine($"Articulation points: {string.Join(", ", result)}");
            Console.WriteLine($"Number of bi-connected components: {componentNodes.Count}");
            Console.WriteLine("Components:");
            for (int i = 0; i < componentNodes.Count; i++)
            {
                Console.WriteLine(string.Join(" ", componentNodes[i]));
            }
        }

        private static void FindArticolationPoints(int node, int depth)
        {
            visited.Add(node);
            lowestPoints[node] = depth;
            depths[node] = depth;
            var childrenCount = 0;

            foreach (var child in graph[node])
            {
                if (!visited.Contains(child))
                {
                    stack.Push(node);
                    stack.Push(child);

                    parents[child] = node;
                    childrenCount += 1;

                    FindArticolationPoints(child, depth + 1);

                    if ((parents[node] == -1 && childrenCount > 1) || 
                        (parents[node] != -1 && lowestPoints[child] >= depth))
                    {
                        children++;
                        result.Add(node);

                        var component = new HashSet<int>();

                        while (true)
                        {
                            var stackChild = stack.Pop();
                            var stackNode = stack.Pop();

                            component.Add(stackNode);
                            component.Add(stackChild);

                            if (stackNode == node && stackChild == child)
                            {
                                break;
                            }
                        }

                        componentNodes.Add(component);
                    }

                    lowestPoints[node] = Math.Min(lowestPoints[node], lowestPoints[child]);
                }
                else if (parents[node] != child && depths[child] < lowestPoints[node])
                {
                    lowestPoints[node] = depths[child];
                }
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
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
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
