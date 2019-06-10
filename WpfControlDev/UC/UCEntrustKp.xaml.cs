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

namespace WpfControlDev.UC
{
    /// <summary>
    /// UCEntrustKp.xaml 的交互逻辑
    /// </summary>
    public partial class UCEntrustKp : UserControl
    {
        public UCEntrustKp()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty EntrustKpProperty = DependencyProperty.Register("OpenClose", typeof(char), typeof(UCEntrustKp));

        public char OpenClose
        {
            get
            {
                return (char)GetValue(EntrustKpProperty);

            }
            set
            {
                SetValue(EntrustKpProperty, value);
            }
        }

        private void buy_Click(object sender, RoutedEventArgs e)
        {

        }

        public static readonly DependencyProperty KpGroupNameProperty = DependencyProperty.Register("KpGroupName", typeof(string), typeof(UCEntrustKp), new PropertyMetadata(null));
        public string KpGroupName// { get; set; }
        {
            get
            {
                return (string)GetValue(KpGroupNameProperty);

            }
            set
            {
                SetValue(KpGroupNameProperty, value);
            }
        }
    }
}
