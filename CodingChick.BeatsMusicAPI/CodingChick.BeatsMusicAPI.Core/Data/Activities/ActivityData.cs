using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Activities
{
    [JsonObject("Datum")]
    public class ActivityData : BaseData
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public int CreatedAt { get; set; }
        public int TotalEditorialPlaylists { get; set; }
    }
}
