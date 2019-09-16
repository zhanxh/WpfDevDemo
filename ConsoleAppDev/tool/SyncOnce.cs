using System;
using System.Threading;

namespace ConsoleAppDev.tool
{
    public class SyncOnce
    {
        private long done = 0;
        private object obj = new object();

        public void Call(Action ac)
        {
            if(Interlocked.Read(ref done)==1)
                return;
            lock (obj)
            {
                if (Interlocked.CompareExchange(ref done, 1, 0)==0)
                {
                    ac?.Invoke();
                }
            }
        }
    }
}