using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Content
{
    public class ContentRefs
    {
        [JsonProperty("artists")]
        public List<RefTypeInfo> Artists { get; set; }
        [JsonProperty("label")]
        public RefTypeInfo Label { get; set; }
        [JsonProperty("tracks")]
        public List<RefTypeInfo> Tracks { get; set; }
        [JsonProperty("user")]
        public RefTypeInfo User { get; set; }
        [JsonProperty("author")]
        public RefTypeInfo Author { get; set; }
    }
}