using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AsyncTest
{


    public class Program
    {
        public static Task Main3(string[] args)
        {
            return Task.Run(() =>
            {
                Console.WriteLine("Main3");
            });
        }



        static async Task DoAsync()
        {
            await Task.Run(async () =>
            {
                await Task.Delay(1000);
            });
        }

        public static async Task<int> Main(string[] args)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await Task.CompletedTask;
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            Task aa = DoAsync();
            try
            {
                aa.Wait();
                //Test8();
                // await Test7();
            }
            catch (Exception ex)
            {
                var asda = ex;
            }
            //var aa = Test();
            //var bb = aa.GetAwaiter().GetResult();
            //var cc = await Test();
            var dd = Console.ReadLine();
            //return cc;
            return 1;
        }

        public async static Task<int> Test()
        {
            Console.WriteLine("Begin Main");
            var res = 1;
            await Task.Yield();
            Console.WriteLine("End Main");
            return res;
        }

        //public async static int Test2()
        //{
        //    Console.WriteLine("Begin Main");
        //    var res = 1;
        //    await Task.Yield();
        //    Console.WriteLine("End Main");
        //    return res;
        //}

        public async static void Test3()
        {
            Console.WriteLine("Begin Main");
            await Task.Yield();
            Console.WriteLine("End Main");
            // return res;
        }

        public static void Test5()
        {
            Console.WriteLine("Begin Main");
            // await Task.Yield();
            Console.WriteLine("End Main");
            // return res;
        }

        public async static Task Test4()
        {
            await Task.Run(() => Test5());
            Console.WriteLine("Begin Main");
            // await Task.Yield();
            Console.WriteLine("End Main");
            // return res;
        }

        public async static Task Test6()
        {
            Task.Run(() => Test5());
            Console.WriteLine("Begin Main");
            // await Task.Yield();
            Console.WriteLine("End Main");
            // return res;
        }

        public async static Task Test7()
        {
            throw new NotImplementedException();
        }

        public static void Test8()
        {
            Task.Run(() => throw new NotImplementedException());
        }
    }
}
