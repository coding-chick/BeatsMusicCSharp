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
        public string Type { get; set; }
        public string Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public int TotalFollowedBy { get; set; }
        public int TotalFollows { get; set; }
    }
}
