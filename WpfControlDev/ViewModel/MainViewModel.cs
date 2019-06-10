using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControlDev.Extend;

namespace WpfControlDev.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            _TxtData = "Hello.";
        }
        private string _TxtData;
        public string TxtData
        {
            get
            {
                return this._TxtData;
            }
            set
            {
                if (this._TxtData != value)
                {
                    this._TxtData = value;
                    RaisePropertyChanged("TxtData");
                }
            }
        }
    }
}
