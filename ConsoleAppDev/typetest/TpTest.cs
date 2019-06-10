using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDev.typetest
{
    public class TpTest
    {
        public void Test1()
        {
            int n = 1000000000;
            int j = 5;

            DateTime d1 = DateTime.Now;

            for (int i = 0; i < n; i++)
            {
                Employee e = new Employee();

                //for (int k = 0; k < j; k++)
                //{
                    Type t = e.TypeInfo;
                //}
            }

            TimeSpan ts = DateTime.Now - d1;

            Console.WriteLine(ts.TotalMilliseconds);

            DateTime d2 = DateTime.Now;

            for (int i = 0; i < n; i++)
            {
                Employee e = new Employee();

                //for (int k = 0; k < j; k++)
                //{
                    Type t = e.GetType();
                //}
            }

            ts = DateTime.Now - d2;

            Console.WriteLine(ts.TotalMilliseconds);


            DateTime d3 = DateTime.Now;

            for (int i = 0; i < n; i++)
            {
                Employee e = new Employee();

                //for (int k = 0; k < j; k++)
                //{
                    Type t = typeof(Employee);
                //}
            }

            ts = DateTime.Now - d3;

            Console.WriteLine(ts.TotalMilliseconds);

            Console.ReadLine();
        }

    }
    class Employee
    {
        Type typeInfo;

        public Type TypeInfo
        {
            get { return this.typeInfo; }
        }

        public Employee()
        {
            this.typeInfo = this.GetType();
        }

        public override string ToString()
        {
            return "Employee";
        }
    }
}
