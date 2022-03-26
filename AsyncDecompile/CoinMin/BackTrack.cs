using System;
using System.Collections.Generic;
using System.Linq;

namespace CoinMin
{
    partial class BackTrack
    {
        partial void Log()
        {
            var allocateMsg = string.Join("+", Com.DicRadixAllocate.Select(x => x.Key + "*" + x.Value).ToArray());
            var msg = $"TotalCount={this.Cons.TotalCount},action={Next.Action},allocate={allocateMsg}";
            System.Console.WriteLine(msg);
        }
    }

    partial class BackTrack
    {
        public static Dictionary<int, int> FromValue(int[] coins, int val)
        {
            var dicRadixMax = RadixHelper.GetRadixMax(coins, val); // 获取基数进制的最大值
            var redixs = dicRadixMax.Keys.ToArray();
            var minCount = dicRadixMax.First().Value;
            var maxCount = dicRadixMax.Sum(x => x.Value);
            for (int curCount = minCount; curCount <= maxCount; curCount++)
            {
                var backTrack = new BackTrack()
                {
                    Cons = Constraint.Create(val, curCount, dicRadixMax),
                    Com = RadixCombine.Create(),
                    Next = NextAction.Create(redixs),
                };
                var res = backTrack.Calc();
                if (res != null)
                {
                    return res;
                }
            }

            return null;
        }

        public Constraint Cons { get; set; } // 条件约束
        public RadixCombine Com { get; set; } // 基数组合
        public NextAction Next { get; set; } // 将要下一步的状态

        #region 状态控制
        public Dictionary<int, int> Calc()
        {
            while (!Next.Terminal)
            {
                Log();
                // 回退
                if (Next.Action == ActionStatus.Back)
                {
                    GoBack();
                    continue;
                }
                // 前进或平移
                if (Next.Action == ActionStatus.Next || Next.Action == ActionStatus.Tran)
                {
                    GoNext();
                    continue;
                }
                throw new ArgumentOutOfRangeException();
            }
            Log();
            return Next.Action == ActionStatus.Sucess ? Com.DicRadixAllocate : null;
        }

        partial void Log();

        public void GoBack()
        {
            // 走到顶端,错误
            if (Next.RadixIdx == -1)
            {
                Next.GoFail();
                return;
            }

            var curRadix = Next.Radix;
            var curRadixCnt = Com.DicRadixAllocate[curRadix];

            // 当前是最小值,继续回退
            if (curRadixCnt == Cons.DicRadixRange[curRadix].min)
            {
                Com.Pop(curRadix);
                Cons.PopRange(curRadix);
                Next.GoBack();
                return;
            }
            else // 减少当前基数的分配
            {
                Com.Pop(curRadix);
                Com.Push(curRadix, curRadixCnt - 1);
                Next.GoTranslate();
                return;
            }
        }

        public void GoNext()
        {
            if (Next.RadixIdx >= Next.Radixs.Length) // 已经分配到最小基数依然没有成功
            {
                Next.GoFail();
                return;
            }

            var curRadix = Next.Radix;
            var range = RadixHelper.GetRadixRange(Cons, Com, curRadix);
            if (range.Max < range.Min) // 范围错误回退
            {
                Next.GoBack();
                return;
            }

            // 先分配资源
            Cons.PushRange(curRadix, range);
            Com.Push(curRadix, range.Max);
            Log();

            // 计算剩余资源
            var rest = Com.GetRest(Cons);

            // 正好分配
            if (rest.Value == 0 && rest.Count == 0)
            {
                Next.GoSucess();
                return;
            }
            else if (rest.Value <= 0 || rest.Count <= 0) // 缺少资源,状态回退
            {
                Cons.PopRange(curRadix);
                Com.Pop(curRadix);
                Next.GoBack();
                return;
            }
            else // 继续下一步
            {
                Next.GoNext();
                return;
            }
        }

        #endregion
    }
}
