using System;
using System.Linq;
using System.Collections.Generic;

namespace CoinMin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var bb = BackTrack.FromValue(new int[] { 10,  4, 2 }, 100009);
            var cc = bb?.Count;
        }
    }
}
