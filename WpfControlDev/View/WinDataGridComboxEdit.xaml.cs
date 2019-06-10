using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfControlDev.ViewModel;

namespace WpfControlDev.View
{
    /// <summary>
    /// WinDateGridComboxEdit.xaml 的交互逻辑
    /// </summary>
    public partial class WinDataGridComboxEdit : Window
    {
        public WinDataGridComboxEdit()
        {
            DataContext = new IOVariableViewModel();
            InitializeComponent();
        }
    }
}
