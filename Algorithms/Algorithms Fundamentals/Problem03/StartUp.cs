using System;
using System.Linq;
using System.Collections.Generic;

namespace Problem03
{
    class StartUp
    {
        private static List<int>[] graph;

        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            graph = new List<int>[n];

            ReadGraph(n);

            int p = int.Parse(Console.ReadLine());

            for (int i = 0; i < p; i++)
            {
                int[] path = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                string result = FindPath(path);

                Console.WriteLine(result);
            }
        }

        private static string FindPath(int[] path)
        {
            if (path[0] > graph.Length - 1 || path[0] < 0)
            {
                return "no";
            }

            for (int i = 0; i < path.Length - 1; i++)
            {
                if (graph[path[i]].Contains(path[i + 1]))
                {
                    continue;
                }
                else
                {
                    return "no";
                }
            }

            return "yes";
        }


        private static void ReadGraph(int n)
        {
            for (int j = 0; j < n; j++)
            {
                graph[j] = new List<int>();
            }

            for (int i = 0; i < n; i++)
            {
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    int[] line = input
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray();

                    for (int j = 0; j < line.Length; j++)
                    {
                        graph[i].Add(line[j]);
                    }
                        
                }
            }
        }
    }
}
