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
    /// WinListView.xaml 的交互逻辑
    /// </summary>
    public partial class WinListView1 : Window
    {
        public WinListView1()
        {
            InitializeComponent();
            this.DataContext = new StudentsViewModel('1');
        }
        

    }
}
