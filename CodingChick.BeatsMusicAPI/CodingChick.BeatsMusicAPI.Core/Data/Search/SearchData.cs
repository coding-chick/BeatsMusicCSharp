using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Search
{
    [JsonObject("Datum")]
    public class SearchData : BaseData
    {
        public string Type { get; set; }
        public string ResultType { get; set; }
        public string Id { get; set; }
        public string Display { get; set; }
        public string Detail { get; set; }
        public Related Related { get; set; }
    }
}