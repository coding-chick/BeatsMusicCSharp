using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Search
{
    public class Related
    {
        [JsonProperty("ref_type")]
        public string RefereceType { get; set; }
        public string Id { get; set; }
        public string Display { get; set; }
    }

}