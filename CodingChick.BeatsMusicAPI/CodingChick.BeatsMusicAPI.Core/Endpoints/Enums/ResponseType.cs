using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints.Enums
{
    public enum ResponseType
    {
        [ParamValue("token")]
        Token,
        [ParamValue("code")]
        Code
    }
}