using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDecompile
{    

    public class AsyncLocalData
    {
        protected static AsyncLocal<Dictionary<string, object>> InnerDataArray = new AsyncLocal<Dictionary<string, object>>(AsuncLocalChanged<Dictionary<string, object>>());

        protected static AsyncLocal<string> InnerDataSingle = new AsyncLocal<string>(AsuncLocalChanged<string>());

        public static Action<AsyncLocalValueChangedArgs<T>> AsuncLocalChanged<T>()
        {
            return args =>
            {
                //  Console.WriteLine($" Mid={Thread.CurrentThread.ManagedThreadId},pre={args.PreviousValue ?? "null"},cur={args.CurrentValue ?? "null"},changed={args.ThreadContextChanged}");
            };
        }

        public static string DataArray
        {
            get
            {
                return (string)InnerDataArray.Value?["DataArray"];
            }
            set
            {
                if (InnerDataArray.Value == null)
                {
                    InnerDataArray.Value = new Dictionary<string, object>();
                }
                InnerDataArray.Value["DataArray"] = value;
            }
        }

        public static string DataSingle
        {
            get
            {
                return InnerDataSingle.Value;
            }
            set
            {
                InnerDataSingle.Value = value;
            }
        }

        public static void Init(string str)
        {
            DataArray = str;
            DataSingle = str;
        }

        public static void Show(string str)
        {
            Console.WriteLine($"{str} Mid={Thread.CurrentThread.ManagedThreadId},DataArray={DataArray},DataSingle={DataSingle}");
        }
    }
}
