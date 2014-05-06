using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Audio
{
    public class AudioRefs
    {
        [JsonProperty("track")]
        public RefTypeInfo Track { get; set; }
    }
}