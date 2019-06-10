using CacheManager.Core;
using ConsoleAppDev.typetest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDev.CacheMgrTest
{
    public class CMTest
    {
        public void Test1()
        {
            var cache = CacheFactory.Build("getStartedCache", settings =>
            {
                settings.WithSystemRuntimeCacheHandle("handleName");
            });
            
            cache.Add("keyA", "valueA");
            cache.Put("keyB", 23);
            cache.Put("keyC", new Employee());
            cache.Update("keyB", v => 42);
            Console.WriteLine("KeyA is " + cache.Get("keyA"));      // should be valueA
            Console.WriteLine("KeyB is " + cache.Get("keyB"));      // should be 42
            Console.WriteLine("KeyC is " + cache.Get("keyC").ToString());
            cache.Remove("keyA");
            Console.WriteLine("KeyA removed? " + (cache.Get("keyA") == null).ToString());
            Console.WriteLine("We are done...");
            Console.ReadKey();
        }
    }
}
