using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using WpfControlDev.Extend;
using WpfControlDev.Model;
using WpfControlDev.Provider;

namespace WpfControlDev.ViewModel
{
    public class StudentsViewModel : ViewModelBase
    {
        public StudentsViewModel()
        {
            studentList = new ObservableCollection<Student>(StudentService.GetStudents());
            _ExchangeOprs = new List<ExchangeOpr>();
            _ExchangeOprs.Add(new ExchangeOpr() { Exchange = "F0", ExchangeCmd = ChgExchangeCmd});
            _ExchangeOprs.Add(new ExchangeOpr() { Exchange = "F1", ExchangeCmd = ChgExchangeCmd });
            _ExchangeOprs.Add(new ExchangeOpr() { Exchange = "F2", ExchangeCmd = ChgExchangeCmd });
            _ExchangeOprs.Add(new ExchangeOpr() { Exchange = "F3", ExchangeCmd = ChgExchangeCmd });
            _ExchangeOprs.Add(new ExchangeOpr() { Exchange = "F4", ExchangeCmd = ChgExchangeCmd });
            _ExchangeOprs.Add(new ExchangeOpr() { Exchange = "F5", ExchangeCmd = ChgExchangeCmd });

            viewsource = CollectionViewSource.GetDefaultView(StudentList);
            viewsource.Filter = (obj) =>
            {
                Student stu = obj as Student;
                if (stu == null)
                    return true;
                if (string.IsNullOrWhiteSpace(exchange))
                    return true;
                else
                    return exchange.Equals(stu.Exchange);
            };
            //viewsource.CurrentChanged += (sender,e) => 
            //{
            //    var data = viewsource.CurrentItem;
            //};
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

        private List<ExchangeOpr> _ExchangeOprs;
        public List<ExchangeOpr> ExchangeOprs
        {
            get { return _ExchangeOprs; }
            set
            {
                _ExchangeOprs = value;
                RaisePropertyChanged("ExchangeOprs");
            }
        }

        public string exchange = string.Empty;
        public ICollectionView viewsource { get; private set; }
        public ICommand ChgExchangeCmd
        {
            get
            {
                return new DelegateCommand((obj)=>
                {
                    var dt1 = DateTime.Now;
                    exchange = obj as string;
                    viewsource.Refresh();
                    var dt2 = DateTime.Now;
                    TimeSpan ts = dt2 - dt1;
                    Console.WriteLine("chg ms : " + ts.TotalMilliseconds);
                });
            }
        }

    }
}
