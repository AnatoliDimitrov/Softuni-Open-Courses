using System;

namespace _10_WordDifferences
{
    class StartUp
    {
        private static int[,] matrix;

        static void Main()
        {
            string firstWord = Console.ReadLine();
            string secondWord = Console.ReadLine();

            matrix = new int[firstWord.Length + 1, secondWord.Length + 1];

            var count = GetCount(firstWord, secondWord);

            Console.WriteLine($"Deletions and Insertions: {count}");
        }

        private static int GetCount(string firstWord, string secondWord)
        {
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                matrix[i, 0] = i;
            }

            for (int i = 1; i < matrix.GetLength(1); i++)
            {
                matrix[0, i] = i;
            }

            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    if (firstWord[i - 1] == secondWord[j - 1])
                    {
                        matrix[i, j] = matrix[i - 1, j - 1];
                    }
                    else
                    {
                        matrix[i, j] = Math.Min(matrix[i - 1, j], matrix[i, j - 1]) + 1;
                    }
                }
            }
            return matrix[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
        }
    }
}
