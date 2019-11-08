using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControlDev.Model;

namespace WpfControlDev.Provider
{
    public class StudentService
    {
        private static List<Student> studentList;
        private static List<Stu> stuList;

        static StudentService()
        {
            studentList = new List<Student>();
            stuList = new List<Stu>();
        }

        public static List<Student> RetrieveStudentList()
        {
            studentList.Add(new Student() { UserId = 1, UserName = "Kevin Garnet", Score = 100 });
            studentList.Add(new Student() { UserId = 2, UserName = "Marry Jane", Score = 85 });
            studentList.Add(new Student() { UserId = 3, UserName = "Lebron James", Score = 80 });
            studentList.Add(new Student() { UserId = 4, UserName = "Larry Bird", Score = 65 });
            studentList.Add(new Student() { UserId = 5, UserName = "Kevin Martin", Score = 60 });
            return studentList;
        }

        public static List<Student> RetrievePassedStudentList()
        {
            studentList.Add(new Student() { UserId = 1, UserName = "Kevin Garnet", Score = 100 });
            studentList.Add(new Student() { UserId = 2, UserName = "Marry Jane", Score = 85 });
            studentList.Add(new Student() { UserId = 3, UserName = "Lebron James", Score = 80 });
            studentList.Add(new Student() { UserId = 4, UserName = "Larry Bird", Score = 65 });
            studentList.Add(new Student() { UserId = 5, UserName = "Kevin Martin", Score = 60 });
            return studentList.Where(x => x.Score >= 60).ToList();
        }

        public static List<Student> GetStudents()
        {
            if (studentList.Count > 100)
                return studentList;

            string[] names = { "Kevin Garnet", "Marry Jane", "Lebron James", "Larry Bird", "Kevin Martin" };
            for (int i = 0; i < 100000; i++)
            {
                var stu = new Student() { Name = GetName() + i, UserName = names[i % 5], Age = i, Exchange = "F" + i % 5, Remark = "SDFASDKJFAdfasdfsadfSLDFdddddddddddddddddddddddddddddd" + i };
                studentList.Add(stu);
            }
            return studentList;
        }
        public static List<Stu> GetStus()
        {
            if (stuList.Count > 0)
                return stuList;

            for (int i = 0; i < 100000; i++)
            {
                var stui = new Stu();
                stui.Id = i;
                stui.Name = "name1" + i;
                stui.Name2 = "name2" + i;
                stui.Name3 = "name3" + i;
                stui.Name4 = "name4" + i;
                stui.Name5 = "name5" + i;
                stui.Name6 = "name6" + i;
                stui.Name7 = "name7" + i;
                stui.Name8 = "name8" + i;
                stui.Name9 = "name9" + i;
                stui.Name10 = "name10" + i;
                stui.Name11 = "name11" + i;
                stui.Name12 = "name12" + i;
                stui.Name13 = "name13" + i;
                stui.Name14 = "name14" + i;
                stui.Name15 = "name15" + i;
                stui.Name16 = "name16" + i;
                stuList.Add(stui);
            }

            return stuList;
        }

        static string str = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static Random rd = new Random();
        public static string GetName()
        {
            StringBuilder sb = new StringBuilder();
            var i1 = rd.Next(52);
            var i2 = rd.Next(52);
            sb.Append(str[i1]);
            sb.Append(str[i2]);
            if (sb.Length == 0)
            {
                sb.Append("IF");
            }
            return sb.ToString();
        }

    }
}
