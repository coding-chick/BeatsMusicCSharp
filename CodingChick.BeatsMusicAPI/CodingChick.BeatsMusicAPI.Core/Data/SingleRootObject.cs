using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data
{
    [JsonObject("RootObject")]
    public class SingleRootObject<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }

        public bool HasErrors
        {
            get { return Code.ToLower() != "ok"; }
        }
    }
}