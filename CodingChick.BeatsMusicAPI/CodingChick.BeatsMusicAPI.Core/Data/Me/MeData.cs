using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Me
{
    [JsonObject("Datum")]
    public class MeData
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        [JsonProperty("expires")]
        public string Expires { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("user_context")]
        public string UserContext { get; set; }

        [JsonProperty("extended")]
        public object Extended { get; set; }
    }
}
