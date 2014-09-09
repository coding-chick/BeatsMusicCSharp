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
        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public bool Verified { get; set; }
        public int TotalFollows { get; set; }
        public int TotalFollowedBy { get; set; }
        public int PlaylistCount { get; set; }
        public string Type { get; set; }
    }

}
