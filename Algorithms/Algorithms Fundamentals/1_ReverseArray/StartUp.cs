using System;

namespace _1_ReverseArray
{
    class StartUp
    {
        private static string[] array;
        static void Main()
        {
            array = Console.ReadLine()
                .Split();

            Reverse(0);

            Console.WriteLine(string.Join(" ", array));
        }

        private static void Reverse(int index)
        {
            if (index >= array.Length / 2)
            {
                return;
            }

            var temp = array[index];
            array[index] = array[array.Length - 1 - index];
            array[array.Length - 1 - index] = temp;

            Reverse(index + 1);
        }
    }
}
