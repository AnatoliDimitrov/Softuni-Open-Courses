using System;
using System.Collections.Generic;

namespace _07_Salaries
{
    class StartUp
    {
        private static List<int>[] graph;
        private static Dictionary<int, int> salaries = new Dictionary<int, int>();

        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            graph = new List<int>[n];

            for (int i = 0; i < n; i++)
            {
                var line = Console.ReadLine();
                graph[i] = new List<int>();

                for (int j = 0; j < n; j++)
                {
                    if (line[j] == 'Y')
                    {
                        graph[i].Add(j);
                    } 
                }
            }

            var totalSalary = 0;

            for (int i = 0; i < n; i++)
            {
                    totalSalary += Dfs(i);
            }

            Console.WriteLine(totalSalary);
        }

        private static int Dfs(int node)
        {
            if (graph[node].Count == 0)
            {
                return 1;
            }

            var salary = 0;
            foreach (var child in graph[node])
            {
                if (salaries.ContainsKey(child))
                {
                    salary += salaries[child];
                }
                else
                {
                    salary += Dfs(child);
                }
            }

            salaries[node] = salary;
            return salary;
        }
    }
}
