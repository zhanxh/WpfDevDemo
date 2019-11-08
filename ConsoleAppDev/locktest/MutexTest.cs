using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppDev.locktest
{
    public class MutexTest
    {
        public void Test()
        {
            IncThread mthrd1 = new IncThread("IncThread thread ", 5);
            DecThread mthrd2 = new DecThread("DecThread thread ", 5);
            mthrd1.thrd.Join();
            mthrd2.thrd.Join();
        }

        public void Test1()
        {

            List<IncDecThread> threads = new List<IncDecThread>();
            for (int i = 0; i < 20; i++)
            {
                IncDecThread th = new IncDecThread(i % 2 == 0 ? "Dec-" : "Inc-", i + 1, 20, i % 2 == 0);
                threads.Add(th);
            }
            foreach(var th in threads)
            {
                th.thrd.Join();
            }
            Console.ReadKey();
        }

    }

    class shareRes
    {
        public static int count = 0;
        public static Mutex mutex = new Mutex();
    }

    class IncDecThread
    {
        bool inc;
        int id;
        int number;
        public Thread thrd;
        public IncDecThread(string name,int id, int n, bool inc)
        {
            this.inc = inc;
            this.id = id;
            thrd = new Thread(this.run);
            number = n;
            thrd.Name = name;
            thrd.Start();
        }
        void run()
        {
            do
            {
                //申请
                shareRes.mutex.WaitOne();
                
                shareRes.count++;
                if (inc)
                    number++;
                else
                    number--;
                Console.WriteLine(string.Format("Name:{0}{1:D2}, Mod:{2}, Num:{3:D3}, Count:{4:D4}", thrd.Name, id, (inc ? "+" : "-"), number, shareRes.count));
                Thread.Sleep(50);
                
                //  释放
                shareRes.mutex.ReleaseMutex();
            } while (number > 0 && number < 40);
        }
    }

    class IncThread
    {
        int number;
        public Thread thrd;
        public IncThread(string name, int n)
        {
            thrd = new Thread(this.run);
            number = n;
            thrd.Name = name;
            thrd.Start();
        }
        void run()
        {
            Console.WriteLine(thrd.Name + "正在等待 the mutex");
            //申请
            shareRes.mutex.WaitOne();
            Console.WriteLine(thrd.Name + "申请到 the mutex");
            do
            {
                Thread.Sleep(1000);
                shareRes.count++;
                Console.WriteLine("In " + thrd.Name + "ShareRes.count is " + shareRes.count);
                number--;
            } while (number > 0);
            Console.WriteLine(thrd.Name + "释放 the nmutex");
            //  释放
            shareRes.mutex.ReleaseMutex();
        }
    }
    class DecThread
    {
        int number;
        public Thread thrd;
        public DecThread(string name, int n)
        {
            thrd = new Thread(this.run);
            number = n;
            thrd.Name = name;
            thrd.Start();
        }
        void run()
        {
            Console.WriteLine(thrd.Name + "正在等待 the mutex");
            //申请
            shareRes.mutex.WaitOne();
            Console.WriteLine(thrd.Name + "申请到 the mutex");
            do
            {
                Thread.Sleep(100);
                shareRes.count--;
                Console.WriteLine("In " + thrd.Name + "ShareRes.count is " + shareRes.count);
                number--;
            } while (number > 0);
            Console.WriteLine(thrd.Name + "释放 the nmutex");
            //  释放
            shareRes.mutex.ReleaseMutex();
        }
    }
}
