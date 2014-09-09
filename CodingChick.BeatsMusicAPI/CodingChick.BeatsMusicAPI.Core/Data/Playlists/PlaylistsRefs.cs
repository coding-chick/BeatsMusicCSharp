using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodingChick.BeatsMusicAPI.Core.Endpoints;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data.Playlists
{
    public class PlaylistsRefs
    {
        public List<RefTypeInfo> Tracks { get; set; }
        public RefTypeInfo User { get; set; }
        public RefTypeInfo Author { get; set; }
    }
}
