using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControlDev.Extend;

namespace WpfControlDev.Model
{
    public class IOVariableModel: ViewModelBase
    {
        private ObservableCollection<IOVariable> _ioVariables;

        public ObservableCollection<IOVariable> IoVariables
        {
            get
            {
                return _ioVariables;
            }
            set
            {
                _ioVariables = value;
                RaisePropertyChanged("IoVariables");
            }
        }

        private ObservableCollection<string> _variableTypes;

        public ObservableCollection<string> VariableTypes
        {
            get
            {
                return _variableTypes;
            }
            set
            {
                _variableTypes = value;
                RaisePropertyChanged("VariableTypes");
            }
        }

        public DelegateCommand TestCommand { get; set; }

        public IOVariableModel()
        {
            VariableTypes = new ObservableCollection<string>();
            IoVariables = new ObservableCollection<IOVariable>();
        }
    }
}
