using System;

namespace HalfMore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ary = new int[] { 1, 2, 1, 2, 3, 3, 3, 3, 1, 3 };
            var winner = Arena.GetWinner(ary);
            Console.WriteLine("Hello World!");
        }

    }
}
