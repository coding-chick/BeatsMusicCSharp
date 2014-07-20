using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Albums;
using CodingChick.BeatsMusicAPI.Core.Data.Content;
using CodingChick.BeatsMusicAPI.Core.Data.Genres;
using CodingChick.BeatsMusicAPI.Core.Data.Playlists;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class GenreEndpoint : BaseEndpoint
    {
        internal GenreEndpoint(BeatsMusicManager beatsMusicManager) : base(beatsMusicManager)
        {
        }

        /// <summary>
        /// Get an existing Beats genres.
        /// </summary>
        /// <param name="genreId">Unique ID of the genre.</param>
        /// <returns>SingleRootObject with the relevant GenreData</returns>
        public async Task<SingleRootObject<GenreData>> GetGenreById(string genreId)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(genreId), "genreId field is null or empty");

            return await BeatsMusicManager.GetSingleParsedResult<GenreData>(string.Format("genres/{0}", genreId), null, false);
        }

        /// <summary>
        /// Get all genres
        /// </summary>
        /// <param name="limit">Specifies the maximum number of records to retrieve. You are guaranteed to get at most this number of results, but you may get fewer. If set to zero or less, returns nothing. Maximum value is 200 (i.e., if greater than 200, returns only 200). Defaults to 20.</param>
        /// <param name="offset">Integer offset into results, 0 based. Defaults to 0.</param>
        /// <returns>MultipleRootObject with the relevant GenreData</returns>
        public async Task<MultipleRootObject<GenreData>> GetAllGenres(int limit = 20, int offset = 0)
        {
            var methodParams = ValidateAndCreateLimitOffsetParams(limit, offset);

            return await BeatsMusicManager.GetMultipleParsedResult<GenreData>("genres", methodParams);
        }

        private List<KeyValuePair<string, string>> ValidateAndCreateLimitOffsetParams(int limit, int offset)
        {
            base.ValidateIdOffsetLimit(offset, limit);
            var methodParams = base.AddOffsetAndLimitParams(new List<KeyValuePair<string, string>>(), offset, limit);
            return methodParams;
        }

        /// <summary>
        /// You can get a list of editor picks of tracks and albums within the Genres in Find It in the Beats Music app.
        /// </summary>
        /// <param name="genreId">The Id of the requested Genre</param>
        /// <param name="limit">Specifies the maximum number of records to retrieve. The number of results returned will be less than or equal to this value. No results are returned if it is set to zero or less. The maximum permitted value is 200. If a value higher than 200 is specified, no more 200 results will be returned. Default 20.</param>
        /// <param name="offset">A zero-based integer offset into the results. Default 0.</param>
        /// <returns>A collection of AlbumData and PlaylistData</returns>
        public async Task<MultipleRootObject<BaseConvertedData>> GetEditorPicksInGenre(string genreId, int limit = 20, int offset = 0)
        {
            return await RunMethodOnGenre("editors_picks", genreId, limit, offset);
        }

        private async Task<MultipleRootObject<BaseConvertedData>> RunMethodOnGenre(string methodName, string genreId, int limit, int offset)
        {
            var methodParams = ValidateAndCreateLimitOffsetParams(limit, offset);
            return
                await
                    BeatsMusicManager.GetMultipleParsedResultWithConverter<BaseConvertedData>(
                        string.Format("genres/{0}/{1}", genreId, methodName), methodParams);
        }

        /// <summary>
        /// Gets a list of featured, tracks and albums.
        /// </summary>
        /// <param name="genreId">The Id of the requested Genre</param>
        /// <param name="limit">Specifies the maximum number of records to retrieve. The number of results returned will be less than or equal to this value. No results are returned if it is set to zero or less. The maximum permitted value is 200. If a value higher than 200 is specified, no more 200 results will be returned. Default 20.</param>
        /// <param name="offset">A zero-based integer offset into the results. Default 0.</param>
        /// <returns>A collection of AlbumData and PlaylistData</returns>
        public async Task<MultipleRootObject<BaseConvertedData>> GetFeaturedInGenre(string genreId, int limit = 20,
            int offset = 0)
        {
            return await RunMethodOnGenre("featured", genreId, limit, offset);
        }


        /// <summary>
        /// Gets a list of new releases, tracks and albums.
        /// </summary>
        /// <param name="genreId">The Id of the requested Genre</param>
        /// <param name="limit">Specifies the maximum number of records to retrieve. The number of results returned will be less than or equal to this value. No results are returned if it is set to zero or less. The maximum permitted value is 200. If a value higher than 200 is specified, no more 200 results will be returned. Default 20.</param>
        /// <param name="offset">A zero-based integer offset into the results. Default 0.</param>
        /// <returns>A collection of AlbumData and PlaylistData</returns>
        public async Task<MultipleRootObject<BaseConvertedData>> GetNewReleasesInGenre(string genreId, int limit = 20,
            int offset = 0)
        {
            return await RunMethodOnGenre("new_releases", genreId, limit, offset);
        }

        /// <summary>
        /// Get a list of playlists associated to the genre. 
        /// </summary>
        /// <param name="genreId">Unique ID for Genre</param>
        /// <param name="limit">Specifies the maximum number of records to retrieve. The number of results returned will be less than or equal to this value. No results are returned if it is set to zero or less. The maximum permitted value is 200. If a value higher than 200 is specified, no more 200 results will be returned. Default 20.</param>
        /// <param name="offset">A zero-based integer offset into the results. Default 0.</param>
        /// <param name="orderBy">A zero-based integer offset into the results. Default 0.</param>
        /// <returns></returns>
        public async Task<MultipleRootObject<PlaylistData>> GetAllPlaylistsInGenre(string genreId, int limit = 20,
            int offset = 0, PlaylistsOrderBy orderBy = PlaylistsOrderBy.NameAscending)
        {
            var methodParams = ValidateAndCreateLimitOffsetParams(limit, offset);
            methodParams = base.AddOrderByParam<PlaylistsOrderBy>(orderBy, methodParams);

            return
                await
                    BeatsMusicManager.GetMultipleParsedResult<PlaylistData>(string.Format("genres/{0}/playlists", genreId),
                        methodParams);
        }

        //TODO: add bios method

    }



   

}
