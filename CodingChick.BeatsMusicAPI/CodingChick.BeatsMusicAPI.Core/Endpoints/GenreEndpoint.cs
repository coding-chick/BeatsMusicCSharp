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

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class GenreEndpoint : BaseEndpoint
    {
        internal GenreEndpoint(BeatsHttpData beatsHttpData) : base(beatsHttpData)
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

            return await BeatsHttpData.GetSingleParsedResult<GenreData>(string.Format("genres/{0}", genreId), null, true);
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

            return await BeatsHttpData.GetMultipleParsedResult<GenreData>("genres", methodParams);
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
        /// <param name="genreId"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<MultipleRootObject<BaseData>> GetEditorPicksInGenre(string genreId, int limit = 20, int offset = 0)
        {
            var methodParams = ValidateAndCreateLimitOffsetParams(limit, offset);
            return await BeatsHttpData.GetMultipleParsedResultWithConverter<BaseData>(string.Format("genres/{0}/editors_picks", genreId), methodParams);
        }
    }



   

}
