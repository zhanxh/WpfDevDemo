using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// WinTimerTestxaml.xaml 的交互逻辑
    /// </summary>
    public partial class WinTimerTest : Window
    {
        private Timer aTimer = null;
        public WinTimerTest()
        {
            InitializeComponent();
            aTimer = new Timer();
            aTimer.Elapsed += new ElapsedEventHandler((obj,args)=> 
            {
                Console.WriteLine("2:" + DateTime.Now.ToString());
            });
            aTimer.Interval = 3000;
            aTimer.Enabled = true;
            Console.WriteLine("1:" + DateTime.Now.ToString());
            //aTimer.Start();
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("3:" + DateTime.Now.ToString());
            aTimer.Stop();
            aTimer.Start();
        }
    }
}
