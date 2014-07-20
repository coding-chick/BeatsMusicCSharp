using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Ratings
{
    public class RatingData : BaseConvertedData
    {
        [JsonProperty("updated_at")]
        public int? UpdatedAt { get; set; }

        [JsonProperty("rated")]
        public RefTypeInfo Rated { get; set; }

        [JsonProperty("rating")]
        public Rating Rating { get; set; }
    }
}