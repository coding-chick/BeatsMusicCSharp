using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Me;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Base
{
    internal class BeatsHttpData : IBeatsHttpData
    {
        private readonly IHttpBeatsMusicEngine _httpBeatsMusicEngine;

        public BeatsHttpData(IHttpBeatsMusicEngine httpBeatsMusicEngine)
        {
            _httpBeatsMusicEngine = httpBeatsMusicEngine;
        }

        public async Task<MultipleRootObject<T>> GetMultipleParsedResult<T>(string methodName,
            List<KeyValuePair<string, string>> methodParams, bool useToken = false)
        {
            string dataResponse = await GetDataResponse(methodName, methodParams, useToken);

            MultipleRootObject<T> parsedDataResponse = ParsedMultipleDataResponse<T>(dataResponse);
            return parsedDataResponse;
        }

        public async Task<SingleRootObject<T>> GetSingleParsedResult<T>(string methodName,
            List<KeyValuePair<string, string>> methodParams, bool useToken = false)
        {
            string dataResponse = await GetDataResponse(methodName, methodParams, useToken);

            // This is a very annoying fix to make sure API calls are consistent and all return "data", and since "me" api is different in returning "result" I made sure all return the same.
            if (typeof (T) == typeof (MeData))
            {
                dataResponse = dataResponse.Replace("result", "data");
            }

            SingleRootObject<T> parsedDataResponse = ParsedSingleDataResponse<T>(dataResponse);

            return parsedDataResponse;
        }

        public async Task<SingleRootObject<T>> PostData<T>(string methodName,
            List<KeyValuePair<string, string>> dataParams)
        {
            if (dataParams == null)
                dataParams = new List<KeyValuePair<string, string>>();

            HttpContent httpResponse = await _httpBeatsMusicEngine.PostAsync(methodName, dataParams);
            string dataResponse = await httpResponse.ReadAsStringAsync();

            return ParsedSingleDataResponse<T>(dataResponse);
        }

        public async Task<SingleRootObject<T>> PutData<T>(string methodName,
            List<KeyValuePair<string, string>> dataParams, bool addCredentials = true)
        {
            if (dataParams == null)
                dataParams = new List<KeyValuePair<string, string>>();

            HttpContent httpResponse = await _httpBeatsMusicEngine.PutAsync(methodName, dataParams, addCredentials);
            string dataResponse = await httpResponse.ReadAsStringAsync();

            return ParsedSingleDataResponse<T>(dataResponse);
        }

        public async Task<bool> DeleteData(string methodName, List<KeyValuePair<string, string>> dataParams)
        {
            if (dataParams == null)
                dataParams = new List<KeyValuePair<string, string>>();

            HttpContent httpResponse = await _httpBeatsMusicEngine.DeleteAsync(methodName, dataParams);
            string dataResponse = await httpResponse.ReadAsStringAsync();

            if (dataResponse.ToLower().Contains("ok"))
                return true;
            return false;
        }

        private MultipleRootObject<T> ParsedMultipleDataResponse<T>(string dataResponse)
        {
            var parsedDataResponse = JsonConvert.DeserializeObject<MultipleRootObject<T>>(dataResponse);
            return parsedDataResponse;
        }

        public async Task<MultipleRootObject<T>> GetMultipleParsedResultWithConverter<T>(string methodName,
            List<KeyValuePair<string, string>> methodParams, bool useToken = false)
        {
            string dataResponse = await GetDataResponse(methodName, methodParams, useToken);

            var parsedDataResponse = JsonConvert.DeserializeObject<MultipleRootObject<T>>(dataResponse,
                new BaseDataConverter());

            return parsedDataResponse;
        }

        private SingleRootObject<T> ParsedSingleDataResponse<T>(string dataResponse)
        {
            var parsedDataResponse = JsonConvert.DeserializeObject<SingleRootObject<T>>(dataResponse);
            return parsedDataResponse;
        }

        private async Task<string> GetDataResponse(string methodName, List<KeyValuePair<string, string>> methodParams,
            bool useToken = false)
        {
            if (methodParams == null)
                methodParams = new List<KeyValuePair<string, string>>();

            HttpContent contentResult;
            if (useToken)
                contentResult = await _httpBeatsMusicEngine.GetAsyncWithToken(methodName, methodParams);
            else
                contentResult = await _httpBeatsMusicEngine.GetAsyncNoToken(methodName, methodParams);

            string dataResponse = await contentResult.ReadAsStringAsync();
            return dataResponse;
        }


        public async Task<bool> PostData(string methodName, List<KeyValuePair<string, string>> dataParams)
        {
            if (dataParams == null)
                dataParams = new List<KeyValuePair<string, string>>();

            HttpContent httpResponse = await _httpBeatsMusicEngine.PostAsync(methodName, dataParams);
            string dataResponse = await httpResponse.ReadAsStringAsync();

            if (dataResponse.ToLower().Contains("ok"))
                return true;
            return false;
        }
    }
}