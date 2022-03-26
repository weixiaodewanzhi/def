using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Memleak
{
    /// <summary>
    /// 事件声明类
    /// </summary>
    internal class EventDeclare
    {
        /// <summary>
        /// 定义事件
        /// </summary>
        public event EventHandler<EventArgs> OnChanged;

        /// <summary>
        /// 调用事件
        /// </summary>
        public void Invoke()
        {
            if (OnChanged != null)
            {
                OnChanged.Invoke(this, EventArgs.Empty);
            }
        }
    }

    /// <summary>
    /// 事件转化为弱事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WeakEventHandler<T>
    {
        private readonly WeakReference _targetReference; // 原事件的调用者
        private readonly System.Reflection.MethodInfo _method; // 原事件的方法

        public WeakEventHandler(EventHandler<T> callback)
        {
            _method = callback.Method;
            _targetReference = new WeakReference(callback.Target, true);
        }

        /// <summary>
        /// 使用弱引用构建委托方法,执行;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Handler(object sender, T e)
        {
            var target = _targetReference.Target;
            if (target == null)
            {
                return;
            }

            var callback = (Action<object, T>)Delegate.CreateDelegate(typeof(Action<object, T>), target, _method, true);
            if (callback == null)
            {
                return;
            }
            callback(sender, e);
        }
    }

    /// <summary>
    /// 事件实现类
    /// </summary>
    internal class EventImplement : IDisposable
    {
        private int[] inner = null;
        private EventDeclare _SubscribeClass = null;

        public EventImplement(EventDeclare cls)
        {
            inner = new int[1024 * 1024]; // 加这个只是为了显著的看出是否释放此对象;
            _SubscribeClass = cls;

            // 解决方式1:使用弱引用代替原始引用;
            if (TestClass.UseWeakEvent)
            {
                _SubscribeClass.OnChanged += new WeakEventHandler<EventArgs>(OnChanged).Handler;
            }
            else
            {
                _SubscribeClass.OnChanged += OnChanged;
            }
        }

        private void OnChanged(object sender, EventArgs e)
        {
            // 解决方式2:在此处取消事件;缺点:不能多次触发事件了;
            if (TestClass.RemoveEventInHandler)
            {
                _SubscribeClass.OnChanged -= OnChanged;
                _SubscribeClass = null;
            }
        }

        public void Dispose()
        {
            // 解决方式3:在此处取消事件;
            if (TestClass.RemoveEventInDispose)
            {
                _SubscribeClass.OnChanged -= OnChanged;
                _SubscribeClass = null;
            }
        }

        public void DoSomeThing()
        {

        }

        ~EventImplement()
        {
            // 当EventConfig.GcRightNow时才调用到这里;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }

    internal class TestClass
    {
        public static bool UseWeakEvent = false;
        public static bool RemoveEventInDispose = false;
        public static bool RemoveEventInHandler = false;
        public static bool GcRightNow = false;
        public static bool UseDispose = false;

        public static void Config(int condition)
        {
            switch (condition)
            {
                case 1:
                    // 不释放
                    RemoveEventInDispose = true;
                    break;
                case 2:
                    // 释放
                    RemoveEventInDispose = true;
                    UseDispose = true;
                    break;
                case 3:
                    // 释放
                    RemoveEventInHandler = true;
                    break;
                case 4:
                    // 释放
                    UseWeakEvent = true;
                    break;
                case 5:
                    // 释放
                    UseWeakEvent = true;
                    GcRightNow = true;
                    break;
                default:
                    // 不释放
                    break;
            }
        }

        public static EventDeclare declare = null;
        public void LeakTest()
        {
            declare = new EventDeclare();
            RefEvent(declare);

            // 立即释放内存
            if (GcRightNow)
            {
                GC.Collect();
                // 挂起当前线程，直到正在处理终结器已清空该队列。
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }
        public void RefEvent(EventDeclare declare)
        {
            if (TestClass.UseDispose)
            {
                using var events = new EventImplement(declare);
                declare.Invoke();
                events.DoSomeThing();
            }
            else
            {
                var events = new EventImplement(declare);
                declare.Invoke();
                events.DoSomeThing();
            }
        }
    }
}
