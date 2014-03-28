using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints.Enums
{
    public enum TracksOrderByData
    {
        [ParamValue("track_position")]
        TrackPosition,
        [ParamValue("title asc")]
        TitleAscending,
        [ParamValue("title desc")]
        TitleDescending,
        [ParamValue("popularity asc")]
        PopularityAscending,
        [ParamValue("popularity desc")]
        PopularityDesc,
        [ParamValue("release_date asc")]
        ReleaseDateAscending,
        [ParamValue("release_date desc")]
        ReleaseDateDescending,
        [ParamValue("original_release_date")]        
        OriginalReleaseDateAscending
    }
}