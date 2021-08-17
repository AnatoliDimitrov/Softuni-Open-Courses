using System;
using System.Collections.Generic;
using System.Linq;

namespace _07_Molecules
{
    class StartUp
    {
        private static List<int>[] moleculs;
        private static HashSet<int> visited = new HashSet<int>();
        private static HashSet<int> all = new HashSet<int>();

        static void Main()
        {
            int moleculsCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            moleculs = new List<int>[moleculsCount + 1];

            for (int i = 0; i < moleculsCount + 1; i++)
            {
                moleculs[i] = new List<int>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var edge = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                int from = edge[0];
                int to = edge[1];

                all.Add(from);
                all.Add(to);

                moleculs[from].Add(to);
            }

            int start = int.Parse(Console.ReadLine());

            Dfs(start);

            all.ExceptWith(visited);

            Console.WriteLine(string.Join(" ", all));
        }

        private static void Dfs(int start)
        {
            if (visited.Contains(start))
            {
                return;
            }

            visited.Add(start);

            foreach (var child in moleculs[start])
            {
                if (visited.Contains(child))
                {
                    continue;
                }
                Dfs(child);
            }
        }
    }
}
