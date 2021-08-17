namespace _5.Problem
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    class SoftJump
    {
        static void Main()
        {
            string[] input = Console.ReadLine().Split(" ");

            int rows = int.Parse(input[0]);
            int cols = int.Parse(input[1]);

            List<List<char>> matrix = new List<List<char>>();

            for (int i = 0; i < rows; i++)
            {
                List<char> row = Console.ReadLine().ToCharArray().ToList();
                matrix.Add(row);
            }

            int[] index = new int[] { rows - 1, matrix[rows - 1].IndexOf('S') };
            //Console.WriteLine(string.Join(" ", index));

            int readings = int.Parse(Console.ReadLine());

            int jumps = 0;
            for (int i = 0; i < readings; i++)
            {
                int[] command = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
                int matrixRow = command[0];
                int steps = command[1];
                for (int h = 0; h < steps; h++)
                {
                    char temp = matrix[matrixRow][cols - 1];
                    matrix[matrixRow].RemoveAt(cols - 1);
                    matrix[matrixRow].Insert(0, temp);
                    
                }
                if (matrix[index[0] - 1][index[1]] == '-')
                {
                    jumps++;
                    int tmpindex = matrix[index[0]].IndexOf('S');
                    if (index[0] == rows - 1)
                    {

                        matrix[index[0]][tmpindex] = '0';
                    }
                    else
                    {

                        matrix[index[0]][tmpindex] = '-';
                    }

                    matrix[index[0] - 1][tmpindex] = 'S';
                    index[0]--;
                }
            }
            for (int i = 0; i < rows - 1; i++)
            {
               
            }

            if (matrix[0].Contains('S'))
            {
                Console.WriteLine("Win");
            }
            else
            {
                Console.WriteLine("Lose");
            }

            Console.WriteLine($"Total Jumps: {jumps}");

            for (int j = 0; j < rows; j++)
            {
                for (int g = 0; g < cols; g++)
                {
                    Console.Write(matrix[j][g]);
                }
                Console.WriteLine();
            }
        }
    }
}
