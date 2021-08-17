using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.RoadTrip
{
    class StartUp
    {
        private static int[,] matrix;
        private static bool[,] beated;
        private static int[] enemyEnergyPoints;
        private static int[] enemyBattlePoints;

        static void Main()
        {
            enemyEnergyPoints = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            enemyBattlePoints = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var energy = int.Parse(Console.ReadLine());

            var maxBattlePoints = FindMaxBattlePoints(energy);

            Console.WriteLine($"Maximum value: {maxBattlePoints}");

            var stack = new Stack<int>();

            var col = energy;

            //for (int i = 0; i < matrix.GetLength(0); i++)
            //{
            //    for (int j = 0; j < matrix.GetLength(1); j++)
            //    {
            //        Console.Write(matrix[i, j] + " ");
            //    }

            //    Console.WriteLine();
            //}

            //for (int i = matrix.GetLength(0) - 1; i >= 0; i--)
            //{
            //    if (beated[i, col])
            //    {
                    
            //        stack.Push(i - 1);
            //        col -= enemyEnergyPoints[i - 1];
            //    }
            //}

            //Console.WriteLine(string.Join(" ", stack));
        }

        private static int FindMaxBattlePoints(int energy)
        {
            matrix = new int[enemyBattlePoints.Length + 1, energy + 1];
            beated = new bool[enemyBattlePoints.Length + 1, energy + 1];

            for (int i = 1; i < matrix.GetLength(0); i++)
            
            {
                var enemyPoints = enemyEnergyPoints[i - 1];
                var enemyEnergy = enemyBattlePoints[i - 1];

                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    var skip = matrix[i - 1, j];

                    if (enemyEnergy > j)
                    {
                        matrix[i, j] = skip;
                        continue;
                    }

                    var take = matrix[i - 1, j - enemyEnergy] + enemyPoints;

                    if (take > skip)
                    {
                        matrix[i, j] = take;
                        beated[i, j] = true;
                    }
                    else
                    {
                        matrix[i, j] = skip;
                    }
                }
            }

            return matrix[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
        }
    }
}
