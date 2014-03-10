using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using CodingChick.BeatsMusicAPI.Core.Helpers;
using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core
{
    public class Authorization
    {
        private readonly ResponseType _responseType;
        private readonly string _redirectUri;
        private readonly string _clientId;

        public Authorization(string redirectUri, string clientId)
        {
            _redirectUri = redirectUri;
            _clientId = clientId;
        }

        public ResponseType ResponseType { get; set; }

        public string ClientId
        {
            get { return _clientId; }
        }

        public string RedirectUri
        {
            get { return _redirectUri; }
        }

        public string ClientSecret { get; set; }

        public string AuthorizatioUri { get { return "oauth2/authorize?"; } }

        public string ReadOnlyAccessToken { get; set; }

        public string ReadWriteAccessToken { get; set; }

        public string Code { get; set; }


        public Dictionary<string, string> CreateAuthorizatioUriParams(ResponseType responseType)
        {
            var responseTypeString = ParamValueAttributeHelper.GetParamValueOfEnumAttribute<ResponseType>(responseType);

            Dictionary<string, string> authParams = new Dictionary<string, string>()
                {
                    {"response_type", responseTypeString},
                    {"redirect_uri", RedirectUri},
                    {"client_id", ClientId}
                };

            return authParams;
        }


        public KeyValuePair<string, string>[] GetAuthorizationTokenParams()
        {
            //TODO: make sure ClientSecret is not null

            return new[]
                {
                    new KeyValuePair<string, string>("client_secret", ClientSecret),
                    new KeyValuePair<string, string>("client_id", ClientId),
                    new KeyValuePair<string, string>("redirect_uri", RedirectUri),
                    new KeyValuePair<string, string>("code", Code)
                };
        }

        public async Task<bool> ParseAccessToken(HttpContent response)
        {
            var dataResponse = await response.ReadAsStringAsync();

            //TODO: store this to hd? implement logic for expiration
            var parsedDataResponse = JsonConvert.DeserializeObject<AuthorizationRootObject>(dataResponse);

            if (parsedDataResponse.Code.ToLower() == "ok")
            {
                ReadWriteAccessToken = parsedDataResponse.Result.AccessToken;
                return true;
            }
            return false;
        }
    }


    public class AuthorizationResult
    {
        [JsonProperty("return_type")]
        public string ReturnType { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
        [JsonProperty("state")]
        public object State { get; set; }
        [JsonProperty("uri")]
        public object Uri { get; set; }
        [JsonProperty("extended")]
        public object Extended { get; set; }
    }

    [JsonObject("RootObject")]
    public class AuthorizationRootObject
    {
        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }
        [JsonProperty("result")]
        public AuthorizationResult Result { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
