using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Search;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class SearchEndpoint : BaseEndpoint
    {
        internal SearchEndpoint(BeatsMusicManager beatsMusicManager) : base(beatsMusicManager)
        {
        }

        public async Task<MultipleRootObject<SearchData>> SearchByArtist(string artistName)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(artistName), "user field is empty");
            return await GetSearchResult(artistName, "artist");
        }

        public async Task<MultipleRootObject<SearchData>> SearchByGenre(string genre)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(genre), "user field is empty");
            return await GetSearchResult(genre, "genre");
        }

        public async Task<MultipleRootObject<SearchData>> SearchByAlbum(string album)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(album), "user field is empty");

            return await GetSearchResult(album, "album");
        }

        public async Task<MultipleRootObject<SearchData>> SearchByTrack(string track)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(track), "user field is empty");

            return await GetSearchResult(track, "track");
        }

        public async Task<MultipleRootObject<SearchData>> SearchByPlaylist(string playlist)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(playlist), "user field is empty");

            return await GetSearchResult(playlist, "playlist");
        }

        public async Task<MultipleRootObject<SearchData>> SearchByUser(string user)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(user), "user field is empty");

            return await GetSearchResult(user, "user");
        }

        private async Task<MultipleRootObject<SearchData>> GetSearchResult(string queryParam, string queryType)
        {

            Dictionary<string, string> searchParams = new Dictionary<string, string>()
                {
                    {"q", queryParam},
                    {"type", queryType}
                };

            return await BeatsMusicManager.GetMultipleParsedResult<SearchData>("search", searchParams.ToList());
        }
    }
}