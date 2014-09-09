using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data
{
    [JsonObject("RootObject")]
    public class SingleRootObject<T> : RootObject<T>
    {
        public T Data { get; set; }
        
    }
}