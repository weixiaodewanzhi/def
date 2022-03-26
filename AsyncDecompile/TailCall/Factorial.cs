using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TailCall
{
    /// <summary>
    /// 数学算法:
    /// https://www.cnblogs.com/wujiechao/p/5601938.html
    /// https://blog.csdn.net/alexingcool/article/details/7997599
    /// 组合数 C-Combination; 排列数 A-Arrangement; 旧教材为P-Permutation
    /// 公式:C(n, r)=n!/(r!* (n-r)!); 当n为偶数时,n!=C(n,2/n).(n/2)!^2 复杂度=(logn)^2
    /// 公式:C(n,r)=C(n-1,r-1)+C(n-1,r); 复杂度=logn;
    /// </summary>
    internal class Factorial
    {
        public static int TimeOverflowBound { get; set; } = 50000000;

        public static BigInteger Normal(int val)
        {
            var res = BigInteger.One;
            for (var cur = 1; cur <= val; cur++)
            {
                res = res * cur;
            }
            return res;
        }

        public static BigInteger Recursion(int val)
        {
            if (val <= 2)
            {
                return val;
            }
            return val * Recursion(val - 1);
        }

        public static BigInteger TailRecursion(int totalIdx)
        {
            return TailRecursion(totalIdx, 1, 1);
        }

        /// <summary>
        /// 尾递归
        /// </summary>
        /// <param name="totalIdx">总共需要计算的</param>
        /// <param name="nextIdx">下一个要计算的</param>
        /// <param name="preResult">当前计算的结果</param>
        /// <returns></returns>
        private static BigInteger TailRecursion(int totalIdx, int nextIdx, BigInteger preResult)
        {
            return nextIdx > totalIdx ? preResult : TailRecursion(totalIdx, nextIdx + 1, nextIdx * preResult);
        }

        public static BigInteger FRecursively(int n, BigInteger res)
        {
            if (n < 2)
            {
                return res;
            }
            return FRecursively(n - 1, res * n);
        }

        public static BigInteger FRecursively(int n)
        {
            return FRecursively(n, 1);
        }
    }
}
