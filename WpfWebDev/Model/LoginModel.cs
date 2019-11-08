using Newtonsoft.Json;

namespace WpfWebDev.Model
{
    public class LoginInfo
    {
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }


    public class LoginRst
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
        [JsonProperty(PropertyName = "user")]
        public UserInfo User { get; set; }
    }

    public class LoginRsp:ResponseData
    {
        [JsonProperty(PropertyName = "data")]
        public LoginRst Data { get; set; }
    }
}
