using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TailCall
{
    internal class FiboItem
    {
        public int Idx { get; set; }
        public BigInteger curVal { get; set; }
        public BigInteger preVal { get; set; }

        public static FiboItem GetSeed() => new FiboItem()
        {
            Idx = 1,
            curVal = 1,
            preVal = 0,
        };

        public void AddOne()
        {
            var val = this.curVal + this.preVal;
            this.preVal = this.curVal;
            this.curVal = val;
            this.Idx += 1;
        }

        public static int TimeOverflowBound { get; set; } = 500000;

        public static BigInteger Get(int num)
        {
            if (num >= TimeOverflowBound)
            {
                return -2;
            }
            var cur = GetSeed();
            while (cur.Idx < num)
            {
                cur.AddOne();
            }
            return cur.curVal;
        }
    }

    /// <summary>
    /// 不推荐,调用栈
    /// </summary>
    internal class Fibonacci
    {
        public static int StackOverflowBound { get; set; } = 50000;

        public static BigInteger Get(int num)
        {
            if (num >= StackOverflowBound)
            {
                return -1;
            }
            var totalIdx = num;
            int curIdx = 1;
            BigInteger curVal = 1;
            BigInteger preVal = 0;
            return TailRecursion(ref totalIdx, ref curIdx, ref curVal, ref preVal);
        }

        private static BigInteger TailRecursion(ref int totalIdx, ref int curIdx, ref BigInteger curVal, ref BigInteger preVal)
        {
            if (curIdx < totalIdx)
            {
                var tmp = curVal + preVal;
                preVal = curVal;
                curVal = tmp;
                curIdx += 1;
                return TailRecursion(ref totalIdx, ref curIdx, ref curVal, ref preVal);
            }
            else
            {
                return curVal;
            }
        }
    }

    /// <summary>
    /// 不推荐,调用栈
    /// </summary>
    internal class Fibonacci2
    {
        public static int StackOverflowBound { get; set; } = 50000;

        public static BigInteger Get(int num)
        {
            if (num >= StackOverflowBound)
            {
                return -1;
            }
            var acc = new BigInteger[] { 1, 0 };
            return TailRecursion(num - 1, acc);
        }

        private static BigInteger TailRecursion(int cycleCount, BigInteger[] acc)
        {
            if (cycleCount == 0) return acc[0];
            var accNew = acc[0] + acc[1];
            acc[1] = acc[0];
            acc[0] = accNew;
            return TailRecursion(cycleCount - 1, acc);
        }
    }

    /// <summary>
    /// 按位计算斐波那契矩阵的幂;
    /// </summary>
    internal class FiboBitPowerMatrix
    {
        protected static Dictionary<BigInteger, FiboMatrix> dicPower { get; private set; } = new Dictionary<BigInteger, FiboMatrix>()
        {
            // 斐波那契矩阵
            [0] = new FiboMatrix()
            {
                M11 = 1,
                M12 = 1,
                M21 = 1,
                M22 = 0,
                CurPower = 1,
            }
        };

        /// <summary>
        /// 按位指定的整幂矩阵,从0开始,如第3位就是2^(3+1)次幂;
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public static FiboMatrix GetBitPower(int idx)
        {
            if (!dicPower.ContainsKey(idx))
            {
                var preMat = GetBitPower(idx - 1);
                dicPower[idx] = preMat * preMat;
            }
            return dicPower[idx];
        }
    }

    /// <summary>
    /// 数据量大时性能最优.3秒内50w;
    /// </summary>
    internal class FiboMatrix
    {
        #region 自有属性
        public int CurPower { get; set; } = 0;
        public BigInteger M11 { get; set; }
        public BigInteger M12 { get; set; }
        public BigInteger M21 { get; set; }
        public BigInteger M22 { get; set; }
        #endregion

        #region 乘法
        private static BigInteger Plus(BigInteger a1, BigInteger a2, BigInteger b1, BigInteger b2)
        {
            if (a2 == b1)
            {
                return (a1 + b2) * a2;
            }
            return a1 * b1 + a2 * b2;
        }

        public void SetM12(FiboMatrix a, FiboMatrix b)
        {
            M12 = Plus(a.M11, a.M12, b.M12, b.M22);
        }

        public void SetM22(FiboMatrix a, FiboMatrix b)
        {
            M22 = Plus(a.M21, a.M22, b.M12, b.M22);
        }

        public void SetOther(FiboMatrix a, FiboMatrix b)
        {
            // | F(n+1),F(n) |
            // | F(n), F(n-1) |
            // 优化算法
            this.M21 = this.M12;
            this.M11 = this.M21 + this.M22;
            this.CurPower = a.CurPower + b.CurPower;
        }

        public static FiboMatrix operator *(FiboMatrix a, FiboMatrix b)
        {
            var res = new FiboMatrix();
            if (a.M11 > int.MaxValue || b.M11 > int.MaxValue)
            {
                var calc = new Task[]{
                 Task.Run(() => res.SetM12(a,b)),
                 Task.Run(() => res.SetM22(a,b)),
                };

                Task.WaitAll(calc);
            }
            else
            {
                res.SetM12(a, b);
                res.SetM22(a, b);
            }

            res.SetOther(a, b);
            return res;
        }

        #endregion

        #region 常量

        /// <summary>
        /// 单位矩阵
        /// </summary>
        public static readonly FiboMatrix I = new FiboMatrix()
        {
            M11 = 1,
            M22 = 1,
            CurPower = 0,
        };
        #endregion

        public static FiboMatrix GetPower(int num)
        {
            var res = FiboMatrix.I; // 用于保存最终结果
            var bitMap = new string(Convert.ToString(num, 2).Reverse().ToArray()); // 反序2进制,按位相乘
            Task curTask = Task.Run(() => { }); // 空任务,作为延续后续任务的基础
            for (int idx = 0; idx < bitMap.Length; idx++)
            {
                var bitPower = FiboBitPowerMatrix.GetBitPower(idx);

                // 如果此位被设置,则并行计算非整幂矩阵
                if (bitMap[idx] == '1')
                {
                    curTask = curTask.ContinueWith((task, obj) =>
                    {
                        var mat = (FiboMatrix)obj;
                        res = res * mat;
                    }, bitPower);
                }
            }

            // 等待非整幂矩阵计算完毕
            curTask.Wait();

            return res;
        }

        public static BigInteger Get(int num)
        {
            var mat = GetPower(num - 1);
            return mat.M11;
        }

        public override string ToString()
        {
            var res = $"{M11},{M12},{M21},{M22}";
            return res;
        }
    }
}
