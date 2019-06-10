using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControlDev.Extend;
using WpfControlDev.Model;

namespace WpfControlDev.ViewModel
{
    public class IOVariableViewModel : ViewModelBase
    {
        public IOVariableModel Model { get; set; }

        public IOVariableViewModel()
        {
            Model = new IOVariableModel();

            Model.TestCommand = new DelegateCommand(TestCommandHandler);

            Model.VariableTypes.Add("整型");
            Model.VariableTypes.Add("实型");
            Model.VariableTypes.Add("字符串");
        }
        private void TestCommandHandler(object obj)
        {

        }
    }
}
