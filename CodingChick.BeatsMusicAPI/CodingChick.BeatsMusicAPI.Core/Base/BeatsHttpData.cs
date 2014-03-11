using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public async Task<MultipleRootObject<T>> GetMultipleParsedResult<T>(string methodName, List<KeyValuePair<string, string>> methodParams, bool useToken = false)
        {
            var dataResponse = await GetDataResponse(methodName, methodParams, useToken);

            var parsedDataResponse = JsonConvert.DeserializeObject<MultipleRootObject<T>>(dataResponse);

            return parsedDataResponse;
        }

        public async Task<SingleRootObject<T>> GetSingleParsedResult<T>(string methodName, Dictionary<string, string> methodParams, bool useToken = false)
        {
            var dataResponse = await GetDataResponse(methodName, methodParams.ToList(), useToken);

            var parsedDataResponse = JsonConvert.DeserializeObject<SingleRootObject<T>>(dataResponse);

            return parsedDataResponse;
        }

        private async Task<string> GetDataResponse(string methodName, List<KeyValuePair<string, string>> methodParams, bool useToken = false)
        {
            if (methodParams == null)
                methodParams = new List<KeyValuePair<string, string>>();

            HttpContent contentResult;
            if (useToken)
                contentResult = await _httpBeatsMusicEngine.GetAsyncWithToken(methodName, methodParams);
            else
                contentResult = await _httpBeatsMusicEngine.GetAsyncNoToken(methodName, methodParams);

            var dataResponse = await contentResult.ReadAsStringAsync();
            return dataResponse;
        }


        public async Task<SingleRootObject<T>> PostData<T>(string methodName, List<KeyValuePair<string, string>> dataParams)
        {
            var httpResponse = await _httpBeatsMusicEngine.PostAsync(methodName, dataParams);
            var dataResponse = await httpResponse.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SingleRootObject<T>>(dataResponse);
        }
    }
}