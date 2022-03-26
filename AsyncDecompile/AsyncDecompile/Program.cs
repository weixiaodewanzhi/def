using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AsyncDecompile
{


    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            AsyncLocalData.Init("Init ");
            //ExecutionContext.SuppressFlow();

            MySynchronizationContext.Init("Init ");

            ShowContext("A00");

            //var res = await TaskMethodAsync().ConfigureAwait(false);
            var res = await TaskMethodAsync();

            ShowContext("A01");
            return res;
        }

        public static void ShowContext(string str)
        {
            AsyncLocalData.DataArray += (str + " ");
            AsyncLocalData.DataSingle += (str + " ");
            //  AsyncLocalData.Show(str);
            var con = (MySynchronizationContext)SynchronizationContext.Current;
            if (con != null)
            {
                con.AsyncData.Value += (str + " ");
                con.StaticData += (str + " ");
                con.AsyncDataAry.Value[0] += (str + " ");
                con.StaticData += (str + " ");
                con.StaticDataAry[0] += (str + " ");
            }

            MySynchronizationContext.Show(str);

        }

        public static async Task<int> TaskMethodAsync()
        {
            ShowContext("A1");
            var res = 2;
            await Task.Yield();
            ShowContext("A2-Yield");
            var fac = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());

            var aa = await fac.StartNew(() =>
            {
                ShowContext("A21");
                return 1;
            });
            var bb = await fac.StartNew(() =>
            {
                ShowContext("A22");
                return 1;
            });
            ShowContext("A3");
            await Task.Delay(2000);
            ShowContext("A4-Delay");
            return res;
        }
    }
}
