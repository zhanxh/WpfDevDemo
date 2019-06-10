using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControlDev.Model;

namespace WpfControlDev.Extend
{
    public class FileOpr
    {
        public static void getDirectory(string path, ref List<FileMsg> filemsgs)
        {
            DirectoryInfo root = new DirectoryInfo(path);
            foreach (DirectoryInfo d in root.GetDirectories())
            {
                getDirectory(d.FullName, ref filemsgs);
            }
            FindFile(root, ref filemsgs);
        }
        public static void FindFile(DirectoryInfo di, ref List<FileMsg> filemsgs)
        {
            Debug.WriteLine(di.FullName);
            FileInfo[] fis = di.GetFiles();
            foreach (var info in fis)
            {
                //if (info.Name.EndsWith(".txt"))
                //    Logs.Add(info.FullName);
                if (!info.Name.EndsWith(".dll") && !info.Name.EndsWith(".exe"))
                    continue;
                //Debug.WriteLine(info.FullName);
                FileMsg fm = new FileMsg();
                fm.Name = info.Name;
                fm.FullName = info.FullName;
                fm.LastUpDate = info.LastWriteTime;
                fm.DirName = info.Directory.FullName;
                var fi = FileVersionInfo.GetVersionInfo(info.FullName);
                var vv = FileVersionInfo.GetVersionInfo(info.FullName).FileVersion;
                if (vv != null)
                {
                    vv = vv?.Replace(',', '.');
                    fm.Version = new Version(vv);
                    fm.sVersion = vv;
                }
                else
                {
                    fm.Version = new Version();
                    fm.sVersion = "";
                }
                //text_notice.Text += fm.ToString() + "\r\n";
                filemsgs.Add(fm);
            }
        }
    }
}
