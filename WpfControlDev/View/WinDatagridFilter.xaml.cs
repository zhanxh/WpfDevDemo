using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfControlDev.Extend;
using WpfControlDev.ViewModel;

namespace WpfControlDev.View
{
    /// <summary>
    /// WinDatagridFilter.xaml 的交互逻辑
    /// </summary>
    public partial class WinDatagridFilter : Window
    {
        public WinDatagridFilter()
        {
            InitializeComponent();
            this.DataContext = new StudentsViewModel();
        }

        private void listFilter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //找到第一个RadioButton,触发Command
            var fd = ControlSearch.GetVisualChild<RadioButton>(rdList);
            if (fd != null)
            {
                fd.IsChecked = true;
                fd.Command?.Execute(fd.CommandParameter);
            }
        }
    }
}
