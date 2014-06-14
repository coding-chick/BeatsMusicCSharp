using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data
{
    public abstract class RootObject<T> : IServerResponseProvider
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        public bool HasErrors
        {
            get { return Code.ToLower() != "ok"; }
        }

        string IServerResponseProvider.ServerJson { get; set; }
    }
}