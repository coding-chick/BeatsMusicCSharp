using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CodingChick.BeatsMusicAPI.Core.Base
{
    public class HttpClientAccessor : IHttpClientAccessor
    {
        private readonly HttpClient _httpClient;

        public HttpClientAccessor()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.AllowAutoRedirect = false;
            _httpClient = new HttpClient(httpClientHandler);
        }

        public async Task<HttpContent> GetAsync(string address)
        {
            return await GetAsync(address, new Dictionary<string, IEnumerable<string>>());
        }

        public async Task<HttpContent> GetAsync(string address, IDictionary<string, IEnumerable<string>> headers)
        {
            foreach (var header in headers)
            {
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            var response = await _httpClient.GetAsync(address);
            return response.Content;
        }


        public async Task<HttpContent> DeleteAsync(string address)
        {
            return await DeleteAsync(address, new Dictionary<string, IEnumerable<string>>());
        }

        public async Task<HttpContent> DeleteAsync(string address, IDictionary<string, IEnumerable<string>> headers)
        {
            foreach (var header in headers)
            {
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            var response = await _httpClient.DeleteAsync(address);
            return response.Content;
        }

        public async Task<HttpResponseHeaders> GetHeaderAsync(string finalAddress)
        {
            //Would rather make this a true Head call, but the method isn't allowed so I am faking it
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(finalAddress, UriKind.Absolute));
            var response = await _httpClient.SendAsync(request);

            return response.Headers;
        }

        public async Task<HttpContent> PutAsync(string address, HttpContent content, string charSet = "",
            string mediaType = "")
        {
            AddHeadersToContent(content, charSet, mediaType);

            var response = await _httpClient.PutAsync(address, content);
            return response.Content;
        }


        public async Task<HttpContent> PostAsync(string address, HttpContent content,
            string charSet = "",
            string mediaType = "")
        {
            AddHeadersToContent(content, charSet, mediaType);

            try
            {
                var client = new HttpClient();
                var response = await client.PostAsync(address, content);
                return response.Content;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static void AddHeadersToContent(HttpContent content, string charSet, string mediaType)
        {
            if (charSet != string.Empty && mediaType != string.Empty)
            {
                content.Headers.ContentType.CharSet = charSet;
                content.Headers.ContentType.MediaType = mediaType;
            }
        }
    }
}