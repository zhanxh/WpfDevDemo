using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfControlDev.Extend;

namespace WpfControlDev.ViewModel
{
    public class PwdVm: ViewModelBase
    {
        private string _Password;
        public string AccPwd
        {
            get { return _Password; }
            set { _Password = value; RaisePropertyChanged("AccPwd"); }
        }

        public ICommand TestCmd
        {
            get
            {
                return new DelegateCommand((obj) => 
                {
                    Console.WriteLine("Password : " + AccPwd);
                });
            }
        }
    }
}
