using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints.Enums
{
    public enum AlbumsOrderBy
    {
       
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
        ReleaseDateDescending
    }
}