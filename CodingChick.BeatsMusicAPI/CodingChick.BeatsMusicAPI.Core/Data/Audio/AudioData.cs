using CodingChick.BeatsMusicAPI.Core.Endpoints;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Audio
{
    [JsonObject("Datum")]
    public class AudioData : BaseData
    {
        public string Location { get; set; }
        public string Resource { get; set; }
        public string Codec { get; set; }
        public int Bitrate { get; set; }
        public string Type { get; set; }
        public AudioRefs Refs { get; set; }

    }
}