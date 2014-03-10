using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Base
{
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

        public async Task<HttpContent> GetAsyncNoToken(string method, Dictionary<string, string> queryParams)
        {
            queryParams.Add("client_id", _authorization.ClientId);

            string finalAddress = HttpUtilityHelper.CreateFullAddess(MethodsApiAddress, method, queryParams);
            var result = await _clientAccessor.GetAsync(finalAddress);

            return result;
        }

        public string UriAddressToNavigateForPermissions(ResponseType responseType)
        {
            _authorization.ResponseType = responseType;
            var authParams = _authorization.CreateAuthorizatioUriParams(responseType);
            return BaseApiAddress + _authorization.AuthorizatioUri + HttpUtilityHelper.ToQueryString(authParams);
        }

        public async Task<HttpContent> PostAsync(string method, Dictionary<string, string> dataParams)
        {
            if (_authorization.ReadWriteAccessToken == null)
            {
                var succeed = await RenewReadWriteAccessToken();
            }

            dataParams.Add("access_token", _authorization.ReadWriteAccessToken);
            var fullAddress = HttpUtilityHelper.CreateFullAddess(MethodsApiAddress, method, dataParams);
            var httpContent = new StringContent(string.Empty);

            var result = await _clientAccessor.PostAsync(fullAddress, httpContent);
            return result;
        }

        private async Task<bool> RenewReadWriteAccessToken()
        {
            //TODO: edit URL to be refactored
            const string requestTokenUri = @"https://partner.api.beatsmusic.com/oauth2/token";
            var requestParams =
             _authorization.GetAuthorizationTokenParams();

            HttpContent response =
                await _clientAccessor.PostAsync(requestTokenUri, new FormUrlEncodedContent(requestParams));

             return await _authorization.ParseAccessToken(response);
        }
    }
}
