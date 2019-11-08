using Newtonsoft.Json;

namespace WpfWebDev.Model
{
    public class UserInfo: BaseModel
    {
        [JsonProperty(PropertyName = "user_id")]
        public string UserID { get; set; }
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "nick_name")]
        public string NickName { get; set; }
        [JsonProperty(PropertyName = "nick_icon")]
        public string NickIcon { get; set; }
        [JsonProperty(PropertyName = "user_phone")]
        public string UserPhone { get; set; }
        [JsonProperty(PropertyName = "user_email")]
        public string UserEmail { get; set; }
        [JsonProperty(PropertyName = "org_id")]
        public string OrgID { get; set; }
        [JsonIgnore]
        public OrgInfo OrgInfo { get; set; }
    }
}
