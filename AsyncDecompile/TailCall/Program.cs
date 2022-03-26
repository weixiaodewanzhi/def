using System;

namespace TailCall
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestFibo();
            //TestFactorial(7000);

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        static void TestFibo()
        {
            TestFibo(5);
            TestFibo(50);
            TestFibo(500);
            TestFibo(5000);
            TestFibo(50000);
            TestFibo(500000);
            TestFibo(5000000);
            //TestFibo(10000000);
        }

        static void TestFibo(int num)
        {
            Console.WriteLine("参数,num=" + num);
            TimeWrraper("FiboItem.Get", FiboItem.Get, num);
            TimeWrraper("Fibonacci.Get", Fibonacci.Get, num);
            TimeWrraper("Fibonacci2.Get", Fibonacci2.Get, num);
            TimeWrraper("FiboMatrix.Get", FiboMatrix.Get, num);
            Console.WriteLine();
        }

        static void TestFactorial(int idx)
        {
            var res1 = TimeWrraper("Factorial.Normal", Factorial.Normal, 120000);
            var res2 = TimeWrraper("Factorial.Recursion", Factorial.Recursion, 8000);
            var res3 = TimeWrraper("Factorial.TailRecursion", Factorial.TailRecursion, 6800);
            var res4 = TimeWrraper("Factorial.FContinuation", Factorial.FRecursively, 6000);
        }

        static Tres TimeWrraper<Tpar, Tres>(string str, Func<Tpar, Tres> func, Tpar par)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Tres res = default(Tres);
            Exception exp = null;
            try
            {
                res = func(par);
            }
            catch (Exception ex)
            {
                exp = ex;
            }
            sw.Stop();

            var resStr = res?.ToString() ?? "";
            resStr = resStr.Length > 20 ? resStr.Substring(0, 20) : resStr;
            Console.WriteLine($"方法{str}耗时:{sw.ElapsedMilliseconds},前20位结果为:{resStr}");
            if (exp != null)
            {
                Console.WriteLine($"方法{str}执行错误:{exp.Message}");
            }
            return res;
        }
    }
}
