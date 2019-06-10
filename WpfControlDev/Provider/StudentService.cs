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

        static StudentService()
        {
            studentList = new List<Student>();
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
            string[] names = { "Kevin Garnet", "Marry Jane", "Lebron James", "Larry Bird", "Kevin Martin" };
            for (int i = 0; i < 100; i++)
            {
                var stu = new Student() { Name = GetName() + i, UserName = names[i % 5], Age = i, Exchange = "F" + i % 5, Remark = "SDFASDKJFAdfasdfsadfSLDFdddddddddddddddddddddddddddddd" + i };
                studentList.Add(stu);
            }
            return studentList;
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
