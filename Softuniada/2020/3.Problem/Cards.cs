namespace _3.Problem
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    class Cards
    {
        static void Main()
        {
            int countCards = int.Parse(Console.ReadLine());

            List<int> cards = new List<int>();

            for (int i = 1; i <= countCards; i++)
            {
                cards.Add(i);
            }

            List<int> commands = Console.ReadLine().Split(" ").Select(int.Parse).ToList();

            for (int i = 0; i < commands.Count; i++)
            {
                List<int> firstPart = new List<int>();
                List<int> secondPart = new List<int>();
                for (int j = 0; j < cards.Count; j++)
                {
                    if (j < commands[i])
                    {
                        firstPart.Add(cards[j]);
                    }
                    else
                    {
                        secondPart.Add(cards[j]);
                    }
                }

                cards.Clear();

                for (int g = 0; g < countCards; g++)
                {
                    if (g < firstPart.Count)
                    {
                        cards.Add(firstPart[g]);
                    }
                    if (g < secondPart.Count)
                    {
                        cards.Add(secondPart[g]);
                    }
                }
            }

            Console.WriteLine(string.Join(" ", cards));
        }
    }
}
