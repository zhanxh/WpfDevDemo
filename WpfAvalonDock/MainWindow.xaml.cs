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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.AvalonDock;

namespace WpfAvalonDock
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CommandManager.AddPreviewExecutedHandler(this, new ExecutedRoutedEventHandler(this.ContentClosing));
        }

        /// <summary>
        /// Handler called when user clicked on one of the three buttons in a DockablePane
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContentClosing(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Close)// || e.Command == DockablePaneCommands.Close
            {
                e.Handled = true;
                DockingManager source = sender as DockingManager;
                if (source.ActiveContent != null)
                {
                    //source.ActiveContent.Close();
                }
            }
            //else if (e.Command == DockablePaneCommands.Hide || e.Command == DockablePaneCommands.ToggleAutoHide)
            //{
            //    e.Handled = true;
            //}
            //string.IsNullOrWhiteSpace

        }
        
    }
}
