using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WpfControlDev.Tool
{
    public class DownLoadTest
    {
        Stopwatch watch = new Stopwatch();
        public DownLoadTest()
        {
            watch.Start();
        }
        public async Task<string> DownLoadStringTaskAsync(string url)
        {
            Debug.WriteLine(string.Format("异步程序获取{0}开始运行:{1,4:N0}ms", url, watch.Elapsed.TotalMilliseconds));
            WebClient wc = new WebClient();
            string str = await wc.DownloadStringTaskAsync(url);
            Debug.WriteLine(string.Format("异步程序获取{0}运行结束:{1,4:N0}ms", url, watch.Elapsed.TotalMilliseconds));
            return url+">>结果";
        }
    }
}
