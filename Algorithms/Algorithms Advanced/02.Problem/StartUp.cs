using System;
using System.Linq;
using System.Collections.Generic;
using Wintellect.PowerCollections;

namespace _02.Problem
{
    public class Edge
    {
        public Edge(int first, int second, TimeSpan weight)
        {
            this.First = first;
            this.Second = second;
            this.Weight = weight;
        }

        public int First { get; set; }

        public int Second { get; set; }

        public TimeSpan Weight { get; set; }
    }

    public class StartUp
    {
        private static Dictionary<int, List<Edge>> graph = new Dictionary<int, List<Edge>>();
        private static TimeSpan[] times;
        private static bool[] visited;

        static void Main()
        {
            int rooms = int.Parse(Console.ReadLine());

            var exits = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            int edgesCount = int.Parse(Console.ReadLine());

            ReadGraph(rooms, edgesCount);

            var maxNode = graph.Keys.Max();

            times = new TimeSpan[maxNode + 1];

            for (int i = 0; i <= maxNode; i++)
            {
                times[i] = TimeSpan.MaxValue;
            }

            for (int i = 0; i < exits.Length; i++)
            {
                    times[exits[i]] = TimeSpan.Zero;
            }

            for (int i = 0; i < exits.Length; i++)
            {
                int exit = exits[i];
                for (int start = 0; start < rooms; start++)
                {
                    if (start == exit)
                    {
                        continue;
                    }

                    FindMinimalCost(exit, start);
                }
            }

            var max = Console.ReadLine()
                .Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            TimeSpan maxTime = new TimeSpan(0, 0, int.Parse(max[0]), int.Parse(max[1]));

            for (int i = 0; i < rooms; i++)
            {
                if (times[i] == TimeSpan.MaxValue)
                {
                    Console.WriteLine($"Unreachable {i} (N/A)");
                }
                else if (times[i] > maxTime)
                {
                    Console.WriteLine($"Unsafe {i} ({times[i]})");
                }
                else if (times[i] == TimeSpan.Zero)
                {
                    continue;
                }
                else
                {
                    Console.WriteLine($"Safe {i} ({times[i]})");
                }
            }
        }

        private static void FindMinimalCost(int start, int end)
        {
            var queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => times[f].CompareTo(times[s])));
            visited = new bool[graph.Keys.Max() + 1];

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

                    if (!visited[child])
                    {
                        visited[child] = true;
                        queue.Add(child);
                    }

                    TimeSpan w = edge.Weight;
                    TimeSpan d = times[minDistanceNode];

                    TimeSpan r = w.Add(d);
                    var min = r.TotalMinutes;

                    if (times[child] > r )
                    {
                        times[child] = r;

                        queue = new OrderedBag<int>(queue, Comparer<int>.Create((f, s) => times[f].CompareTo(times[s])));
                    } 
                }
            }
        }

        private static void ReadGraph(int rooms, int edgesCount)
        {
            for (int i = 0; i < rooms; i++)
            {
                graph[i] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var edgeInfo = Console.ReadLine()
                    .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var first = int.Parse(edgeInfo[0]);
                var second = int.Parse(edgeInfo[1]);

                var timespan = edgeInfo[2]
                    .Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();
                TimeSpan time = new TimeSpan(0, 0, int.Parse(timespan[0]), int.Parse(timespan[1]));
                var edge = new Edge(first, second, time);

                graph[first].Add(edge);
                graph[second].Add(edge);
            }
        }
    }
}
