using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfWebDev.Tool
{
    public class JsonUtil
    {
        public static string SerializeObject<T>(T t)
        {
            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.NullValueHandling = NullValueHandling.Ignore; //忽略null值
            return JsonConvert.SerializeObject(t, Formatting.Indented, jsetting);
        }

        public static T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
