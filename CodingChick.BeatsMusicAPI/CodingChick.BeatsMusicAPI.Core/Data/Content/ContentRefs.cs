using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Content
{
    public class ContentRefs
    {
        public List<RefTypeInfo> Artists { get; set; }
        public RefTypeInfo Label { get; set; }
        public List<RefTypeInfo> Tracks { get; set; }
        public RefTypeInfo User { get; set; }
        public RefTypeInfo Author { get; set; }
    }
}