using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Data;

namespace CodingChick.BeatsMusicAPI.Core.Base
{
    internal class BeatsMusicManager : IBeatsMusicManager
    {
        private readonly IHttpBeatsMusicEngine _httpBeatsMusicEngine;
        private readonly IJsonBeatsMusicEngine _jsonBeatsMusicEngine;

        public BeatsMusicManager(IHttpBeatsMusicEngine httpBeatsMusicEngine, IJsonBeatsMusicEngine jsonBeatsMusicEngine)
        {
            _httpBeatsMusicEngine = httpBeatsMusicEngine;
            _jsonBeatsMusicEngine = jsonBeatsMusicEngine;
        }

        public async Task<MultipleRootObject<T>> GetMultipleParsedResult<T>(string methodName, List<KeyValuePair<string, string>> methodParams, bool useToken = false)
        {
            var dataResponse = await GetDataResponse(methodName, methodParams, useToken);
            var parsedDataResponse = _jsonBeatsMusicEngine.ParseMultipleDataResponse<T>(dataResponse);
            return parsedDataResponse;
        }

        public async Task<MultipleRootObject<T>> GetMultipleParsedResultWithConverter<T>(string methodName, List<KeyValuePair<string, string>> methodParams, bool useToken = false)
        {
            var dataResponse = await GetDataResponse(methodName, methodParams, useToken);
            var parsedDataResponse = _jsonBeatsMusicEngine.ParseMultipleDataResponseWithConverter<T>(dataResponse);
            return parsedDataResponse;
        }

        public async Task<SingleRootObject<T>> GetSingleParsedResult<T>(string methodName, List<KeyValuePair<string, string>> methodParams, bool useToken = false)
        {
            var dataResponse = await GetDataResponse(methodName, methodParams, useToken);
            var parsedDataResponse = _jsonBeatsMusicEngine.ParseSingleDataResponse<T>(dataResponse);
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
            if (dataParams == null)
                dataParams = new List<KeyValuePair<string, string>>();

            var httpResponse = await _httpBeatsMusicEngine.PostAsync(methodName, dataParams);
            var dataResponse = await httpResponse.ReadAsStringAsync();

            return _jsonBeatsMusicEngine.ParseSingleDataResponse<T>(dataResponse);
        }

        public async Task<SingleRootObject<T>> PutData<T>(string methodName, List<KeyValuePair<string, string>> dataParams, bool addCredentials = true)
        {
            if (dataParams == null)
                dataParams = new List<KeyValuePair<string, string>>();

            var httpResponse = await _httpBeatsMusicEngine.PutAsync(methodName, dataParams, addCredentials);
            var dataResponse = await httpResponse.ReadAsStringAsync();

            return _jsonBeatsMusicEngine.ParseSingleDataResponse<T>(dataResponse);
        }

        public async Task<bool> DeleteData(string methodName, List<KeyValuePair<string, string>> dataParams)
        {
            if (dataParams == null)
                dataParams = new List<KeyValuePair<string, string>>();

            var httpResponse = await _httpBeatsMusicEngine.DeleteAsync(methodName, dataParams);
            var dataResponse = await httpResponse.ReadAsStringAsync();

            if (dataResponse.ToLower().Contains("ok"))
                return true;
            return false;
        }
    }
}