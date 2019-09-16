using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfControlDev.Model;
using WpfControlDev.Provider;
using WpfControlDev.Tool;

namespace WpfControlDev.View
{
    /// <summary>
    /// WinMain.xaml 的交互逻辑
    /// </summary>
    public partial class WinMain : Window
    {
        public WinMain()
        {
            InitializeComponent();
            Test();
        }

        private void Test()
        {
            var tp = typeof(ViewModel.UcMvvmVm);
            var props = tp.GetProperties();
            var ms = tp.GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            foreach(var prop in props)
            {
                Console.WriteLine(prop.Name);
            }
        }

        private void gvBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt1 = DateTime.Now;
            WinListView wlv = new WinListView();
            wlv.Owner = this;
            wlv.Show();
            DateTime dt2 = DateTime.Now;
            TimeSpan ts = dt2 - dt1;
            Console.WriteLine("ls ms:" + ts.TotalMilliseconds);

        }

        private void canvasBtn_Click(object sender, RoutedEventArgs e)
        {
            WinCanvas wc = new WinCanvas();
            wc.Owner = this;
            wc.Show();
        }

        private void gdFilter_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt1 = DateTime.Now;
            WinDatagridFilter gdf = new WinDatagridFilter();
            gdf.Owner = this;
            gdf.Show();
            DateTime dt2 = DateTime.Now;
            TimeSpan ts = dt2 - dt1;
            Console.WriteLine("gd ms:" + ts.TotalMilliseconds);
        }

        private void GroupTest_Click(object sender, RoutedEventArgs e)
        {
            var dd = StudentService.GetStudents();
            var gp = dd.GroupBy(p => p.Exchange);
            foreach(var g in gp)
            {
                Console.WriteLine(g.Key);
                foreach(var p in g)
                {
                    Console.WriteLine(p.ToString());
                }
            }

            var datas = GroupDataProvider.GetGroupData();
            //方法一
            var cmp = (from t in datas group t by new { CompanyID = t.CompanyID } into g select new CCompany() { CompanyID = g.Key.CompanyID }).ToList();
            //方法二
            var cmp1 = datas.GroupBy(p => p.CompanyID).Select(p => new CCompany() { CompanyID= p.Key }).ToList();
            //方法二
            var deps = datas.GroupBy(p => new { p.CompanyID, p.DepartmentID }).Select(p => new CDepartment() { CompanyID = p.Key.CompanyID, DepartmentID = p.Key.DepartmentID });
        }

        int count = 0;
        private async void asyncUITest_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + ":" + count);
            var a = await Task.Run(() =>
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " " + count);
                Thread.Sleep(1000);
                return count++;
            });
            
            msgTxt.Content = "async" + a;

            Test ts = new Test();
            ts.HTest1();
            //Test.TaskMethodTest();
        }

        private void dgCombox_Click(object sender, RoutedEventArgs e)
        {
            WinDataGridComboxEdit win = new WinDataGridComboxEdit();
            win.Owner = this;
            win.ShowDialog();
        }

        private void ucMvvm_Click(object sender, RoutedEventArgs e)
        {
            UCMvvm ucMvvm = new UCMvvm();
            ucMvvm.Owner = this;
            ucMvvm.Show();
        }

        private void timerBtn_Click(object sender, RoutedEventArgs e)
        {
            WinTimerTest wtt = new WinTimerTest();
            wtt.Owner = this;
            wtt.ShowDialog();
        }

        private void cmpBtn_Click(object sender, RoutedEventArgs e)
        {
            WinVersionCmp cmp = new WinVersionCmp();
            cmp.Owner = this;
            cmp.Show();
        }

        private void brushBtn_Click(object sender, RoutedEventArgs e)
        {
            WinLinearGradientBrush lgb = new WinLinearGradientBrush();
            lgb.Owner = this;
            lgb.Show();
        }

        private void lbBtn_Click(object sender, RoutedEventArgs e)
        {
            WinListBoxStyle lbs = new WinListBoxStyle();
            lbs.Owner = this;
            lbs.Show();
        }

        private void tvsBtn_Click(object sender, RoutedEventArgs e)
        {
            WinTreeViewStyle tvs = new WinTreeViewStyle();
            tvs.Owner = this;
            tvs.Show();
        }

        private void tvsEAgg_Click(object sender, RoutedEventArgs e)
        {
            WinEventAggregator wea = new WinEventAggregator();
            wea.Owner = this;
            wea.Show();
        }

        private void dgColumnCombox_Click(object sender, RoutedEventArgs e)
        {
            WinDatagridComboLinkage wdgcl = new WinDatagridComboLinkage();
            wdgcl.Owner = this;
            wdgcl.Show();
        }

        private void tgbtn_Click(object sender, RoutedEventArgs e)
        {
            WinToggleButtonStyle wtbs = new WinToggleButtonStyle();
            wtbs.Owner = this;
            wtbs.Show();
        }

        private void checkboxBtn_Click(object sender, RoutedEventArgs e)
        {
            WinCheckBoxStyle wcbs = new WinCheckBoxStyle();
            wcbs.Owner = this;
            wcbs.Show();
        }

        private void RepeatBtn_OnClick(object sender, RoutedEventArgs e)
        {
            WinRepeatBtnStyle win = new WinRepeatBtnStyle();
            win.Owner = this;
            win.Show();
        }

        private void AutoGridBtn_OnClick(object sender, RoutedEventArgs e)
        {
            WinAutoGird win = new WinAutoGird();
            win.Owner = this;
            win.Show();
        }

        private void Ctrls_OnClick(object sender, RoutedEventArgs e)
        {
            WinCtrls win = new WinCtrls();
            win.Owner = this;
            win.Show();
        }

        private void Menus_OnClick(object sender, RoutedEventArgs e)
        {
            WinMenu win = new WinMenu();
            win.Owner = this;
            win.Show();
        }
    }
}
