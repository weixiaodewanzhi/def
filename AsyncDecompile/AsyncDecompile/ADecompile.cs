using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AsyncDecompile2
{
    public class AsyncSingle
    {
        // Method TaskMethod with token 06000007
        public Task<int> TaskMethod()
        {
            return Task.FromResult<int>(1);
        }

        [/*Attribute with token 0C00001A*/AsyncStateMachine(typeof(AsyncSingle.__RunAsync__d__1))]
        public Task<int> RunAsync()
        {
            AsyncSingle.__RunAsync__d__1 stateMachine = new AsyncSingle.__RunAsync__d__1();
            stateMachine.____t__builder = AsyncTaskMethodBuilder<int>.Create();
            stateMachine.____4__this = this;
            stateMachine.____1__state = -1;
            stateMachine.____t__builder.Start<AsyncSingle.__RunAsync__d__1>(ref stateMachine);
            return stateMachine.____t__builder.Task;
        }

        // Type <RunAsync>d__1 with token 02000007
        [/*Attribute with token 0C000019*/CompilerGenerated]
        private sealed class __RunAsync__d__1 : IAsyncStateMachine
        {
            /// <summary>
            /// 状态;
            /// 初始-1;结束-2;未完成0;
            /// </summary>
            public int ____1__state;
            // Field <>t__builder with token 04000004

            public AsyncTaskMethodBuilder<int> ____t__builder;
            // Field <>4__this with token 04000005

            public AsyncSingle ____4__this;
            /// <summary>
            /// TaskAwaiter的最终结果
            /// </summary>
            private int __res__5__1;
            /// <summary>
            /// TaskAwaiter的临时结果
            /// </summary>
            private int ____s__2;
            /// <summary>
            /// TaskAwaiter
            /// </summary>
            private TaskAwaiter<int> ____u__1;

            void IAsyncStateMachine.MoveNext()
            {
                int num1 = this.____1__state;
                int res51;
                try
                {
                    TaskAwaiter<int> awaiter;
                    int num2;
                    if (num1 != 0)
                    {
                        awaiter = this.____4__this.TaskMethod().GetAwaiter();
                        if (!awaiter.IsCompleted)
                        {
                            this.____1__state = num2 = 0;
                            this.____u__1 = awaiter;
                            AsyncSingle.__RunAsync__d__1 stateMachine = this;
                            this.____t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<int>, AsyncSingle.__RunAsync__d__1>(ref awaiter, ref stateMachine);
                            return;
                        }
                    }
                    else
                    {
                        awaiter = this.____u__1;
                        this.____u__1 = new TaskAwaiter<int>();
                        this.____1__state = num2 = -1;
                    }
                    this.____s__2 = awaiter.GetResult();
                    this.__res__5__1 = this.____s__2;
                    res51 = this.__res__5__1;
                }
                catch (Exception ex)
                {
                    this.____1__state = -2;
                    this.____t__builder.SetException(ex);
                    return;
                }
                this.____1__state = -2;
                this.____t__builder.SetResult(res51);
            }

            // Method SetStateMachine with token 0600000C
            [/*Attribute with token 0C00001D*/DebuggerHidden]
            void IAsyncStateMachine.SetStateMachine(/*Parameter with token 08000002*/IAsyncStateMachine stateMachine)
            {
            }
        }
    }
}
