using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<MultipleRootObject<T>> GetMultipleParsedResult<T>(string methodName,
            List<KeyValuePair<string, string>> methodParams, bool useToken = false)
        {
            string dataResponse = await GetDataResponse(methodName, methodParams, useToken);
            MultipleRootObject<T> parsedDataResponse = _jsonBeatsMusicEngine.ParseMultipleDataResponse<T>(dataResponse);
            return parsedDataResponse;
        }

        public async Task<SingleRootObject<T>> GetSingleParsedResult<T>(string methodName,
            List<KeyValuePair<string, string>> methodParams, bool useToken = false)
        {
            string dataResponse = await GetDataResponse(methodName, methodParams, useToken);
            SingleRootObject<T> parsedDataResponse = _jsonBeatsMusicEngine.ParseSingleDataResponse<T>(dataResponse);
            return parsedDataResponse;
        }

        public async Task<SingleRootObject<T>> PostData<T>(string methodName,
            List<KeyValuePair<string, string>> dataParams)
        {
            if (dataParams == null)
                dataParams = new List<KeyValuePair<string, string>>();

            HttpContent httpResponse = await _httpBeatsMusicEngine.PostAsync(methodName, dataParams);
            string dataResponse = await httpResponse.ReadAsStringAsync();

            return _jsonBeatsMusicEngine.ParseSingleDataResponse<T>(dataResponse);
        }

        public async Task<SingleRootObject<T>> PutData<T>(string methodName,
            List<KeyValuePair<string, string>> dataParams, bool addCredentials = true)
        {
            if (dataParams == null)
                dataParams = new List<KeyValuePair<string, string>>();

            HttpContent httpResponse = await _httpBeatsMusicEngine.PutAsync(methodName, dataParams, addCredentials);
            string dataResponse = await httpResponse.ReadAsStringAsync();

            return _jsonBeatsMusicEngine.ParseSingleDataResponse<T>(dataResponse);
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

        public async Task<MultipleRootObject<T>> GetMultipleParsedResultWithConverter<T>(string methodName,
            List<KeyValuePair<string, string>> methodParams, bool useToken = false)
        {
            string dataResponse = await GetDataResponse(methodName, methodParams, useToken);
            MultipleRootObject<T> parsedDataResponse =
                _jsonBeatsMusicEngine.ParseMultipleDataResponseWithConverter<T>(dataResponse);
            return parsedDataResponse;
        }

        public async Task<Uri> GetDataUri(string methodName, List<KeyValuePair<string, string>> methodParams,
            bool useToken)
        {
            HttpResponseHeaders dataResponse =
                await _httpBeatsMusicEngine.HeadAsyncWithNoToken(methodName, methodParams);
            return dataResponse.Location;
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

        public async Task<bool> PutData(string methodName, List<KeyValuePair<string, string>> dataParams,
            bool addCredentials = true)
        {
            if (dataParams == null)
                dataParams = new List<KeyValuePair<string, string>>();

            HttpContent httpResponse = await _httpBeatsMusicEngine.PutAsync(methodName, dataParams, addCredentials);
            string dataResponse = await httpResponse.ReadAsStringAsync();

            if (dataResponse.ToLower().Contains("ok"))
                return true;
            return false;
        }
    }
}