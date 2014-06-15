using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Users
{
    [JsonObject("Datum")]
    public class UserData : BaseData
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("total_followed_by")]
        public int TotalFollowedBy { get; set; }
        [JsonProperty("total_follows")]
        public int TotalFollows { get; set; }
    }
}
