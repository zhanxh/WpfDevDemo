using System;
using System.Collections.Generic;

namespace ConsoleAppDev.lqtest
{
    public class StuScore
    {
        public int GradeID { get; set; }
        public int ClassID { get; set; }
        public int StuID { get; set; }
        public string StuName { get; set; }
        public string Subject { get; set; }
        public int Score { get; set; }
        public int Date { get; set; }
        public int Time { get; set; }

        public override string  ToString()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", GradeID, ClassID, StuID, StuName, Subject, Score, Date, Time);
        }
    }

    public class LastScore : IComparer<StuScore>
    {
        public int Compare(StuScore x, StuScore y)
        {
            if (x.Date < y.Date)
                return -1;
            else if (x.Date > y.Date)
                return 1;
            else //if (x.Date == y.Date)
            {
                if (x.Time < y.Time)
                    return -1;
                else if (x.Time > y.Time)
                    return 1;
                else
                    return 0;
            }
        }
        
    }
}