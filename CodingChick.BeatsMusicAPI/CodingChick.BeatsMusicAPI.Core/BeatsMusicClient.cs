using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Endpoints;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;

[assembly: InternalsVisibleTo("CodingChick.BeatsMusicAPI.Tests")]
namespace CodingChick.BeatsMusicAPI.Core
{
    public class BeatsMusicClient
    {
        private Authorization _authorization;
        private IHttpBeatsMusicEngine _httpBeatsMusicEngine;
        private BeatsHttpData _beatsHttpData;

        /// <summary>
        /// Initializes a new instance of <see cref="BeatsMusicClient"/> for read-only operations. 
        /// </summary>
        /// <param name="clientId">Beats Music API client ID.</param>
        /// <param name="redirectUri">Beats Music Redirect Uri.</param>
        public BeatsMusicClient(string clientId, string redirectUri)
        {
            _authorization = new Authorization(redirectUri, clientId);

            _httpBeatsMusicEngine = new HttpBeatsMusicEngine(new HttpClientAccessor(), _authorization);
            _beatsHttpData = new BeatsHttpData(_httpBeatsMusicEngine);
            _search = new Lazy<SearchEndpoint>(() => new SearchEndpoint(_beatsHttpData));
            _playlists = new Lazy<PlaylistsEndpoint>(() => new PlaylistsEndpoint(_beatsHttpData));
            _albums = new Lazy<AlbumsEndpoint>(() => new AlbumsEndpoint(_beatsHttpData));
            _artists = new Lazy<ArtistsEndpoint>(() => new ArtistsEndpoint(_beatsHttpData));
            _highlights = new Lazy<HighlightsEndpoint>(() => new HighlightsEndpoint(_beatsHttpData));
            _follow = new Lazy<FollowEndpoint>(() => new FollowEndpoint(_beatsHttpData));
            _genre = new Lazy<GenreEndpoint>(() => new GenreEndpoint(_beatsHttpData));
            _audio = new Lazy<AudioEndpoint>(() => new AudioEndpoint(_beatsHttpData));


        }

        /// <summary>
        /// Initializes a new instance of <see cref="BeatsMusicClient"/> for read-write and user specific operations. 
        /// </summary>
        /// <param name="clientId">Beats Music API client ID.</param>
        /// <param name="redirectUri">Beats Music API Redirect Uri.</param>
        /// <param name="clientSecret">Beats Music API client secret</param>
        public BeatsMusicClient(string clientId, string redirectUri, string clientSecret)
            : this(clientId, redirectUri)
        {
            _authorization.ClientSecret = clientSecret;
        }

        private Lazy<SearchEndpoint> _search;

        private Lazy<PlaylistsEndpoint> _playlists;
        private Lazy<AlbumsEndpoint> _albums;
        private Lazy<ArtistsEndpoint> _artists;
        private Lazy<HighlightsEndpoint> _highlights;
        private Lazy<FollowEndpoint> _follow;
        private Lazy<GenreEndpoint> _genre;
        private Lazy<AudioEndpoint> _audio;

        public AudioEndpoint Audio
        {
            get { return _audio.Value; }
        }

        public GenreEndpoint Genre
        {
            get { return _genre.Value; }
        }

        public FollowEndpoint Follow
        {
            get { return _follow.Value; }
        }

        public HighlightsEndpoint Highlights
        {
            get { return _highlights.Value; }
        }

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



        public string ServerCode
        {
            get { return _authorization.Code; }
            set { _authorization.Code = value; }
        }

        public ArtistsEndpoint Artists
        {
            get { return _artists.Value; }
        }

        public string UriAddressToNavigateForPermissions()
        {
            if (_authorization.ClientSecret == null)
                return _httpBeatsMusicEngine.UriAddressToNavigateForPermissions(ResponseType.Token);
            else
            {
                return _httpBeatsMusicEngine.UriAddressToNavigateForPermissions(ResponseType.Code);
            }
        }

        public void SetClientAccessTokenFromRedirectUri(string accessToken, int expiresAt)
        {
            _authorization.ReadOnlyAccessToken = accessToken;
            _authorization.SetExpiresAt(expiresAt);
        }

        //Internal properties exposed for testing purposes only.
        #region Internal
        internal string ReadWriteAccessToken
        {
            get { return _authorization.ReadWriteAccessToken; }
            set { _authorization.ReadWriteAccessToken = value; }
        }

        internal void SetExpiresAt(int seconds)
        {
            _authorization.SetExpiresAt(seconds);
        }
        #endregion
    }
}
