using CodingChick.BeatsMusicAPI.Core.Endpoints;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Audio
{
    [JsonObject("Datum")]
    public class AudioData
    {
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("resource")]
        public string Resource { get; set; }
        [JsonProperty("codec")]
        public string Codec { get; set; }
        [JsonProperty("bitrate")]
        public int Bitrate { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("refs")]
        public AudioRefs Refs { get; set; }

    }
}