using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data
{
    [JsonObject("RootObject")]
    public class MultipleRootObject<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        public bool HasErrors
        {
            get { return Code.ToLower() == "ok"; }
        }
    }
}