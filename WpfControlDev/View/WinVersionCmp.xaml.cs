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
using System.Windows.Forms;
using WpfControlDev.Extend;
using WpfControlDev.Model;

namespace WpfControlDev.View
{
    /// <summary>
    /// WinVersionCmp.xaml 的交互逻辑
    /// </summary>
    public partial class WinVersionCmp : Window
    {
        public WinVersionCmp()
        {
            InitializeComponent();
            txt1.Text = "F:\\MT专业版(方正中期0125） - 副本\\MtLib";
            txt2.Text = "F:\\SVN\\PBOXClient\\trunk\\Sources\\MtProfession\\Target\\Debug\\MtLib";
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string m_Dir = m_Dialog.SelectedPath.Trim();
            txt1.Text = m_Dir;
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string m_Dir = m_Dialog.SelectedPath.Trim();
            txt2.Text = m_Dir;
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            List<FileMsg> fm1 = new List<FileMsg>();
            List<FileMsg> fm2 = new List<FileMsg>();
            FileOpr.getDirectory(txt1.Text, ref fm1);
            FileOpr.getDirectory(txt2.Text, ref fm2);
            StringBuilder sb = new StringBuilder();
            List<FileMsg> gyDll = new List<FileMsg>();
            foreach (var f1 in fm1)
            {
                var fx = fm2.FirstOrDefault(p => p.Name == f1.Name);
                if (fx != null)
                {
                    if (!fx.sVersion.Equals(f1.sVersion))
                    {
                        Console.WriteLine("12312312");
                    }
                    var str = string.Format("{0,16}{1,16}{2,8}  {3}\r\n", f1.sVersion, fx.sVersion, f1.sVersion.Equals(fx.sVersion), f1.Name);
                    sb.Append(str);
                    //sb.Append(f1.sVersion.Equals(fx.sVersion) + "\t\t" + f1.sVersion + "\t\t" + fx.sVersion + "\t\t" + f1.Name);
                    //sb.Append("\r\n");
                    gyDll.Add(fx);
                }
                else
                {
                    var str = string.Format("{0,16}  {1}\r\n", f1.sVersion, f1.Name);
                    sb.Append(str);
                }
            }
            var ex2 = fm2.Except(gyDll);
            foreach(var f2 in ex2)
            {
                var str = string.Format("{0,16}  {1}\r\n", f2.sVersion, f2.Name);
                sb.Append(str);
            }
            txtCmp.Text = sb.ToString();
        }
    }

    public class Cmp : IEqualityComparer<FileMsg>
    {
        public bool Equals(FileMsg x, FileMsg y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(FileMsg obj)
        {
            return 10086;
        }
    }
}
