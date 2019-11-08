using WpfControlDev.Extend;

namespace WpfControlDev.Model
{
    public class Student : ViewModelBase
    {
        public int UserId { get; set; }


        public string Name { get; set; }
        public int Age { get; set; }
        public char Sex { get; set; }

        private int score;
        public int Score
        {
            get
            {
                return this.score;
            }
            set
            {
                if (this.score != value)
                {
                    this.score = value;
                    RaisePropertyChanged("Score");
                }
            }
        }

        public string UserName { get; set; }
        public string Exchange { get; set; }
        public string Remark { get; set; }
    }

    public class Stu
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string Name5 { get; set; }
        public string Name6 { get; set; }
        public string Name7 { get; set; }
        public string Name8 { get; set; }
        public string Name9 { get; set; }
        public string Name10 { get; set; }
        public string Name11 { get; set; }
        public string Name12 { get; set; }
        public string Name13 { get; set; }
        public string Name14 { get; set; }
        public string Name15 { get; set; }
        public string Name16 { get; set; }
    }
}
