using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints.Enums
{
    public enum AlbumsOrderBy
    {
        [ParamValue("title")]
        Title,
        [ParamValue("title asc")]
        TitleAscending,
        [ParamValue("title desc")]
        TitleDescending,
        [ParamValue("popularity")]
        Popularity,
        [ParamValue("popularity desc")]
        PopularityDesc,
        [ParamValue("release_date")]
        ReleaseDate,
        [ParamValue("release_date asc")]
        ReleaseDateAscending,
        [ParamValue("release_date desc")]
        ReleaseDateDescending
    }
}