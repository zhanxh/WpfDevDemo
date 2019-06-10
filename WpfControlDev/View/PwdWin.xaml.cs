using System.Windows;
using WpfControlDev.ViewModel;

namespace WpfControlDev.View
{
    /// <summary>
    /// PwdWin.xaml 的交互逻辑
    /// </summary>
    public partial class PwdWin : Window
    {
        public PwdWin()
        {
            InitializeComponent();
            this.DataContext = new PwdVm();
        }
    }
}
