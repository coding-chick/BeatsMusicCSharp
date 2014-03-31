using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodingChick.BeatsMusicAPI.Core.Endpoints;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Playlists
{
    public class PlaylistsRefs
    {
        [JsonProperty("tracks")]
        public List<RefTypeInfo> Tracks { get; set; }
        [JsonProperty("user")]
        public RefTypeInfo User { get; set; }
        [JsonProperty("author")]
        public RefTypeInfo Author { get; set; }
    }
}
