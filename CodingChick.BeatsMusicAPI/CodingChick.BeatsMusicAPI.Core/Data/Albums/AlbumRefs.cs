using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Albums
{
    public class AlbumRefs
    {
        public List<RefTypeInfo> Artists { get; set; }
        public RefTypeInfo Label { get; set; }
        public List<RefTypeInfo> Tracks { get; set; }
    }
}