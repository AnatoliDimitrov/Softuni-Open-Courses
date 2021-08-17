using Wintellect.PowerCollections;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int k, int[] cookies)
        {
            OrderedBag<int> bag = new OrderedBag<int>(cookies);

            int counter = 0;

            while (bag.GetFirst() < k && bag.Count > 1)
            {
                int firstCookie = bag.RemoveFirst();
                int secondCookie = bag.RemoveFirst();

                bag.Add(firstCookie + (2 * secondCookie));

                counter++;
            }

            int first = bag.GetFirst();

            if (first > k)
            {
                return counter;
            }
            else
            {
                return -1;
            }
        }
    }
}
