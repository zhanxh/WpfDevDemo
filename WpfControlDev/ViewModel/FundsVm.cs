using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfControlDev.Extend;

namespace WpfControlDev.ViewModel
{
    public class FundsParm:ViewModelBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }
        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }
    }
    public class FundsVm : ViewModelBase
    {
        public FundsVm()
        {
            _FutuEnable = "0.00";
            _SecuEnable = "0.00";
            _SoptEnable = "0.00";
            _FutuFloat = "0.00";
            _SecuFloat = "0.00";
            _SoptFloat = "0.00";
            _SoptTerm = new ObservableCollection<FundsParm>();
            _FutuTerm = new ObservableCollection<FundsParm>();
            _SecuTerm = new ObservableCollection<FundsParm>();
            for(int i = 0; i < 10; i++)
            {
                _SoptTerm.Add(new FundsParm() { Name = "权" + i, Value = "权钱" + i });
            }
            for (int i = 0; i < 15; i++)
            {
                _FutuTerm.Add(new FundsParm() { Name = "期" + i, Value = "期钱" + i });
            }
            for (int i = 0; i < 40; i++)
            {
                _SecuTerm.Add(new FundsParm() { Name = "证" + i, Value = "证钱" + i });
            }

        }

        ObservableCollection<FundsParm> _SoptTerm;
        public ObservableCollection<FundsParm> SoptTerm
        {
            get { return _SoptTerm; }
            set
            {
                _SoptTerm = value;
                RaisePropertyChanged("SoptTerm");
            }
        }


        ObservableCollection<FundsParm> _FutuTerm;
        public ObservableCollection<FundsParm> FutuTerm
        {
            get { return _FutuTerm; }
            set
            {
                _FutuTerm = value;
                RaisePropertyChanged("FutuTerm");
            }
        }

        ObservableCollection<FundsParm> _SecuTerm;
        public ObservableCollection<FundsParm> SecuTerm
        {
            get { return _SecuTerm; }
            set
            {
                _SecuTerm = value;
                RaisePropertyChanged("SecuTerm");
            }
        }



        string _SecuEnable;
        public string SecuEnable
        {
            get { return _SecuEnable; }
            set
            {
                _SecuEnable = value;
                RaisePropertyChanged("SecuEnable");
            }
        }
        string _FutuEnable;
        public string FutuEnable
        {
            get { return _FutuEnable; }
            set
            {
                _FutuEnable = value;
                RaisePropertyChanged("FutuEnable");
            }
        }
        string _SoptEnable;
        public string SoptEnable
        {
            get { return _SoptEnable; }
            set
            {
                _SoptEnable = value;
                RaisePropertyChanged("SoptEnable");
            }
        }
        string _FutuFloat;
        string _SecuFloat;
        string _SoptFloat;

        public string FutuFloat
        {
            get { return _FutuFloat; }
            set
            {
                _FutuFloat = value;
                RaisePropertyChanged("FutuFloat");
            }
        }
        public string SecuFloat
        {
            get { return _SecuFloat; }
            set
            {
                _SecuFloat = value;
                RaisePropertyChanged("SecuFloat");
            }
        }
        public string SoptFloat
        {
            get { return _SoptFloat; }
            set
            {
                _SoptFloat = value;
                RaisePropertyChanged("SoptFloat");
            }
        }
    }
    
}
