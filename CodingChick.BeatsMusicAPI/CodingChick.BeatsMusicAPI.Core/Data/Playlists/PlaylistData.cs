using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodingChick.BeatsMusicAPI.Core.Endpoints;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Playlists
{
    [JsonObject("Datum")]    
    public class PlaylistData : BaseConvertedData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserDisplayName { get; set; }
        public string Access { get; set; }
        public int Duration { get; set; }
        public int TotalTracks { get; set; }
        public int TotalSubscribers { get; set; }
        public int CreatedAt { get; set; }
        public int UpdatedAt { get; set; }
        public bool ParentalAdvisory { get; set; }
        public PlaylistsRefs Refs { get; set; }
    }
}
