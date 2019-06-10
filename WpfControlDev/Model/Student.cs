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
}
