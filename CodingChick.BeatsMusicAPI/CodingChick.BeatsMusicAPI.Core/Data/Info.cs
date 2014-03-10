using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data
{
    public class Info
    {
        [JsonProperty("offset")]
        public int Offset { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}