using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Search
{
    public class Related
    {
        [JsonProperty("ref_type")]
        public string RefereceType { get; set; }
        [JsonProperty("id")]        
        public string Id { get; set; }
        [JsonProperty("display")]
        public string Display { get; set; }
    }

}