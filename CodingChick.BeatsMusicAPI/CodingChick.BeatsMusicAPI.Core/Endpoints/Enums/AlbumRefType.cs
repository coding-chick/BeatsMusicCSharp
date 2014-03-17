using System;
using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints.Enums
{
    [Flags]
    public enum AlbumRefType
    {
        AllRefs = 1,
        [ParamValue("artists")]
        Artists = 2,
        [ParamValue("album")]
        Album = 4,
        [ParamValue("label")]
        Label = 8
    }
}