using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Search
{
    [JsonObject("Datum")]
    public class SearchData
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("result_type")]
        public string ResultType { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("display")]
        public string Display { get; set; }
        [JsonProperty("detail")]
        public string Detail { get; set; }
        [JsonProperty("related")]
        public Related Related { get; set; }
    }
}