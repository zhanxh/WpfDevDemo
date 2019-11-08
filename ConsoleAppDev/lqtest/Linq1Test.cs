using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleAppDev.lqtest
{
    public class Linq1Test
    {
        public List<StuScore> source = new List<StuScore>();

        public Linq1Test()
        {
            source.Add(new StuScore() { GradeID = 1, ClassID = 1, StuID = 998, StuName = "张三", Subject = "语文", Score = 89, Date = 20190807, Time = 140012 });
            source.Add(new StuScore() { GradeID = 1, ClassID = 1, StuID = 998, StuName = "张三", Subject = "语文", Score = 77, Date = 20190806, Time = 160012 });
            source.Add(new StuScore() { GradeID = 1, ClassID = 1, StuID = 998, StuName = "张三", Subject = "语文", Score = 89, Date = 20190809, Time = 100012 });
            source.Add(new StuScore() { GradeID = 1, ClassID = 1, StuID = 998, StuName = "张三", Subject = "语文", Score = 77, Date = 20190805, Time = 170012 });

            source.Add(new StuScore() { GradeID = 1, ClassID = 1, StuID = 998, StuName = "张三", Subject = "数学", Score = 67, Date = 20190805, Time = 140012 });
            source.Add(new StuScore() { GradeID = 1, ClassID = 1, StuID = 998, StuName = "张三", Subject = "数学", Score = 78, Date = 20190806, Time = 160012 });
            source.Add(new StuScore() { GradeID = 1, ClassID = 1, StuID = 998, StuName = "张三", Subject = "数学", Score = 34, Date = 20190802, Time = 100012 });
            source.Add(new StuScore() { GradeID = 1, ClassID = 1, StuID = 998, StuName = "张三", Subject = "数学", Score = 89, Date = 20190804, Time = 170012 });

            source.Add(new StuScore() { GradeID = 1, ClassID = 1, StuID = 998, StuName = "李四", Subject = "政治", Score = 55, Date = 20190807, Time = 140012 });
            source.Add(new StuScore() { GradeID = 1, ClassID = 1, StuID = 998, StuName = "李四", Subject = "政治", Score = 87, Date = 20190206, Time = 160012 });
            source.Add(new StuScore() { GradeID = 1, ClassID = 1, StuID = 998, StuName = "李四", Subject = "政治", Score = 66, Date = 20190809, Time = 100012 });
            source.Add(new StuScore() { GradeID = 1, ClassID = 1, StuID = 998, StuName = "李四", Subject = "政治", Score = 68, Date = 20190905, Time = 170012 });

            source.Add(new StuScore() { GradeID = 1, ClassID = 1, StuID = 998, StuName = "李四", Subject = "历史", Score = 56, Date = 20190805, Time = 140012 });
            source.Add(new StuScore() { GradeID = 1, ClassID = 1, StuID = 998, StuName = "李四", Subject = "历史", Score = 43, Date = 20190706, Time = 160012 });
            source.Add(new StuScore() { GradeID = 1, ClassID = 1, StuID = 998, StuName = "李四", Subject = "历史", Score = 83, Date = 20190802, Time = 100012 });
            source.Add(new StuScore() { GradeID = 1, ClassID = 1, StuID = 998, StuName = "李四", Subject = "历史", Score = 97, Date = 20190604, Time = 170012 });
        }

        public void Test()
        {
            //var datas = source.GroupBy(p => new {p.GradeID, p.ClassID, p.StuID, p.StuName, p.Subject})
            //    .Select(p => p.OrderByDescending(x => new {x.Date, x.Time}).FirstOrDefault()).ToList();
            //foreach (var data in datas)
            //{
            //    Console.WriteLine(data.ToString());
            //}

            var datas = source.GroupBy(p => new { p.GradeID, p.ClassID, p.StuID, p.StuName, p.Subject })
                .Select(p => p.OrderByDescending(x => x, new LastScore()).FirstOrDefault()).ToList();
            foreach (var data in datas)
            {
                Console.WriteLine(data.ToString());
            }
        }

        public void Test1()
        {
            var datas = from n in source
                        group n by new { n.GradeID, n.ClassID, n.StuID, n.StuName, n.Subject }
                into g
                        select g.OrderByDescending(x => x, new LastScore()).FirstOrDefault();
            foreach (var data in datas)
            {
                Console.WriteLine(data.ToString());
            }
        }

    }
}