using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Reviews
{
    [JsonObject("Datum")]
    public class ReviewData
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("headline")]
        public string Headline { get; set; }
        [JsonProperty("rating")]
        public string Rating { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("subject")]
        public RefTypeInfo Subject { get; set; }
    }
}
