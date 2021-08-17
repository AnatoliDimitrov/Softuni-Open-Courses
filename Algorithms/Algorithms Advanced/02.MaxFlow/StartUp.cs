using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Channels;

namespace _02.MaxFlow
{
    class StartUp
    {
        private static int[,] matrix;
        private static int[] parents;

        static void Main()
        {
            var size = int.Parse(Console.ReadLine());

            matrix = new int[size, size];
            parents = new int[size + 1];
            Array.Fill(parents, -1);

            ReadMatrix();

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            var maxFlow = 0;

            while (Bfs(source, destination))
            {
                var currentFlow = ReconstructPath(source, destination);

                maxFlow += currentFlow;

                DecrementFlow(source, destination, currentFlow);
            }

            Console.WriteLine($"Max flow = {maxFlow}");
        }

        private static void DecrementFlow(int source, int destination, int currentFlow)
        {
            var node = destination;

            while (node != source)
            {
                var parent = parents[node];

                matrix[parent, node] -= currentFlow;

                if (matrix[parent, node] < 0)
                {
                    matrix[parent, node] = 0;
                }

                node = parent;
            }
        }

        private static int ReconstructPath(int source, int destination)
        {
            var node = destination;
            var flow = int.MaxValue;

            while (node != source)
            {
                var parent = parents[node];

                if (matrix[parent, node] < flow)
                {
                    flow = matrix[parent, node];
                }

                node = parent;
            }

            return flow;
        }

        private static bool Bfs(int source, int destination)
        {
            var queue = new Queue<int>();
            var visited = new HashSet<int>();

            visited.Add(source);

            queue.Enqueue(source);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node == destination)
                {
                    return true;
                }

                visited.Add(node);

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[node, i] > 0 && !visited.Contains(i))
                    {
                        queue.Enqueue(i);
                        visited.Add(i);
                        parents[i] = node;
                    }
                }
            }

            return false;
        }

        private static void ReadMatrix()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var line = Console.ReadLine()
                    .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int j = 0; j < line.Length; j++)
                {
                    matrix[i, j] = line[j];
                }
            }
        }
    }
}
