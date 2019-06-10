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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfControlDev.Model;
using WpfControlDev.ViewModel;

namespace WpfControlDev.UC
{
    /// <summary>
    /// CodeSeach.xaml 的交互逻辑
    /// </summary>
    public partial class UCCodeSeach : UserControl
    {
        private const int SpaceTime = 150;
        public UCCodeSeach()
        {
            InitializeComponent();
            txtFilter.Focus();
            this.DataContext = new StudentsViewModel();
            timer.Interval = new TimeSpan(0, 0, 0, 0, SpaceTime);
            timer.Tick += (obj, arg) =>
            {
                timer.Stop();
                (FindResource("ViewSource") as CollectionViewSource).View.Refresh();
                listFilter.SelectedIndex = 0;
            };
        }

        public void Show()
        {
            this.Visibility = Visibility.Visible;
            txtFilter.Focus();
        }

        private void OnViewFilter(object sender, FilterEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFilter.Text))
            {
                var str = (e.Item as Student).Name.ToLower();
                str.StartsWith(txtFilter.Text.ToLower());
                e.Accepted = str.Contains(txtFilter.Text.ToLower());
            }
        }
        private DispatcherTimer timer = new DispatcherTimer();
        private void OnFilterChanged(object sender, TextChangedEventArgs e)
        {
            if (timer.IsEnabled)
                timer.Stop();
            timer.Interval = new TimeSpan(0, 0, 0, 0, SpaceTime);
            timer.Start();
        }

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Down)
            {
                if (txtFilter.IsFocused)
                {
                    if (listFilter.SelectedIndex < listFilter.Items.Count - 1)
                    {
                        ListViewAutomationPeer lvap = new ListViewAutomationPeer(listFilter);
                        var svap = lvap.GetPattern(PatternInterface.Scroll) as ScrollViewerAutomationPeer;
                        var scroll = svap.Owner as ScrollViewer;
                        //Console.WriteLine("1VOffset:{0},Index:{1},H:{2}", scroll.VerticalOffset, listFilter.SelectedIndex, scroll.ViewportHeight);
                        listFilter.SelectedIndex++;
                        if (listFilter.SelectedIndex - scroll.VerticalOffset >= scroll.ViewportHeight)
                        {
                            scroll.LineDown();
                        }
                    }
                }
            }
            else if(e.Key == Key.Up)
            {
                if (txtFilter.IsFocused)
                {
                    if (listFilter.SelectedIndex > 0)
                    {
                        ListViewAutomationPeer lvap = new ListViewAutomationPeer(listFilter);
                        var svap = lvap.GetPattern(PatternInterface.Scroll) as ScrollViewerAutomationPeer;
                        var scroll = svap.Owner as ScrollViewer;
                        //Console.WriteLine("1VOffset:{0},Index:{1},H:{2}", scroll.VerticalOffset, listFilter.SelectedIndex, scroll.ViewportHeight);
                        if (listFilter.SelectedIndex - scroll.VerticalOffset ==0)
                        {
                            scroll.LineUp();
                        }
                        listFilter.SelectedIndex--;
                    }
                }
            }
            else if(e.Key == Key.Enter)
            {
                Student stu = listFilter.SelectedItem as Student;
                if (stu != null)
                {
                    System.Console.WriteLine("{0},{1},{2}", stu.Name, stu.Age, stu.UserName);
                }
            }
        }
        
        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            //var ff = Keyboard.FocusedElement as ListViewItem;
            //Student dd = null;
            //if (ff != null)
            //{
            //    dd = ff.DataContext as Student;
            //}
            //if ((ff == null || dd == null) && !txtFilter.IsFocused)
            //{
            //    isFocus = false;
            //}
            //ListViewItem item = listFilter.ItemContainerGenerator.ContainerFromIndex(listFilter.SelectedIndex) as ListViewItem;
            //if(item != null)
            //    Console.WriteLine("lost:{0}{1}", txtFilter.IsFocused, item.IsFocused);
            this.Visibility = Visibility.Hidden;
        }

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            //var ff = Keyboard.FocusedElement as ListViewItem;
            //Student dd = null;
            //if (ff != null)
            //{
            //    dd = ff.DataContext as Student;
            //}
            //if ((ff == null || dd == null) && !txtFilter.IsFocused)
            //{
            //    isFocus = false;
            //}
            this.Visibility = Visibility.Visible;
        }

    }
}
