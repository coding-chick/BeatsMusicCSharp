using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints.Enums
{
    public enum FollowType
    {
        [ParamValue("users")]
        User,
        [ParamValue("curators")]
        Curator,
        [ParamValue("genres")]
        Genre
    }
}