using System;
using System.Linq;
using System.Collections.Generic;

namespace backtrack
{
    internal class Program
    {


        static void Main(string[] args)
        {
            var a = new MyClass();
            a.backtrack(0);
            a.Show();
            Console.WriteLine("Hello World!");
        }
    }

    public class MyClass
    {
        // 储存被选取的元素
        public const int SelectCount = 3;
        public List<string> SelectItems = new List<string>(SelectCount) { "", "", "" };

        // 可以选取的基
        public readonly string[] BaseItems = new string[] { "a", "b", "c", "d", "e" };
        public const int BaseCount = 5;
        public bool[] BaseUse = new bool[5];

        public List<List<string>> Solutions = new List<List<string>>();

        public void backtrack(int level)
        {
            // 已经选择完毕
            if (level == SelectCount)
            {
                Solutions.Add(SelectItems.Select(x => x).ToList());
                return;
            }

            // 从基数中选择
            for (int idxBase = 0; idxBase < 5; idxBase++)
            {
                if (!BaseUse[idxBase])
                {
                    BaseUse[idxBase] = true;
                    SelectItems[level] = BaseItems[idxBase];
                    backtrack(level + 1); //进入到下一个维度
                    BaseUse[idxBase] = false;
                }
            }
        }

        public void Show()
        {
            var ary = Solutions.Select(x => string.Join(",", x.ToArray())).ToArray();
            var msg = string.Join(Environment.NewLine, ary);
            Console.WriteLine("结果为:");
            Console.WriteLine(msg);
        }
    }
}
