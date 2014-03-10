using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Albums
{
    public class AlbumRefs
    {
        [JsonProperty("artists")]
        public List<RefTypeInfo> Artists { get; set; }
        [JsonProperty("label")]
        public RefTypeInfo Label { get; set; }
        [JsonProperty("tracks")]
        public List<RefTypeInfo> Tracks { get; set; }
    }
}