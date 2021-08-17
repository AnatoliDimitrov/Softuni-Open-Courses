using System;
using System.Collections.Generic;
using System.Transactions;

namespace _02.MaximumTasksAssignment
{
    class StartUp
    {
        private static int[,] matrix;
        private static int[] parents;

        static void Main()
        {
            var people = int.Parse(Console.ReadLine());
            var tasks = int.Parse(Console.ReadLine());

            var size = people + tasks + 2;

            ReadMatrix(people, tasks, size);


            parents = new int[size];
            Array.Fill(parents, -1);

            while (Bfs(0, size - 1))
            {
                ReconstructPath(size - 1);
            }

            //Print();

            for (int i = 1; i <= people; i++)
            {
                for (int j = people + 1; j <= people + tasks; j++)
                {
                    if (matrix[j, i] > 0)
                    {
                        Console.WriteLine((char)(64 + i) + "-" + (j - people));
                    }
                }
            }

            //Console.WriteLine();

            //for (int i = 1; i <= people; i++)
            //{
            //    for (int j = tasks + 1; j <matrix.GetLength(0); j++)
            //    {
            //        if (matrix[i, j] == 1)
            //        {
            //            Console.WriteLine((char)(64 + i) + "-" + (j - tasks));
            //        }
            //    }
            //}
        }

        private static void ReconstructPath(int node)
        {
            while (node != 0)
            {
                var parent = parents[node];
                matrix[node, parent] = 1;
                matrix[parent, node] = 0;
                node = parent;
            }
        }

        private static bool Bfs(int sourse, int destination)
        {
            var visited = new HashSet<int>();
            var queue = new Queue<int>();

            queue.Enqueue(sourse);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node == destination)
                {
                    return true;
                }

                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    if (matrix[node, i] > 0 && !visited.Contains(i))
                    {
                        visited.Add(i);
                        parents[i] = node;
                        queue.Enqueue(i);
                    }
                }
            }

            return false;
        }

        private static void ReadMatrix(int people, int tasks, int size)
        {
            matrix = new int[size, size];

            for (int i = 1; i <= people; i++)
            {
                matrix[0, i] = 1;
            }

            for (int i = people + 1; i < size - 1; i++)
            {
                matrix[i, size - 1] = 1;
            }

            for (int i = 1; i <= people; i++)
            {
                var tasksInfo = Console.ReadLine();

                for (int j = 0; j < tasksInfo.Length; j++)
                {
                    if (tasksInfo[j] == 'Y')
                    {
                        matrix[i, people + j + 1] = 1;
                    }
                }
            }
        }

        private static void Print()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }

                Console.WriteLine();
            }
        }
    }
}
