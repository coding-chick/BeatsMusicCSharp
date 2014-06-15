using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Genres
{
    [JsonObject("Datum")]
    public class GenreData : BaseData
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("verified")]
        public bool Verified { get; set; }
        [JsonProperty("total_follows")]
        public int TotalFollows { get; set; }
        [JsonProperty("total_followed_by")]
        public int TotalFollowedBy { get; set; }
        [JsonProperty("playlist_count")]
        public int PlaylistCount { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }

}
