using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data
{
    public class BaseData
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}