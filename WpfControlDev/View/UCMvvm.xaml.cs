using System;
using System.Windows;
using System.Windows.Media;
using WpfControlDev.ViewModel;

namespace WpfControlDev.View
{
    /// <summary>
    /// UCMvvm.xaml 的交互逻辑
    /// </summary>
    public partial class UCMvvm : Window
    {
        //private UcMvvmVm vm = new UcMvvmVm();
        public UCMvvm()
        {
            InitializeComponent();
            this.DataContext = new UcMvvmVm();
        }

        private void c1_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            Console.WriteLine(string.Format("C1 ORGB::{0}:{1}:{2}", e.OldValue.R, e.OldValue.G, e.OldValue.B));
            Console.WriteLine(string.Format("C1 NRGB::{0}:{1}:{2}", e.NewValue.R, e.NewValue.G, e.NewValue.B));
        }

        private void c2_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            Console.WriteLine(string.Format("C2 ORGB::{0}:{1}:{2}", e.OldValue.R, e.OldValue.G, e.OldValue.B));
            Console.WriteLine(string.Format("C2 NRGB::{0}:{1}:{2}", e.NewValue.R, e.NewValue.G, e.NewValue.B));
        }
    }
}
