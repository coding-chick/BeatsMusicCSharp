using System;
using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints.Enums
{
    [Flags]
    public enum TracksRefType
    {
        AllRefs = 1,
        [ParamValue("artists")]
        Artists = 2,
        [ParamValue("album")]
        Album = 4,
        [ParamValue("genres")]        
        Genres = 8
    }
}