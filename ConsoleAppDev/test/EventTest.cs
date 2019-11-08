using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDev.test
{
    public class EventTest
    {
        public event Action<string> MsgEventNotify;


        private void Test0(string msg)
        {
            Console.WriteLine("Msg0 : " + msg);
        }
        private void Test1(string msg)
        {
            Console.WriteLine("Msg1 : " + msg);
        }
        private void Test2(string msg)
        {
            Console.WriteLine("Msg2 : " + msg);
        }
        private void Test3(string msg)
        {
            Console.WriteLine("Msg3 : " + msg);
        }
        private void Test4(string msg)
        {
            Console.WriteLine("Msg4 : " + msg);
            throw  new Exception("Test4");
        }
        private void Test5(string msg)
        {
            Console.WriteLine("Msg5 : " + msg);
        }
        private void Test6(string msg)
        {
            Console.WriteLine("Msg6 : " + msg);
        }
        private void Test7(string msg)
        {
            Console.WriteLine("Msg7 : " + msg);
        }
        private void Test8(string msg)
        {
            Console.WriteLine("Msg8 : " + msg);
        }
        private void Test9(string msg)
        {
            Console.WriteLine("Msg9 : " + msg);
        }

        public EventTest()
        {
            MsgEventNotify += Test0;
            MsgEventNotify += Test1;
            MsgEventNotify += Test2;
            MsgEventNotify += Test3;
            MsgEventNotify += Test4;
            MsgEventNotify += Test5;
            MsgEventNotify += Test6;
            MsgEventNotify += Test7;
            MsgEventNotify += Test8;
            MsgEventNotify += Test9;
        }

        public void SetNotify(string hello)
        {
            MsgEventNotify?.Invoke(hello);
        }
    }


}
