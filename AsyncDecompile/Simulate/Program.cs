using System;

namespace Simulate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var queueTest = new VirtualQueueTest();
            var aa = queueTest.Test();
            var stackTest = new VirtualStackTest();
            var bb = stackTest.Test();
            Console.WriteLine("Hello World!");
        }
    }
}
