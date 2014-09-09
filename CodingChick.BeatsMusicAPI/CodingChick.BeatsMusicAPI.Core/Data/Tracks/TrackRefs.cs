using System.Collections.Generic;
using CodingChick.BeatsMusicAPI.Core.Endpoints;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Tracks
{
    public class TrackRefs
    {
        public List<RefTypeInfo> Artists { get; set; }
        public RefTypeInfo Album { get; set; }
    }
}