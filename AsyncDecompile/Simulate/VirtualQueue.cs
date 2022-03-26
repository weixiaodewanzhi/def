using System;
using System.Collections.Generic;
using System.Text;

namespace Simulate
{
    internal class VirtualQueue<T>
    {
        public Stack<T> Data = new Stack<T>();
        public Stack<T> Temp = new Stack<T>();
        public void Enqueue(T item)
        {
            Data.Push(item);
        }

        public T Dequeue()
        {
            if (Data.Count == 0 && Temp.Count == 0)
            {
                throw new Exception("没有元素");
            }

            while (Data.Count > 1)
            {
                Temp.Push(Data.Pop());
            }
            var item = Data.Pop();
            while (Temp.Count > 0)
            {
                Data.Push(Temp.Pop());
            }
            return item;
        }
    }

    internal class VirtualQueueTest
    {
        public List<int> Test1()
        {
            var lst = new List<int>();
            var queueReal = new Queue<int>();
            queueReal.Enqueue(1);
            queueReal.Enqueue(2);
            queueReal.Enqueue(4);
            lst.Add(queueReal.Dequeue());
            lst.Add(queueReal.Dequeue());
            queueReal.Enqueue(3);
            queueReal.Enqueue(5);
            queueReal.Enqueue(6);
            lst.Add(queueReal.Dequeue());
            lst.Add(queueReal.Dequeue());
            lst.Add(queueReal.Dequeue());
            queueReal.Enqueue(7);
            lst.Add(queueReal.Dequeue());
            lst.Add(queueReal.Dequeue());
            return lst;
        }

        public List<int> Test2()
        {
            var lst = new List<int>();
            var queueReal = new VirtualQueue<int>();
            queueReal.Enqueue(1);
            queueReal.Enqueue(2);
            queueReal.Enqueue(4);
            lst.Add(queueReal.Dequeue());
            lst.Add(queueReal.Dequeue());
            queueReal.Enqueue(3);
            queueReal.Enqueue(5);
            queueReal.Enqueue(6);
            lst.Add(queueReal.Dequeue());
            lst.Add(queueReal.Dequeue());
            lst.Add(queueReal.Dequeue());
            queueReal.Enqueue(7);
            lst.Add(queueReal.Dequeue());
            lst.Add(queueReal.Dequeue());
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
