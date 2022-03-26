using System;

namespace Memleak
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Test1();
            Test2();
        }

        static void Test1()
        {
            TestClass.Config(0);
            new TestClass().LeakTest();
        }

        static void Test2()
        {
            ClassMemberTest.RefClassMember = true;
            new ClassMemberTest().LeakTest();
        }
    }
}
