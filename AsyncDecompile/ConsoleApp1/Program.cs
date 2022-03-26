using System;
using System.Linq;

namespace ConsoleApp1
{
    class TreeNode
    {
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public bool hasChildren()
        {
            return false;
        }

        public int LeftHeight { get; set; }
        public int RightHeight { get; set; }

        public bool ClacHeightAndIsBanlce()
        {
            if (this.Left == null)
            {
                if (Right.hasChildren())
                {
                    return false;
                }
                LeftHeight = 1;
            }
            if (this.Right == null)
            {
                if (Left.hasChildren())
                {
                    return false;
                }
                RightHeight = 1;
            }

            this.Left.ClacHeightAndIsBanlce();
            this.Right.ClacHeightAndIsBanlce();
            if (System.Math.Abs(LeftHeight - RightHeight) > 1)
            {
                return false;
            }
            return true;
        }


    }



    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static int Calc(int[] ary, int i, int j)
        {
            // j>i;
            // return ary[i] + i + ary[j] - j
            // j<i
            //  return ary[i] - i + ary[j] + j
            return 0;
        }

        public static int? GetMax(int[] ary)
        {
            //var ary = new int[] { 1, 2, 5, 8, -1 };
            var aidic = ary.ToDictionary(x => x, x => ary[x] + x);
            var ajdic = ary.ToDictionary(x => x, x => ary[x] - x);
            var res = from ai in aidic
                      from aj in ajdic
                      where ai.Key != aj.Key
                      select ai.Value + aj.Value;
            return res.Max();

            //ary[i] - i;

            var maxi1 = default(int?);
            var maxi2 = default(int?);

            for (int i = 0; i < ary.Length; i++)
            {
                var tmpi1 = ary[i] + i;
                var tmpi2 = ary[i] - i;

                if (maxi1 == null || maxi1 < tmpi1)
                {
                    maxi1 = tmpi1;
                }
                if (maxi2 == null || maxi2 < tmpi2)
                {
                    maxi2 = tmpi2;
                }
            }


            return maxi1 + maxi2;
        }
    }
}
