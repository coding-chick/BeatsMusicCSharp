using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodingChick.BeatsMusicAPI.Core.Data
{
    public class BaseConvertedData : BaseData
    {
        public string Type { get; set; }

        public string Id { get; set; }
    }

    public abstract class BaseData
    {
        [JsonExtensionData]
        public IDictionary<string, JToken> AdditionalData;
    }
}