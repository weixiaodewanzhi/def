using System;
using System.Collections.Generic;
using System.Text;

namespace HalfMore
{
    internal class Arena
    {
        public int Winner { get; private set; }
        public int WinnerCount { get; private set; }

        public override string ToString()
        {
            return $"Winner={Winner},Count={WinnerCount}";
        }

        public void SetWinner(int winner)
        {
            Winner = winner;
            WinnerCount = 1;
        }

        public void PK(int athlete)
        {
            if (Winner == athlete)
            {
                // 擂主加成
                WinnerCount++;
                return;
            }

            // 擂主减员
            WinnerCount--;

            // 换擂主
            if (WinnerCount == 0)
            {
                SetWinner(athlete);
            }
        }

        public static bool Valid(int[] ary, int num)
        {
            var count = 0;
            for (int i = 0; i < ary.Length; i++)
            {
                if (num == ary[i])
                {
                    count++;
                }
            }
            return count > ary.Length / 2;
        }

        public static int? GetWinner(int[] ary)
        {
            var length = ary.Length;
            if (length == 0)
            {
                throw new ArgumentException("数组错误");
            }

            if (length == 1)
            {
                return ary[0];
            }

            var halfmore = new Arena();
            halfmore.SetWinner(ary[0]);

            for (int i = 1; i < length; i++)
            {
                halfmore.PK(ary[i]);
            }

            return Valid(ary, halfmore.Winner) ? halfmore.Winner : default(int?);
        }
    }
}
