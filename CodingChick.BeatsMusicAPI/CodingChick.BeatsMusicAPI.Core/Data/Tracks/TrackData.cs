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
        public string Type { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public int DiscNumber { get; set; }
        public bool ParentalAdvisory { get; set; }
        public bool EditedVersion { get; set; }
        public int Duration { get; set; }
        public int TrackPosition { get; set; }
        public int Popularity { get; set; }
        public bool Streamable { get; set; }
        public string ReleaseDate { get; set; }
        public string ArtistDisplayName { get; set; }
        public TrackRefs Refs { get; set; }
    }
}
