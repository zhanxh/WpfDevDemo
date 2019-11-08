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
using WpfWebDev.Common;

namespace WpfWebDev
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            var userName = user.Text.Trim();
            var password = pwd.Password.Trim();
            var rspInfo = CommonFunction.Login(userName, password);
            if(rspInfo == null)
            {
                MessageBox.Show("登陆失败！！！");
            }
            else
            {
                if (rspInfo.Code == 200)
                {
                    GlobalParamsTL.UserTocken = rspInfo.Data.Token;
                }
                string msg = string.Format("code:{0},msg:{1},data:{2}", rspInfo.Code, rspInfo.Msg??"", rspInfo.Data?.ToString() ?? "");
                MessageBox.Show(msg);
            }
        }
    }
}
