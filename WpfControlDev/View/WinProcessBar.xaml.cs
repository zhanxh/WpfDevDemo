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

namespace WpfControlDev.View
{
    /// <summary>
    /// WinProcessBar.xaml 的交互逻辑
    /// </summary>
    public partial class WinProcessBar : Window
    {
        public WinProcessBar()
        {
            InitializeComponent();
        }

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            if(pb.Value<100)
                pb.Value += 1;
        }

        private void Dec_OnClick(object sender, RoutedEventArgs e)
        {
            if(pb.Value>0)
                pb.Value -= 1;
        }
    }
}
