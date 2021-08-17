using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace home
{
    class HomeStart
    {
        static void Main(string[] args)
        {
            List<char> path = Console.ReadLine().ToCharArray().ToList();

            List<char> directions = new List<char>() { 'L', 'R', 'S' };

            List<int> possitions = new List<int>();
            List<StringBuilder> total = new List<StringBuilder>();
            int counter = 0; 
            for (int i = 0; i < path.Count; i++)
            {
                if (path[i] == '*')
                {
                    possitions.Add(i);
                    counter++;
                }
            }

            string word = "LRS";
            Permutation("abc");
        }
        static void Permutation(string rest, string prefix = "")
        {
            if (string.IsNullOrEmpty(rest)) Console.WriteLine(prefix);

            // Each letter has a chance to be permutated
            for (int i = 0; i < rest.Length; i++)
            {
                Permutation(rest.Remove(i, 1), prefix + rest[i]);
            }
        }
    }
}
