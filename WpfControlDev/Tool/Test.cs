using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfControlDev.Tool
{
    public class Test
    {
        public async Task<string> TTest(string url)
        {
            DownLoadTest dl = new DownLoadTest();
            var tk1 = await dl.DownLoadStringTaskAsync(url);
            return tk1;
        }

        public void HTest()
        {
            Task<string> task = new Task<string>(() =>
            {
                var rst = TTest("http://www.baidu.com");
                return rst.Result;
            });
            
            task.ContinueWith((obj)=> 
            {
                Console.WriteLine(" --> ContinueWith1>>>" + obj.Result);
                return TTest("http://www.163.com");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
            
            task.ContinueWith((obj) =>
            {
                Console.WriteLine(" --> ContinueWith2>>>" + obj.Result);
                return TTest("http://www.sina.com");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            task.Start();
            Console.WriteLine(task.Result);
        }

        public async void HTest1()
        {
            var rst1 = await TTest("http://www.baidu.com");
            Console.WriteLine("rst1:"+rst1);
            var rst2 = await TTest("http://www.163.com");
            Console.WriteLine("rst2:" + rst2);
            var rst3 = await TTest("http://www.sina.com.cn");
            Console.WriteLine(string.Format("rst3:{0}", rst3));
            
        }


        static int TaskMethod(string name, int seconds)
        {
            Console.WriteLine("Task Method : Task {0} is running on a thread id {1}. Is thread pool thread: {2}",
            name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            return 42 * seconds;
        }

        public static void TaskMethodTest()
        {
            Console.WriteLine("主线程 Thread id {0}, 是否是线程池线程: {1}",
            Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);

            var firstTask = new Task<int>(() => TaskMethod("第一个任务", 3));
            var secondTask = new Task<int>(() => TaskMethod("第二个任务", 2));

            firstTask.ContinueWith(t => Console.WriteLine("后续：第一个任务的返回结果为 {0}. Thread id {1}, 是否是线程池线程: {2}",
            t.Result, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread),
            TaskContinuationOptions.OnlyOnRanToCompletion);

            firstTask.Start();
            secondTask.Start();

            Thread.Sleep(TimeSpan.FromSeconds(4)); //给予足够时间，让firstTask、secondTask及其后续操作执行完毕。

            Task continuation = secondTask.ContinueWith(t => Console.WriteLine("后续：第二个任务的返回结果为 {0}. Thread id {1}, 是否是线程池线程: {2}",
            t.Result, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread),
            TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously);
        }

    }
}
