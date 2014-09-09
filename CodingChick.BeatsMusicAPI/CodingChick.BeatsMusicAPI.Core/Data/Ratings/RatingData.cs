using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Ratings
{
    public class RatingData : BaseConvertedData
    {
        public int? UpdatedAt { get; set; }
        public RefTypeInfo Rated { get; set; }
        public Rating Rating { get; set; }
    }
}