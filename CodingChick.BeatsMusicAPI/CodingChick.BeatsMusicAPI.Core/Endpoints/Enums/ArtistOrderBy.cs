using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints.Enums
{
    public enum ArtistOrderBy
    {
        [ParamValue("popularity desc")]
        PopularityDescending,
        [ParamValue("name asc")]
        NameAscending,
        [ParamValue("name desc")]
        NameDescending
    }
}