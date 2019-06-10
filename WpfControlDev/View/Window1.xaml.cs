using System;
using System.Text;
using System.Windows;
using System.Security.Cryptography;
using System.IO;
using WpfControlDev.ViewModel;

namespace WpfControlDev.View
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            this.DataContext = new FastKeyVm();
        }

    }
}
