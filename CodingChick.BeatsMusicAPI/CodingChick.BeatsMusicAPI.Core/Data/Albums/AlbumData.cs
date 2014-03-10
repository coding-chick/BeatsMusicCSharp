using System;
using System.Linq;
using System.Text;
using CodingChick.BeatsMusicAPI.Core.Endpoints;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Albums
{
    [JsonObject("Datum")]
    public class AlbumData
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("parental_advisory")]
        public bool ParentalAdvisory { get; set; }

        [JsonProperty("edited_version")]
        public bool EditedVersion { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("release_format")]
        public string ReleaseFormat { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }

        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        [JsonProperty("streamable")]
        public bool Streamable { get; set; }

        [JsonProperty("artist_display_name")]
        public string ArtistDisplayName { get; set; }

        [JsonProperty("refs")]
        public AlbumRefs AlbumRefs { get; set; }

        [JsonProperty("canonical")]
        public bool Canonical { get; set; }

        [JsonProperty("total_companion_albums")]
        public int TotalCompanionAlbums { get; set; }

        [JsonProperty("total_tracks")]
        public int TotalTracks { get; set; }

        [JsonProperty("essential")]
        public bool Essential { get; set; }
    }
}
