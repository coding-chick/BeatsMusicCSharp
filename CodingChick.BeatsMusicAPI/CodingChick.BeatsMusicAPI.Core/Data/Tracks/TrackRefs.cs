using System.Collections.Generic;
using CodingChick.BeatsMusicAPI.Core.Endpoints;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Tracks
{
    public class TrackRefs
    {
        [JsonProperty("artists")]
        public List<RefTypeInfo> Artists { get; set; }
        [JsonProperty("album")]
        public RefTypeInfo Album { get; set; }
    }
}