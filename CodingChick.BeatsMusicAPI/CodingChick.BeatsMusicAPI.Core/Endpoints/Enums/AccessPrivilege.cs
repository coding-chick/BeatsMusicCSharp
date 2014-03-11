using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints.Enums
{
    public enum AccessPrivilege
    {
        [ParamValue("public")]
        Public,
        [ParamValue("private")]
        Private
    }
}