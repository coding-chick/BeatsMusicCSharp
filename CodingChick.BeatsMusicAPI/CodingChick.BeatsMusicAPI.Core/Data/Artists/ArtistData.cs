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
        public string Type { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public int Popularity { get; set; }
        public bool Streamable { get; set; }
        public string TotalAlbums { get; set; }
        public string TotalSingles { get; set; }
        public string TotalEps { get; set; }
        public string TotalLps { get; set; }
        public int TotalFreeplays { get; set; }
        public string TotalCompilations { get; set; }
        public string TotalTracks { get; set; }
        public ArtistRefs Refs { get; set; }
        public bool Verified { get; set; }
        public int TotalFollows { get; set; }
        public int TotalFollowedBy { get; set; }
    }
}
