using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WpfWebDev.Tool
{
    public class HttpUtil
    {
        /// <summary>
        /// 请求URL
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <param name="inputParam">接收参数</param>
        /// <param name="method">请求类型:GET/POST</param>
        /// <returns>返回JSON格式结果</returns>
        public static string RequestUrl(string url, string inputParam, string method)
        {
            try
            {
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);//webrequest请求api地址
                request.Accept = "text/html,application/xhtml+xml,*/*";
                request.ContentType = "application/json";
                request.Method = method.ToUpper().ToString();
                byte[] buffer = encoding.GetBytes(inputParam);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"请求URL异常:{ex.Message + ex.StackTrace}");
            }
        }
    }
}
