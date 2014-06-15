using System;
using System.Linq;
using System.Text;
using CodingChick.BeatsMusicAPI.Core.Endpoints;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Tracks
{
    [JsonObject("Datum")]
    public class TrackData : BaseData
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("disc_number")]
        public int DiscNumber { get; set; }
        [JsonProperty("parental_advisory")]
        public bool ParentalAdvisory { get; set; }
        [JsonProperty("edited_version")]
        public bool EditedVersion { get; set; }
        [JsonProperty("duration")]
        public int Duration { get; set; }
        [JsonProperty("track_position")]
        public int TrackPosition { get; set; }
        [JsonProperty("popularity")]
        public int Popularity { get; set; }
        [JsonProperty("streamable")]
        public bool Streamable { get; set; }
        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }
        [JsonProperty("artist_display_name")]
        public string ArtistDisplayName { get; set; }
        [JsonProperty("refs")]
        public TrackRefs Refs { get; set; }
    }
}
