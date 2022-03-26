using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CoinMin
{
    internal enum ActionStatus
    {
        Next, // 向下
        Back, // 向上
        Tran, // 平移
        Sucess, // 成功
        Fail, // 失败
    }

    internal class Constraint
    {
        public int TotalValue { get; set; } // 总值
        public int TotalCount { get; set; } // 总数
        public Dictionary<int, int> DicRadixMax { get; set; } // 按照其他高位算出的本基数的最大值,(最小值是0)
        public Dictionary<int, (int max, int min)> DicRadixRange { get; set; }  // 按照其他位分配的本基数的范围        

        public static Constraint Create(int totalValue, int totalCount, Dictionary<int, int> dicRadixMax)
        {
            return new Constraint()
            {
                TotalValue = totalValue,
                TotalCount = totalCount,
                DicRadixMax = dicRadixMax,
                DicRadixRange = new Dictionary<int, (int max, int min)>(),
            };
        }
        public void PopRange(int curRadix) => DicRadixRange.Remove(curRadix);
        public void PushRange(int curRadix, (int max, int min) range) => DicRadixRange.Add(curRadix, range);
    }

    internal class RadixCombine
    {
        public Dictionary<int, int> DicRadixAllocate { get; set; } // 各基数的分配值
        public static RadixCombine Create() => new RadixCombine() { DicRadixAllocate = new Dictionary<int, int>(), };
        public void Pop(int curRadix) => DicRadixAllocate.Remove(curRadix);
        public void Push(int curRadix, int curRadixVal) => DicRadixAllocate.Add(curRadix, curRadixVal);

        public (int Value, int Count) GetRest(Constraint cons)
        {
            var restValue = cons.TotalValue - DicRadixAllocate.Sum(x => x.Key * x.Value);
            var restCount = cons.TotalCount - DicRadixAllocate.Sum(x => x.Value);
            return (restValue, restCount);
        }
    }

    internal class NextAction
    {
        public int RadixIdx { get; set; } // 当前待分配的基数索引
        public ActionStatus Action { get; set; } = ActionStatus.Next; // 动作状态
        public int[] Radixs { get; set; } // 所有基数,从大到小
        public static NextAction Create(int[] radix) => new NextAction() { Action = ActionStatus.Next, RadixIdx = 0, Radixs = radix };
        public bool Terminal { get => Action == ActionStatus.Fail || Action == ActionStatus.Sucess; }
        public int Radix { get => Radixs[RadixIdx]; }
        public void GoNext() { RadixIdx++; Action = ActionStatus.Next; }
        public void GoTranslate() { RadixIdx++; Action = ActionStatus.Tran; }
        public void GoBack() { RadixIdx--; Action = ActionStatus.Back; }
        public void GoSucess() { Action = ActionStatus.Sucess; }
        public void GoFail() { Action = ActionStatus.Fail; }
    }

    internal class RadixHelper
    {
        // 最小公倍数
        public static int GCD(int min, int max)
        {
            int remainder;
            while (min != 0)
            {
                remainder = max % min;
                max = min;
                min = remainder;
            }
            return max;
        }

        // min作为基数取值的上确界
        public static int Sup(int min, int max) => max / GCD(min, max);

        // 各个基数的最大值
        public static Dictionary<int, int> GetRadixMax(int[] radixs, int totalVal)
        {
            var dic = new Dictionary<int, int>();
            var radixAsc = radixs.Where(x => x <= totalVal).Distinct().OrderBy(x => x).ToArray(); // 从小到大,高位在右侧;            
            for (int idx = 0; idx < radixAsc.Length; idx++)
            {
                var curRadix = radixAsc[idx];
                if (curRadix > totalVal)
                {
                    continue;
                }
                // 计算高位的影响;
                var bitMax = totalVal / curRadix;
                for (int idxRight = idx + 1; idxRight < radixAsc.Length; idxRight++)
                {
                    var curMax = Sup(curRadix, radixAsc[idxRight]) - 1;
                    if (curMax < bitMax)
                    {
                        bitMax = curMax;
                    }
                }

                dic.Add(curRadix, bitMax);
            }
            dic = dic.Reverse().ToDictionary(x => x.Key, x => x.Value); // 倒序排列
            return dic;
        }

        // 基数的取值范围
        public static (int Max, int Min) GetRadixRange(Constraint cons, RadixCombine com, int curRadix)
        {
            // 可分配的最大数量
            var curValMax = cons.TotalValue - com.DicRadixAllocate.Sum(x => x.Key * x.Value);
            var maxByVal = curValMax / curRadix; // 通过数值约束
            var maxByCnt = cons.TotalCount - com.DicRadixAllocate.Sum(x => x.Value); // 通过数量约束
            var maxByRasix = cons.DicRadixMax[curRadix]; // 通过基数约束
            var max = Math.Min(Math.Min(maxByVal, maxByCnt), maxByRasix);

            // 可分配的最小数量
            var curValMin = curValMax - cons.DicRadixMax.Where(x => x.Key < curRadix).Sum(x => x.Key * x.Value);
            var minByVal = curValMin / curRadix; // 通过数值约束
            var minByRadix = 0; // 通过基数约束
            var min = Math.Max(minByVal, minByRadix);

            return (max, min);
        }
    }
}
