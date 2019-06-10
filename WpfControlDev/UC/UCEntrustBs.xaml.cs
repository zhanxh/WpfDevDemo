using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    /// UCEntrustBs.xaml 的交互逻辑
    /// </summary>
    public partial class UCEntrustBs : UserControl
    {
        public UCEntrustBs()
        {
            
            InitializeComponent();
            //var num = Numb;
            //buy.GroupName = "bs" + num;
            //sale.GroupName = "bs" + num;
        }
        //static int _numb = 0;
        //static int Numb
        //{
        //    get { return ++_numb; }
        //}

        static FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata('1',FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, EntrustBsPropertyChangedCallback);
        
        public static readonly DependencyProperty EntrustBsProperty = DependencyProperty.Register("BuySale", typeof(char), typeof(UCEntrustBs), metadata);


        private static void EntrustBsPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        public char BuySale
        {
            get
            {
                return (char)GetValue(EntrustBsProperty);

            }
            set
            {
                SetValue(EntrustBsProperty, value);
            }
        }

        private void buy_Click(object sender, RoutedEventArgs e)
        {
            //if (buy.IsChecked == true)
            //    BuySale = '1';
            //else
            //    BuySale = '2';
        }


        public static readonly DependencyProperty BsGroupNameProperty = DependencyProperty.Register("BsGroupName", typeof(string), typeof(UCEntrustBs), new PropertyMetadata(null));
        public string BsGroupName// { get; set; }
        {
            get
            {
                return (string)GetValue(BsGroupNameProperty);

            }
            set
            {
                SetValue(BsGroupNameProperty, value);
            }
        }
    }
    public class ConvertBs : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            char c = (char)value;
            if(parameter.ToString() == "b")
            {
                if (c == '1')
                    return true;
                else
                    return false;
            }
            if (parameter.ToString() == "s")
            {
                if (c == '2')
                    return true;
                else
                    return false;
            }
            if (parameter.ToString() == "o")
            {
                if (c == '1')
                    return true;
                else
                    return false;
            }
            if (parameter.ToString() == "c")
            {
                if (c == '2')
                    return true;
                else
                    return false;
            }
            if (parameter.ToString() == "t")
            {
                if (c == '4')
                    return true;
                else
                    return false;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter.ToString() == "s")
            {
                if ((bool)value == true)
                    return '2';
                else
                    return '1';
            }
            if (parameter.ToString() == "b")
            {
                if ((bool)value == true)
                    return '1';
                else
                    return '2';
            }

            if (parameter.ToString() == "o")
            {
                if ((bool)value)
                    return '1';
            }
            if (parameter.ToString() == "c")
            {
                if ((bool)value)
                    return '2';
            }
            if (parameter.ToString() == "t")
            {
                if ((bool)value)
                    return '4';
            }
            return null;
        }
    }
}
