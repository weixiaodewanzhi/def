using System;
using System.Collections.Generic;
using System.Text;

namespace Memleak
{
    public class JobClass
    {
        public void Invoke(Action act)
        {
            act();
        }
    }

    public class MyClass
    {
        private int[] ary = new int[1024 * 1024];
        private JobClass _jobClass;
        private int _id;

        public MyClass(JobClass jobClass)
        {
            _jobClass = jobClass;
        }

        public void Foo()
        {
            if (ClassMemberTest.RefClassMember)
            {
                _jobClass.Invoke(() =>
                {
                    System.Console.WriteLine(_id);
                });
            }
            else
            {
                var aa = _id;
                _jobClass.Invoke(() =>
                {
                    System.Console.WriteLine(aa);
                });
            }
        }
    }

    internal class ClassMemberTest
    {
        public static bool RefClassMember = true;

        public void LeakTest()
        {
            var jobClass = new JobClass();
            var myClass = new MyClass(jobClass);
            myClass.Foo();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
