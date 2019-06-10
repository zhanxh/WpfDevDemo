using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfControlDev.Model
{
    public class ExchangeOpr
    {
        public string Exchange { get; set; }
        public ICommand ExchangeCmd { get; set; }
    }
}
