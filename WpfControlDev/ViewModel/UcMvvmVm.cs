using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using WpfControlDev.Extend;

namespace WpfControlDev.ViewModel
{
    public class UcMvvmVm :ViewModelBase
    {
        
        public char BuySale
        {
            get
            {
                return _EntrustBs;
            }
            set
            {
                _EntrustBs = value;
                RaisePropertyChanged("BuySale");
            }
        }

        private char _BuySale2 = '2';
        public char BuySale2
        {
            get
            {
                return _BuySale2;
            }
            set
            {
                _BuySale2 = value;
                RaisePropertyChanged("BuySale2");
            }
        }

        private char _OpenClose = '1';
        public char OpenClose
        {
            get
            {
                return _OpenClose;
            }
            set
            {
                _OpenClose = value;
                RaisePropertyChanged("OpenClose");
            }
        }

        private char _EntrustBs = '1';


        private char _EntrustKp1 = '1';
        private char _EntrustKp2 = '2';
        public char EntrustKp1
        {
            get { return _EntrustKp1; }
            set { _EntrustKp1 = value; RaisePropertyChanged("EntrustKp1"); }
        }

        public char EntrustKp2
        {
            get { return _EntrustKp2; }
            set { _EntrustKp2 = value; RaisePropertyChanged("EntrustKp2"); }
        }


        private Color _C1Color = Color.FromRgb(100,100,100);
        public Color C1Color
        {
            get { return _C1Color; }
            set { _C1Color = value;  RaisePropertyChanged("C1Color"); }
        }

        private Color _C2Color = Color.FromRgb(200, 200, 200);
        public Color C2Color
        {
            get { return _C2Color; }
            set { _C2Color = value; RaisePropertyChanged("C2Color"); }
        }

        public ICommand Test3Cmd
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Console.WriteLine(string.Format("BuySale : {0}", BuySale));
                    Console.WriteLine(string.Format("BuySale2: {0}", BuySale2));
                    Console.WriteLine(string.Format("EntrustKp1: {0}", EntrustKp1));
                    Console.WriteLine(string.Format("EntrustKp2: {0}", EntrustKp2));
                    Console.WriteLine(string.Format("C1Color：{0}-{1}-{2}", C1Color.R, C1Color.G, C1Color.B));
                    Console.WriteLine(string.Format("C2Color：{0}-{1}-{2}", C2Color.R, C2Color.G, C2Color.B));
                });
            }
        }
    }
}
