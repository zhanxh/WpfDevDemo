using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfControlDev.View
{
    /// <summary>
    /// WinColorAnimation.xaml 的交互逻辑
    /// </summary>
    public partial class WinColorAnimation : Window
    {
        public WinColorAnimation()
        {
            InitializeComponent();
        }

        private void rect_TargetUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var solid = new SolidColorBrush(Colors.Transparent);
            rect.Fill = solid;
            var colorAnm = new ColorAnimation(Colors.Green, Colors.Blue, new Duration(TimeSpan.FromSeconds(5)));
            //colorAnm.From = Colors.Red;
            //colorAnm.To = Colors.Blue;
            //var solid = rect.Fill;
            solid.BeginAnimation(SolidColorBrush.ColorProperty, colorAnm);
        }
    }
}
