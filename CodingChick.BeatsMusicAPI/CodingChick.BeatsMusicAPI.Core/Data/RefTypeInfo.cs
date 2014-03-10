using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data
{
    public class RefTypeInfo
    {
        [JsonProperty("ref_type")]
        public string RefType { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("display")]
        public string Display { get; set; }
    }
}