using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data
{
    [JsonObject("RootObject")]
    public class MultipleRootObject<T>  : RootObject<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }
    }
}