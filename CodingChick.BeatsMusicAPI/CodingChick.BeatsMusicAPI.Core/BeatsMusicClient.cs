using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Endpoints;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;

namespace CodingChick.BeatsMusicAPI.Core
{
    public class BeatsMusicClient
    {
        private readonly string _clientSecret;
        private readonly string _clientId;
        private readonly string _redirectUri;

        private Authorization _authorization;
        private IHttpBeatsMusicEngine _httpBeatsMusicEngine;
        private BeatsHttpData _beatsHttpData;

        public BeatsMusicClient(string clientId, string redirectUri)
        {
            _clientId = clientId;
            _redirectUri = redirectUri;

            _authorization = new Authorization(redirectUri, clientId);

            _httpBeatsMusicEngine = new HttpBeatsMusicEngine(new HttpClientAccessor(), _authorization);
            _beatsHttpData = new BeatsHttpData(_httpBeatsMusicEngine);
            _search = new Lazy<SearchEndpoint>(() => new SearchEndpoint(_beatsHttpData));
            _playlists = new Lazy<PlaylistsEndpoint>(() => new PlaylistsEndpoint(_beatsHttpData));
            _albums = new Lazy<AlbumsEndpoint>(() => new AlbumsEndpoint(_beatsHttpData));
        }

        public BeatsMusicClient(string clientId, string redirectUri, string clientSecret)
            : this(clientId, redirectUri)
        {
            _clientSecret = clientSecret;
            _authorization.ClientSecret = _clientSecret;
        }

        private Lazy<SearchEndpoint> _search;
        private string _readOnlyAccessToken;
        private string _readWriteAccessToken;

        private Lazy<PlaylistsEndpoint> _playlists;
        private string _code;
        private Lazy<AlbumsEndpoint> _albums;

        public AlbumsEndpoint Albums
        {
            get { return _albums.Value; }
        }

        public SearchEndpoint Search
        {
            get { return _search.Value; }
        }

        public PlaylistsEndpoint Playlists
        {
            get { return _playlists.Value; }
        }

        public string ReadOnlyAccessToken
        {
            get { return _readOnlyAccessToken; }
            set
            {
                _readOnlyAccessToken = value;
                _authorization.ReadOnlyAccessToken = _readOnlyAccessToken;
            }
        }

       

        public string ClientSecret
        {
            get { return _clientSecret; }
        }

        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                _authorization.Code = _code;
            }
        }

        public string UriAddressToNavigateForPermissions()
        {
            if (_clientSecret == null)
                return _httpBeatsMusicEngine.UriAddressToNavigateForPermissions(ResponseType.Token);
            else
            {
                return _httpBeatsMusicEngine.UriAddressToNavigateForPermissions(ResponseType.Code);                
            }
        }
    }
}
