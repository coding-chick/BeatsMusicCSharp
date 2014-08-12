using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;

namespace CodingChick.BeatsMusicAPI.Core.Base
{
    public interface IHttpBeatsMusicEngine
    {
        //Task<HttpContent> GetAsyncWithToken(string methodAndParamethers);
        Task<HttpContent> GetAsyncNoToken(string method, List<KeyValuePair<string, string>> queryParams);
        string UriAddressToNavigateForPermissions(ResponseType responseType);
        Task<HttpContent> PostAsync(string method, List<KeyValuePair<string, string>> dataParams);
        Task<HttpContent> GetAsyncWithToken(string method, List<KeyValuePair<string, string>> queryParams);

        Task<HttpContent> PutAsync(string method, List<KeyValuePair<string, string>> dataParams,
            bool addCredentials = true);

        Task<HttpContent> DeleteAsync(string method, List<KeyValuePair<string, string>> dataParams);

        Task<HttpResponseHeaders> GetHeaderAsyncWithNoToken(string method,
            List<KeyValuePair<string, string>> queryParams);

        Task<HttpResponseHeaders> GetHeadAsyncWithToken(string method,
            List<KeyValuePair<string, string>> queryParams);
    }
}