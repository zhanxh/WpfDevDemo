using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfWebDev.Model;
using WpfWebDev.Tool;

namespace WpfWebDev.Common
{
    public class CommonFunction
    {
        public static LoginRsp Login(string user, string pwd)
        {
            LoginInfo loginInfo = new LoginInfo() { UserName = user, Password = pwd };
            var data = JsonUtil.SerializeObject(loginInfo);
            var rspData = HttpUtil.RequestUrl("http://127.0.0.1:8808/login", data, "POST");
            var rsp = JsonUtil.DeserializeObject<LoginRsp>(rspData);
            return rsp;
        }
    }
}
