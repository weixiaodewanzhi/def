using System;
using System.Collections.Generic;
using System.Text;

namespace Simulate
{
    internal class VirtualStack<T>
    {
        public Queue<T> Data = new Queue<T>();
        public Queue<T> Temp = new Queue<T>();

        public void Push(T item)
        {
            Data.Enqueue(item);
        }

        public T Pop()
        {
            if (Data.Count == 0 && Temp.Count == 0)
            {
                throw new Exception("没有元素");
            }
            while (Data.Count > 1)
            {
                Temp.Enqueue(Data.Dequeue());
            }
            var item = Data.Dequeue();
            while (Temp.Count > 0)
            {
                Data.Enqueue(Temp.Dequeue());
            }
            return item;
        }
    }

    internal class VirtualStackTest
    {
        public List<int> Test1()
        {
            var queueitem = new Stack<int>();
            var lst = new List<int>();            
            queueitem.Push(1);
            queueitem.Push(2);
            queueitem.Push(4);
            lst.Add(queueitem.Pop());
            lst.Add(queueitem.Pop());
            queueitem.Push(3);
            queueitem.Push(5);
            queueitem.Push(6);
            lst.Add(queueitem.Pop());
            lst.Add(queueitem.Pop());
            lst.Add(queueitem.Pop());
            queueitem.Push(7);
            lst.Add(queueitem.Pop());
            lst.Add(queueitem.Pop());
            return lst;
        }

        public List<int> Test2()
        {
            var queueitem = new VirtualStack<int>();
            var lst = new List<int>();
            queueitem.Push(1);
            queueitem.Push(2);
            queueitem.Push(4);
            lst.Add(queueitem.Pop());
            lst.Add(queueitem.Pop());
            queueitem.Push(3);
            queueitem.Push(5);
            queueitem.Push(6);
            lst.Add(queueitem.Pop());
            lst.Add(queueitem.Pop());
            lst.Add(queueitem.Pop());
            queueitem.Push(7);
            lst.Add(queueitem.Pop());
            lst.Add(queueitem.Pop());
            return lst;
        }

        public int Test()
        {
            var lst1 = Test1();
            var lst2 = Test2();
            var str1 = string.Join(",", lst1);
            var str2 = string.Join(",", lst2);
            return str1.CompareTo(str2);
        }
    }
}
