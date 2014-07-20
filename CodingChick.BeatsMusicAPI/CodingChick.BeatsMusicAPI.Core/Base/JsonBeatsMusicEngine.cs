using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Me;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Base
{
    internal class JsonBeatsMusicEngine : IJsonBeatsMusicEngine
    {
        public MultipleRootObject<T> ParseMultipleDataResponse<T>(string dataResponse)
        {
            var parsedDataResponse = JsonConvert.DeserializeObject<MultipleRootObject<T>>(dataResponse);
            return parsedDataResponse;
        }

        public MultipleRootObject<T> ParseMultipleDataResponseWithConverter<T>(string dataResponse)
        {
            var parsedDataResponse = JsonConvert.DeserializeObject<MultipleRootObject<T>>(dataResponse,
                new BaseDataConverter());
            return parsedDataResponse;
        }

        public SingleRootObject<T> ParseSingleDataResponse<T>(string dataResponse)
        {
            // This is a very annoying fix to make sure API calls are consistent and all return "data", 
            // and since "me" api is different in returning "result" I made sure all return the same.
            if (typeof(T) == typeof(MeData))
            {
                dataResponse = dataResponse.Replace("result", "data");
            }

            var parsedDataResponse = JsonConvert.DeserializeObject<SingleRootObject<T>>(dataResponse);
            return parsedDataResponse;
        }
    }
}