using System;
using System.Linq;
using System.Text;
using CodingChick.BeatsMusicAPI.Core.Endpoints;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Albums
{
    [JsonObject("Datum")]
    public class AlbumData : BaseConvertedData
    {
        public string Title { get; set; }

        public int Duration { get; set; }

        public bool ParentalAdvisory { get; set; }

        public bool EditedVersion { get; set; }

        public string ReleaseDate { get; set; }

        public string ReleaseFormat { get; set; }

        public int Rating { get; set; }

        public int Popularity { get; set; }

        public bool Streamable { get; set; }
        public string ArtistDisplayName { get; set; }

        [JsonProperty("refs")]
        public AlbumRefs AlbumRefs { get; set; }

        public bool Canonical { get; set; }

        public int TotalCompanionAlbums { get; set; }

        public int TotalTracks { get; set; }

        public bool Essential { get; set; }
    }
}
