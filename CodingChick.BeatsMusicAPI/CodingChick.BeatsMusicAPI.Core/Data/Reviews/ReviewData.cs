using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Reviews
{
    [JsonObject("Datum")]
    public class ReviewData : BaseData
    {
        public string Type { get; set; }
        public string Content { get; set; }
        public string Headline { get; set; }
        public string Rating { get; set; }
        public string Source { get; set; }
        public string Author { get; set; }
        public RefTypeInfo Subject { get; set; }
    }
}
