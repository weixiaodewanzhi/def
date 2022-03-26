using System;
using System.Collections.Generic;
using System.Text;

namespace LinkNodeReverse
{
    public class LinkNode<T>
    {
        public T Data { get; set; }
        public LinkNode<T> Next { get; set; }

        public LinkNode(T data)
        {
            Data = data;
            Next = null;
        }
        public LinkNode()
        {
            Next = null;
        }

        /// <summary>
        /// 反转
        /// </summary>
        /// <param name="isInplace">是否原地</param>
        /// <returns></returns>
        public static LinkNode<T> Reverse(LinkNode<T> head, bool isInplace = false)
        {
            LinkNode<T> headReverse = null;
            while (head != null)
            {
                var detached = isInplace ? head : new LinkNode<T>(head.Data);
                head = head.Next;
                detached.Next = headReverse;
                headReverse = detached;
            }
            return headReverse;
        }

        public static T[] GetValue(LinkNode<T> head)
        {
            var lst = new List<T>() { };
            while (head != null)
            {
                lst.Add(head.Data);
                head = head.Next;
            }
            return lst.ToArray();
        }
    }
}
