using CodingChick.BeatsMusicAPI.Core.Data;

namespace CodingChick.BeatsMusicAPI.Core.Base
{
    public interface IJsonBeatsMusicEngine
    {
        MultipleRootObject<T> ParseMultipleDataResponse<T>(string dataResponse);
        MultipleRootObject<T> ParseMultipleDataResponseWithConverter<T>(string dataResponse);
        SingleRootObject<T> ParseSingleDataResponse<T>(string dataResponse);
    }
}