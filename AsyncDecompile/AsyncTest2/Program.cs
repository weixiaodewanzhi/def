using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTest2
{
    internal class Program
    {
        static async Task Main()
        {
            System.Console.WriteLine($"Main Task ID:{Thread.CurrentThread.ManagedThreadId}");
            System.Console.WriteLine($"Test1");


            // Just continue on this thread, or await with try-catch:
            try
            {
                //var aa = Task.Run(Test1);
                var aa = new Task(Test1);
                aa.GetAwaiter().GetResult();
                ;
                //await aa;
                //var aa = Task.Run(Test1);
                //aa.Wait();
                //await aa;
                //System.Console.WriteLine($"before await");
                //await Task.Run(() => { Test1(); });
                //task.Wait();
                System.Console.WriteLine($"after await");
            }
            catch (IndexOutOfRangeException ex)
            {
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine($"ex IndexOutOfRangeException, Task ID:{Thread.CurrentThread.ManagedThreadId}");
            }
            catch (AggregateException ex)
            {
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine($"ex AggregateException, Task ID:{Thread.CurrentThread.ManagedThreadId}");
            }
            finally
            {
                //...
            }
            System.Console.WriteLine("Reach end.");
            Console.ReadKey();
        }

        public static void Test1()
        {
            System.Console.WriteLine($"In Task.Run(), Task ID:{Thread.CurrentThread.ManagedThreadId},{Thread.CurrentThread.IsThreadPoolThread}");
            int[] vary = new int[5];
            while (true)
            {
                Thread.Sleep(3000);
                int d = vary[6];
            }
        }

        public static Task<int> Test2()
        {
            Test1();
            return Task.FromResult(0);
        }

        public static async Task Test3()
        {
            await Task.Run(() => { Test1(); });
        }

    }
}
