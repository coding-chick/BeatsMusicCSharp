using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Base
{
    //TODO: add assersions, lots of assersions
    public class HttpBeatsMusicEngine : IHttpBeatsMusicEngine
    {
        private readonly IHttpClientAccessor _clientAccessor;
        private readonly Authorization _authorization;


        public string BaseApiAddress
        {
            get
            {
                return "https://partner.api.beatsmusic.com/";
            }
        }

        public string MethodsApiAddress
        {
            get { return BaseApiAddress + "v1/api/"; }
        }

        public HttpBeatsMusicEngine(HttpClientAccessor clientAccessor, Authorization authorization)
        {
            _clientAccessor = clientAccessor;
            _authorization = authorization;
        }

        public async Task<HttpContent> GetAsyncNoToken(string method, List<KeyValuePair<string, string>> queryParams)
        {
            queryParams.Add(new KeyValuePair<string, string>("client_id", _authorization.ClientId));

            var result = await CallGetAsync(method, queryParams);

            return result;
        }

        public async Task<HttpContent> GetAsyncWithToken(string method, List<KeyValuePair<string, string>> queryParams)
        {
            await AddAccessTokenToCall(queryParams);

            var result = await CallGetAsync(method, queryParams);

            return result;
        }

        public async Task<HttpContent> PostAsync(string method, List<KeyValuePair<string, string>> dataParams)
        {
            await AddAccessTokenToCall(dataParams);
            var fullAddress = HttpUtilityHelper.CreateFullAddess(MethodsApiAddress, method, dataParams);
            var httpContent = new StringContent(string.Empty);

            var result = await _clientAccessor.PostAsync(fullAddress, httpContent);
            return result;
        }

        public async Task<HttpContent> PutAsync(string method, List<KeyValuePair<string, string>> dataParams)
        {
            await AddAccessTokenToCall(dataParams);
            var fullAddress = HttpUtilityHelper.CreateFullAddess(MethodsApiAddress, method, dataParams);
            var httpContent = new StringContent(string.Empty);

            var result = await _clientAccessor.PutAsync(fullAddress, httpContent);
            return result;
        }

        public async Task<HttpContent> DeleteAsync(string method, List<KeyValuePair<string, string>> dataParams)
        {
            await AddAccessTokenToCall(dataParams);
            var fullAddress = HttpUtilityHelper.CreateFullAddess(MethodsApiAddress, method, dataParams);

            var result = await _clientAccessor.DeleteAsync(fullAddress);
            return result;
        }


        public string UriAddressToNavigateForPermissions(ResponseType responseType)
        {
            _authorization.ResponseType = responseType;
            List<KeyValuePair<string, string>> authParams = _authorization.CreateAuthorizatioUriParams(responseType);
            return BaseApiAddress + _authorization.AuthorizatioUri + HttpUtilityHelper.ToQueryString(authParams);
        }

        private async Task<HttpContent> CallGetAsync(string method, List<KeyValuePair<string, string>> queryParams)
        {
            string finalAddress = HttpUtilityHelper.CreateFullAddess(MethodsApiAddress, method, queryParams);
            var result = await _clientAccessor.GetAsync(finalAddress);
            return result;
        }

        private async Task AddAccessTokenToCall(List<KeyValuePair<string, string>> dataParams)
        {
            if (_authorization.ReadWriteAccessToken == null)
            {
                var succeed = await RenewReadWriteAccessToken();
            }

            dataParams.Add(new KeyValuePair<string, string>("access_token", _authorization.ReadWriteAccessToken));
        }

        private async Task<bool> RenewReadWriteAccessToken()
        {
            string requestTokenUri = BaseApiAddress + _authorization.TokenUri;
            var requestParams =
             _authorization.GetAuthorizationTokenParams();

            HttpContent response =
                await _clientAccessor.PostAsync(requestTokenUri, new FormUrlEncodedContent(requestParams));

             return await _authorization.ParseAccessToken(response);
        }
    }
}
