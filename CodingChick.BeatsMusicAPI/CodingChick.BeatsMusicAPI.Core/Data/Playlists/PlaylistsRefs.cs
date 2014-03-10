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
        //TODO: this is not object, get a real result to parse correctly
        [JsonProperty("tracks")]
        public List<object> Tracks { get; set; }
        [JsonProperty("user")]
        public RefTypeInfo User { get; set; }
        [JsonProperty("author")]
        public RefTypeInfo Author { get; set; }
    }
}
