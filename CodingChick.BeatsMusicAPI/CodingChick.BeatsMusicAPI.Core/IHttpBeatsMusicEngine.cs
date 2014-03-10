using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;

namespace CodingChick.BeatsMusicAPI.Core
{
    public interface IHttpBeatsMusicEngine
    {
        //Task<HttpContent> GetAsyncWithToken(string methodAndParamethers);
        Task<HttpContent> GetAsyncNoToken(string method, Dictionary<string, string> queryParams);
        string UriAddressToNavigateForPermissions(ResponseType responseType);
        Task<HttpContent> PostAsync(string method, Dictionary<string, string> dataParams);
    }
}