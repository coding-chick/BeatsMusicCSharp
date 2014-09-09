using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data
{
    [JsonObject("RootObject")]
    public class MultipleRootObject<T>  : RootObject<T>
    {
        public List<T> Data { get; set; }

        public Info Info { get; set; }
    }
}