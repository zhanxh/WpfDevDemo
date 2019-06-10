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
using System.Windows.Shapes;
using WpfControlDev.ViewModel;

namespace WpfControlDev.View
{
    /// <summary>
    /// WinListView.xaml 的交互逻辑
    /// </summary>
    public partial class WinListView : Window
    {
        public WinListView()
        {
            InitializeComponent();
            this.DataContext = new StudentsViewModel();
        }

        private void testBtn_Click(object sender, RoutedEventArgs e)
        {
            GridView gv = listFilter.View as GridView;
            //Console.WriteLine(listFilter.Width);
            Console.WriteLine(listFilter.ActualWidth);
            var cols = gv.Columns;
            double TotalColWidth = 0;
            foreach(var col in cols)
            {
                Console.WriteLine("col:" + col.ActualWidth);
                TotalColWidth += col.ActualWidth;
            }
            double precent = 1;
            if (TotalColWidth < listFilter.ActualWidth)
            {
                precent = listFilter.ActualWidth / TotalColWidth;
                if (precent == 1)
                    return;
                foreach (var col in cols)
                {
                    col.Width = col.ActualWidth * precent;
                    //col.Width = double.NaN; 触发列宽自适应
                }
            }
            
        }

        Dictionary<string, bool> showMap = new Dictionary<string, bool>();
        private void listFilter_Loaded(object sender, RoutedEventArgs e)
        {
            //showMap.Add("姓名", true);
            //showMap.Add("年龄", true);
            //showMap.Add("名称", true);
            //showMap.Add("交易所", true);
            //showMap.Add("备注", false);

            //GridView gv = listFilter.View as GridView;
            //var cols = gv.Columns;
            //foreach(var col in cols)
            //{
            //    if (showMap.ContainsKey(col.Header.ToString()))
            //    {
            //        var bshow = showMap[col.Header.ToString()];
            //        if (!bshow)
            //        {
            //            col.Width = 0;
            //        }
            //    }

            //}
        }

        private void listFilter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Hehe");
        }

        private void rbBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("click");
        }
    }
}
