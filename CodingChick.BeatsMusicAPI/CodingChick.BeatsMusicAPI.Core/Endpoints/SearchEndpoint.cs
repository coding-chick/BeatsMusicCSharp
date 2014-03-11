using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Search;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class SearchEndpoint : BaseEndpoint
    {
        internal SearchEndpoint(BeatsHttpData beatsHttpData) : base(beatsHttpData)
        {
        }

        public async Task<MultipleRootObject<SearchData>> SearchByArtist(string artistName)
        {
            return await GetSearchResult(artistName, "artist");
        }

        public async Task<MultipleRootObject<SearchData>> SearchByGenre(string genre)
        {
            return await GetSearchResult(genre, "genre");
        }

        public async Task<MultipleRootObject<SearchData>> SearchByAlbum(string album)
        {
            return await GetSearchResult(album, "album");
        }

        public async Task<MultipleRootObject<SearchData>> SearchByTrack(string track)
        {
            return await GetSearchResult(track, "track");
        }

        public async Task<MultipleRootObject<SearchData>> SearchByPlaylist(string playlist)
        {
            return await GetSearchResult(playlist, "playlist");
        }

        public async Task<MultipleRootObject<SearchData>> SearchByUser(string user)
        {
            return await GetSearchResult(user, "user");
        }

        private async Task<MultipleRootObject<SearchData>> GetSearchResult(string queryParam, string queryType)
        {
            Dictionary<string, string> searchParams = new Dictionary<string, string>()
                {
                    {"q", queryParam},
                    {"type", queryType}
                };

            return await BeatsHttpData.GetMultipleParsedResult<SearchData>("search", searchParams);
        }
    }
}