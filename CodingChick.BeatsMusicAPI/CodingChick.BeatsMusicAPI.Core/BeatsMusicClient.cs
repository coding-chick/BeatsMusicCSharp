using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Me;
using CodingChick.BeatsMusicAPI.Core.Endpoints;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using CodingChick.BeatsMusicAPI.Core.Helpers;

[assembly: InternalsVisibleTo("CodingChick.BeatsMusicAPI.Tests")]

namespace CodingChick.BeatsMusicAPI.Core
{
    public class BeatsMusicClient
    {
        private readonly Lazy<AlbumsEndpoint> _albums;
        private readonly Lazy<ArtistsEndpoint> _artists;
        private readonly Lazy<AudioEndpoint> _audio;
        private readonly Authorization _authorization;
        private readonly BeatsMusicManager _beatsMusicManager;
        private readonly Lazy<FollowEndpoint> _follow;
        private readonly Lazy<GenreEndpoint> _genre;
        private readonly Lazy<HighlightsEndpoint> _highlights;
        private readonly IHttpBeatsMusicEngine _httpBeatsMusicEngine;
        private readonly Lazy<ImagesEndpoint> _images;
        private readonly Lazy<MeEndpoint> _me;
        private readonly Lazy<PlaylistsEndpoint> _playlists;
        private readonly Lazy<RatingsEndpoint> _ratings;
        private readonly Lazy<SearchEndpoint> _search;
        private readonly Lazy<TracksEndpoint> _tracks;

        /// <summary>
        ///     Initializes a new instance of <see cref="BeatsMusicClient" /> for read-only operations.
        /// </summary>
        /// <param name="clientId">Beats Music API client ID.</param>
        /// <param name="redirectUri">Beats Music Redirect Uri.</param>
        public BeatsMusicClient(string clientId, string redirectUri)
        {
            _authorization = new Authorization(redirectUri, clientId);

            _httpBeatsMusicEngine = new HttpBeatsMusicEngine(new HttpClientAccessor(), _authorization);

            _beatsMusicManager = new BeatsMusicManager(_httpBeatsMusicEngine, new JsonBeatsMusicEngine());
            _search = new Lazy<SearchEndpoint>(() => new SearchEndpoint(_beatsMusicManager));
            _playlists = new Lazy<PlaylistsEndpoint>(() => new PlaylistsEndpoint(_beatsMusicManager));
            _albums = new Lazy<AlbumsEndpoint>(() => new AlbumsEndpoint(_beatsMusicManager));
            _artists = new Lazy<ArtistsEndpoint>(() => new ArtistsEndpoint(_beatsMusicManager));
            _highlights = new Lazy<HighlightsEndpoint>(() => new HighlightsEndpoint(_beatsMusicManager));
            _follow = new Lazy<FollowEndpoint>(() => new FollowEndpoint(_beatsMusicManager));
            _genre = new Lazy<GenreEndpoint>(() => new GenreEndpoint(_beatsMusicManager));
            _audio = new Lazy<AudioEndpoint>(() => new AudioEndpoint(_beatsMusicManager));
            _me = new Lazy<MeEndpoint>(() => new MeEndpoint(_beatsMusicManager));
            _images = new Lazy<ImagesEndpoint>(() => new ImagesEndpoint(_beatsMusicManager));
            _tracks = new Lazy<TracksEndpoint>(() => new TracksEndpoint(_beatsMusicManager));
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="BeatsMusicClient" /> for read-write and user specific operations.
        /// </summary>
        /// <param name="clientId">Beats Music API client ID.</param>
        /// <param name="redirectUri">Beats Music API Redirect Uri.</param>
        /// <param name="clientSecret">Beats Music API client secret</param>
        public BeatsMusicClient(string clientId, string redirectUri, string clientSecret)
            : this(clientId, redirectUri)
        {
            _authorization.ClientSecret = clientSecret;
        }

        public MeEndpoint Me
        {
            get { return _me.Value; }
        }

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

        public RatingsEndpoint Ratings
        {
            get { return _ratings.Value; }
        }

        public ImagesEndpoint Images
        {
            get { return _images.Value; }
        }

        public TracksEndpoint Tracks
        {
            get { return _tracks.Value; }
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

        public string ClientId
        {
            get { return _authorization.ClientId; }
        }

        public string AccessToken
        {
            get
            {
                if (!string.IsNullOrEmpty(_authorization.ReadOnlyAccessToken))
                    return _authorization.ReadOnlyAccessToken;
                return _authorization.ReadWriteAccessToken;
            }
        }

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

        public string UriAddressToNavigateForPermissions()
        {
            if (_authorization.ClientSecret == null)
                return _httpBeatsMusicEngine.UriAddressToNavigateForPermissions(ResponseType.Token);
            return _httpBeatsMusicEngine.UriAddressToNavigateForPermissions(ResponseType.Code);
        }

        public void SetClientAccessTokenFromRedirectUri(string accessToken, int expiresAt)
        {
            _authorization.ReadOnlyAccessToken = accessToken;
            _authorization.SetExpiresAt(expiresAt);
        }

        public async Task<string> GetBeatsMusicPlayerCode(string playableResourceId)
        {
            SingleRootObject<MeData> user = await Me.GetMeInfo();
            var helper = new FileHelper();
            string fileContents = helper.GetResourceTextFile("PlayerCode.html");
            fileContents = fileContents.Replace("myClientId", _authorization.ClientId);
            fileContents = fileContents.Replace("myAccessToken", AccessToken);
            fileContents = fileContents.Replace("myUserId", user.Data.UserContext);
            fileContents = fileContents.Replace("myTrack", playableResourceId);
            return fileContents;
        }

        //Internal properties exposed for testing purposes only.
    }
}