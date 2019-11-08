using Newtonsoft.Json;
using System;

namespace WpfWebDev.Model
{
    public class BaseModel
    {
        [JsonProperty(PropertyName = "id")]
        public uint ID { get; set; }
        [JsonProperty(PropertyName = "create_by")]
        public string CreatedBy { get; set; }
        [JsonProperty(PropertyName = "create_on")]
        public DateTime CreatedOn { get; set; }
        [JsonProperty(PropertyName = "update_by")]
        public string UpdatedBy { get; set; }
        [JsonProperty(PropertyName = "update_on")]
        public DateTime UpdatedOn { get; set; }
    }
}
