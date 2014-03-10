using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodingChick.BeatsMusicAPI.Core.Endpoints;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Playlists
{
    [JsonObject("Datum")]    
    public class PlaylistData
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("user_display_name")]
        public string UserDisplayName { get; set; }
        [JsonProperty("access")]
        public string Access { get; set; }
        [JsonProperty("duration")]
        public int Duration { get; set; }
        [JsonProperty("total_tracks")]
        public int TotalTracks { get; set; }
        [JsonProperty("total_subscribers")]
        public int TotalSubscribers { get; set; }
        [JsonProperty("created_at")]
        public int CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public int UpdatedAt { get; set; }
        [JsonProperty("parental_advisory")]
        public bool ParentalAdvisory { get; set; }
        [JsonProperty("refs")]
        public PlaylistsRefs Refs { get; set; }
    }
}
