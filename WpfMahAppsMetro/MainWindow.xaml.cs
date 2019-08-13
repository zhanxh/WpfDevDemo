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
using MahApps.Metro.Controls;

namespace WpfMahAppsMetro
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Keyboard.Focus(this.ud1);
        }

        private void MainWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.None && (e.KeyStates != KeyStates.None && (e.KeyStates == Keyboard.GetKeyStates(Key.Enter) || e.KeyStates == Keyboard.GetKeyStates(Key.Up) || e.KeyStates == Keyboard.GetKeyStates(Key.Down))))
            {
                
                FocusNavigationDirection dir = FocusNavigationDirection.Down;
                if (e.KeyStates == Keyboard.GetKeyStates(Key.Up))
                    dir = FocusNavigationDirection.Up;
                TraversalRequest request = new TraversalRequest(dir);
                UIElement focusElement = Keyboard.FocusedElement as UIElement;
                if (focusElement != null)
                {
                    focusElement.MoveFocus(request);
                }
                e.Handled = true;
            }
        }
    }
}
