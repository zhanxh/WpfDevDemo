using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WpfControlDev.Extend;
using WpfControlDev.Model;
using WpfControlDev.Provider;

namespace WpfControlDev.ViewModel
{
    public class ScoreListViewModel : ViewModelBase
    {
        public ScoreListViewModel()
        {
            this.StudentList = new ObservableCollection<Student>(StudentService.RetrieveStudentList());
        }
        

        private bool isPassed;
        public bool IsPassed
        {
            get
            {
                return this.isPassed;
            }
            set
            {
                if (this.isPassed != value)
                {
                    this.isPassed = value;
                    RaisePropertyChanged("IsPassed");
                }
            }
        }

        private ObservableCollection<Student> studentList;
        public ObservableCollection<Student> StudentList
        {
            get
            {
                return this.studentList;
            }
            set
            {
                if (this.studentList != value)
                {
                    this.studentList = value;
                    RaisePropertyChanged("StudentList");
                }
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return new DelegateCommand(OnAdd);
            }
        }
        public ICommand ModifyCommand
        {
            get
            {
                return new DelegateCommand(OnModify);
            }
        }
        public ICommand RemoveCommand
        {
            get
            {
                return new DelegateCommand(OnRemove);
            }
        }

        public void OnAdd(object obj)
        {
            this.StudentList.Add(new Student() { UserId = 7, UserName = "李永京", Score = 75 });
        }

        public void OnModify(object obj)
        {
            var item = this.StudentList.SingleOrDefault(x => x.UserId == 1);
            if (item != null)
            {
                item.Score = 0;
            }
        }

        public void OnRemove(object obj)
        {
            var item = this.StudentList.SingleOrDefault(x => x.UserId == 2);
            if (item != null)
            {
                this.StudentList.Remove(item);
            }
        }

    }
}
