using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodingChick.BeatsMusicAPI.Core
{
    public interface IHttpClientAccessor
    {
        Task<HttpContent> GetAsync(string address);
        Task<HttpContent> GetAsync(string address, IDictionary<string, IEnumerable<string>> headers);

        Task<HttpContent> PostAsync(string address, HttpContent content,
                                    string charSet = "",
                                    string mediaType = "");

        Task<HttpContent> GetWithHeaderAsync(string address, IDictionary<string, IEnumerable<string>> headers);
        Task<HttpContent> GetHeaderAsync(string address, IDictionary<string, IEnumerable<string>> headers);
    }


  
}