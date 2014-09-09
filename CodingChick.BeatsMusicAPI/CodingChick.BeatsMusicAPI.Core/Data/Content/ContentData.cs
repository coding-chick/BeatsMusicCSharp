
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Content
{
    [JsonObject("Datum")]    
    public class ContentData : BaseData
    {
        public string Type { get; set; }
        public string Id { get; set; }
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
        public ContentRefs Refs { get; set; }
        public bool Canonical { get; set; }
        public int TotalCompanionAlbums { get; set; }
        public int TotalTracks { get; set; }
        public bool Essential { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserDisplayName { get; set; }
        public string Access { get; set; }
        public int? TotalSubscribers { get; set; }
        public int? CreatedAt { get; set; }
        public int? UpdatedAt { get; set; }
    }
}