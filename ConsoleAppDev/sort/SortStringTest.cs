using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDev.sort
{
    /// <summary>
    /// 使用100万记录来排序
    /// 来比较StringComparer.CurrentCulture.Compare 
    /// 和 StringComparer.Ordinal.Compare排序性能
    /// </summary>
    public class SortStringTest
    {
        private char[] letters = new char[52];
        public List<string> sortData = new List<string>();
        public void Init()
        {
            Console.WriteLine("Init...");
            var dd = new Random();
            for (var i= 0; i < 26; i++)
            {
                letters[i] = (char)('a' + i);
            }
            for (var i = 0; i < 26; i++)
            {
                letters[i+26] = (char)('A' + i);
            }

            for(int i=0; i< 1000; i++)
            {
                for(int j=0; j < 100; j++)
                {
                    for(int k=0; k<10; k++)
                    {
                        StringBuilder sb = new StringBuilder();
                        for(int m = 0; m < 4; m++)
                        {
                            //var dd = new Random();
                            var d = dd.Next(52);
                            sb.Append(letters[d]);
                        }
                        sb.Append(i);
                        sb.Append(j);
                        sb.Append(k);
                        sortData.Add(sb.ToString());
                    }
                }
            }
        }

        public void Sort()
        {
            Console.WriteLine("TotalSize:" + sortData.Count);
            List<string> s1 = new List<string>(sortData);
            List<string> s2 = new List<string>(sortData);
            //List<string> s3 = new List<string>(sortData);
            //List<string> s4 = new List<string>(sortData);
            DateTime dt1 = DateTime.Now;
            s1.Sort(StringComparer.CurrentCulture.Compare);
            DateTime dt2 = DateTime.Now;
            DateTime dt3 = DateTime.Now;
            s2.Sort(StringComparer.Ordinal.Compare);
            DateTime dt4 = DateTime.Now;

            TimeSpan ts1 = dt2 - dt1;
            TimeSpan ts2 = dt4 - dt3;
            Console.WriteLine("ts1:" + ts1.TotalMilliseconds);
            Console.WriteLine("ts2:" + ts2.TotalMilliseconds);
            Console.WriteLine("Over");
        }
    }
}
