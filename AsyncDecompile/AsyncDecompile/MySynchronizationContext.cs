using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDecompile
{
    public class MySynchronizationContext : SynchronizationContext
    {
        public AsyncLocal<string> AsyncData = new AsyncLocal<string>();
        public string StaticData = "";
        public AsyncLocal<string[]> AsyncDataAry = new AsyncLocal<string[]>();
        public string[] StaticDataAry = new string[1] { "" };


        public static void Init(string str)
        {
            var con = new MySynchronizationContext();
            con.AsyncData.Value = str;
            con.StaticData = str;
            con.AsyncDataAry.Value = new string[1] { str };
            con.StaticDataAry[0] = str;
            SynchronizationContext.SetSynchronizationContext(con);
        }
        public static void Show(string str)
        {
            Console.Write(str);
            var conOri = SynchronizationContext.Current;
            if (conOri == null)
            {
                Console.WriteLine($" Mid={Thread.CurrentThread.ManagedThreadId},SynchronizationContext=null");
                return;
            }

            var con = (MySynchronizationContext)conOri;
            if (con == null)
            {
                Console.WriteLine($" Mid={Thread.CurrentThread.ManagedThreadId},MySynchronizationContext=null");
            }

            Console.Write($" Mid={Thread.CurrentThread.ManagedThreadId}");
            Console.Write($",AsyncData ={con.AsyncData.Value},StaticData={con.StaticData}");
            Console.Write($",AsyncDataAry ={con.AsyncDataAry.Value[0]},StaticDataAry={con.StaticDataAry[0]}");
            Console.WriteLine();
        }


        #region override
        public override void Send(SendOrPostCallback d, object? state)
        {
            var con = CreateCopy();
            SynchronizationContext.SetSynchronizationContext(con);
            base.Send(d, state);
        }

        public override void Post(SendOrPostCallback d, object? state)
        {
            var con = CreateCopy();
            ThreadPool.QueueUserWorkItem(s =>
            {
                SynchronizationContext.SetSynchronizationContext(con);
                s.d(s.state);
            }, (d, state), preferLocal: false);
        }


        public override void OperationStarted()
        {
            Console.WriteLine(nameof(OperationStarted));
        }

        public override void OperationCompleted()
        {
            Console.WriteLine(nameof(OperationCompleted));
        }


        public override int Wait(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
        {
            return base.Wait(waitHandles, waitAll, millisecondsTimeout);
        }

        public override SynchronizationContext CreateCopy()
        {
            var con = new MySynchronizationContext();
            con.StaticData = StaticData;
            con.AsyncData = AsyncData;
            con.StaticDataAry = StaticDataAry;
            con.AsyncDataAry = AsyncDataAry;
            return con;
        }
        #endregion
    }
}
