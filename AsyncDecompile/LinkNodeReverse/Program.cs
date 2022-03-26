using System;
using System.Collections.Generic;
using System.Linq;
namespace LinkNodeReverse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ReservseTest(isInplace: false); // 非原地排序
            ReservseTest(isInplace: true); // 原地排序
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        static void ReservseTest(bool isInplace)
        {
            var lst = new List<LinkNode<int>>();
            lst.Add(new LinkNode<int>(2));
            lst.Add(new LinkNode<int>(5));
            lst.Add(new LinkNode<int>(3));
            lst.Add(new LinkNode<int>(4));
            lst.Add(new LinkNode<int>(7));
            lst.Add(new LinkNode<int>(8));
            for (int i = 0; i < lst.Count - 1; i++)
            {
                lst[i].Next = lst[i + 1];
            }
            var head = lst[0];

            var valhead1 = string.Join("->", LinkNode<int>.GetValue(head));
            var rev = LinkNode<int>.Reverse(head, isInplace);
            var valhead2 = string.Join("->", LinkNode<int>.GetValue(head));
            var valrev = string.Join("->", LinkNode<int>.GetValue(rev));

            Console.WriteLine();
            Console.WriteLine($"反转测试,参数:isInplace={isInplace};");
            Console.WriteLine($"反转前,原始列表头:{valhead1};");
            Console.WriteLine($"反转后,原始列表头:{valhead2};");
            Console.WriteLine($"反转后,新列表头:{valrev};");
            Console.WriteLine();
        }
    }
}
