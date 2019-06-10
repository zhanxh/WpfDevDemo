using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlDev.Model
{
    [Serializable]
    public class FileMsg
    {
        public string Name;
        public string DirName;
        public string FullName;
        public string sVersion;
        public Version Version;
        public DateTime LastUpDate;
        public override string ToString()
        {
            return string.Format("{0}\t{1}{2}", Name.PadRight(32, ' '), Version.ToString().PadRight(16, ' '), LastUpDate.ToString().PadRight(32, ' '));
        }
    }
}
