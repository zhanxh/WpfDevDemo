using Newtonsoft.Json;

namespace WpfWebDev.Model
{
    public class OrgInfo : BaseModel
    {
        [JsonProperty(PropertyName = "org_id")]
        public string OrgID { get; set; }
        [JsonProperty(PropertyName = "org_name")]
        public string OrgName { get; set; }
        [JsonProperty(PropertyName = "org_parent_id")]
        public string OrgParentID { get; set; }
    }
}
