using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints.Enums
{
    public enum PlaylistsOrderBy
    {
        [ParamValue("name asc")]
        NameAscending,
        [ParamValue("name desc")]
        NameDescending,
        [ParamValue("updated_at asc")]
        CreatedAtAscending,
        [ParamValue("created_at desc")]
        CreatedAtDescending,
        [ParamValue("updated_at asc")]
        UpdatedAtAscending,
        [ParamValue("updated_at desc")]
        UpdatedAtDescending,
        [ParamValue("duration asc")]
        DurationAscending,
        [ParamValue("duration desc")]
        DurationDescending,
        [ParamValue("total_tracks asc")]
        TotalTracksAscending,
        [ParamValue("total_tracks desc")]
        TotalTracksDescending
    }
}