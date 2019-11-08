using Newtonsoft.Json;

namespace WpfWebDev.Model
{
    public class ResponseData
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }
        [JsonProperty(PropertyName = "msg")]
        public string Msg { get; set; }
    }
}
