using System;
using System.Linq;
using System.Text;
using CodingChick.BeatsMusicAPI.Core.Endpoints;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Artists
{
    [JsonObject("Datum")]
    public class ArtistData : BaseData
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("popularity")]
        public int Popularity { get; set; }
        [JsonProperty("streamable")]
        public bool Streamable { get; set; }
        [JsonProperty("total_albums")]
        public string TotalAlbums { get; set; }
        [JsonProperty("total_singles")]
        public string TotalSingles { get; set; }
        [JsonProperty("total_eps")]
        public string TotalEps { get; set; }
        [JsonProperty("total_lps")]
        public string TotalLps { get; set; }
        [JsonProperty("total_freeplays")]
        public int TotalFreeplays { get; set; }
        [JsonProperty("total_compilations")]
        public string TotalVompilations { get; set; }
        [JsonProperty("total_tracks")]
        public string TotalTracks { get; set; }
        [JsonProperty("refs")]
        public ArtistRefs Refs { get; set; }
        [JsonProperty("verified")]
        public bool Verified { get; set; }
        [JsonProperty("total_follows")]
        public int TotalFollows { get; set; }
        [JsonProperty("total_followed_by")]
        public int TotalFollowedBy { get; set; }
    }
}
