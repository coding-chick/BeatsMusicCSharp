using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Me
{
    [JsonObject("Datum")]
    public class MeData : BaseData
    {
        public string ClientId { get; set; }
        
        public string TokenType { get; set; }
        
        public string GrantType { get; set; }

        public string Expires { get; set; }

        public string Scope { get; set; }

        public string UserContext { get; set; }

        public object Extended { get; set; }
    }
}
