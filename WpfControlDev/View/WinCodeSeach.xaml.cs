using System.Windows;
using System.Windows.Input;
using WpfControlDev.ViewModel;

namespace WpfControlDev.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WinCodeSeach : Window
    {
        public WinCodeSeach()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }


        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.None)
            {
                if (e.Key >= Key.D0 && e.Key < Key.Z)
                {
                    codesearch1.Show();
                }
            }
        }
    }
}
