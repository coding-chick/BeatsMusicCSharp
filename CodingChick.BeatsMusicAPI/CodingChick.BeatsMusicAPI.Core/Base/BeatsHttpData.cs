using System.Collections.Generic;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Data;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Base
{
    internal class BeatsHttpData
    {
        private readonly IHttpBeatsMusicEngine _httpBeatsMusicEngine;

        public BeatsHttpData(IHttpBeatsMusicEngine httpBeatsMusicEngine)
        {
            _httpBeatsMusicEngine = httpBeatsMusicEngine;
        }

        public async Task<MultipleRootObject<T>> GetMultipleParsedResult<T>(string methodName, Dictionary<string, string> methodParams)
        {
            var dataResponse = await GetDataResponse(methodName, methodParams);

            var parsedDataResponse = JsonConvert.DeserializeObject<MultipleRootObject<T>>(dataResponse);

            return parsedDataResponse;
        }

        public async Task<SingleRootObject<T>> GetSingleParsedResult<T>(string methodName, Dictionary<string, string> methodParams)
        {
            var dataResponse = await GetDataResponse(methodName, methodParams);

            var parsedDataResponse = JsonConvert.DeserializeObject<SingleRootObject<T>>(dataResponse);

            return parsedDataResponse;
        }

        private async Task<string> GetDataResponse(string methodName, Dictionary<string, string> methodParams)
        {
            if (methodParams == null)
                methodParams = new Dictionary<string, string>();

            var contentResult =
                await _httpBeatsMusicEngine.GetAsyncNoToken(methodName, methodParams);

            var dataResponse = await contentResult.ReadAsStringAsync();
            return dataResponse;
        }


        public async Task<SingleRootObject<T>> PostData<T>(string methodName, Dictionary<string, string> dataParams)
        {
            var httpResponse = await _httpBeatsMusicEngine.PostAsync(methodName, dataParams);
            var dataResponse = await httpResponse.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SingleRootObject<T>>(dataResponse);
        }
    }
}