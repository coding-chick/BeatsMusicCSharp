using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Albums;
using CodingChick.BeatsMusicAPI.Core.Data.Artists;
using CodingChick.BeatsMusicAPI.Core.Data.Reviews;
using CodingChick.BeatsMusicAPI.Core.Data.Tracks;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class AlbumsEndpoint : BaseEndpoint
    {

        internal AlbumsEndpoint(BeatsMusicManager beatsMusicManager)
            : base(beatsMusicManager)
        {
        }

        /// <summary>
        /// You can retrieve part or all of the collection of albums in Beats, including those not available for streaming.
        /// </summary>
        /// <param name="albumsOrderBy">Indicates how the results set should be sorted.</param>
        /// <param name="limit">Specifies the maximum number of records to retrieve. The number of results returned will be less than or equal to this value. No results are returned if it is set to zero or less. The maximum permitted value is 200. If a value higher than 200 is specified, no more 200 results will be returned. Default 20</param>
        /// <param name="offset">A zero-based integer offset into the results. Default 0.</param>
        /// <returns>A Task containing a list of AlbumData</returns>
        public async Task<MultipleRootObject<AlbumData>> GetAlbumCollection(AlbumsOrderBy albumsOrderBy, int offset = 0, int limit = 20)
        {
            Contract.Requires<ArgumentOutOfRangeException>(limit >= 0, "limit can only be a positive number");
            Contract.Requires<ArgumentOutOfRangeException>(offset >= 0, "offset can only be a positive number");

            var methodParams = new List<KeyValuePair<string, string>>();
            methodParams = AddOffsetAndLimitParams(methodParams, offset, limit);
            methodParams = AddOrderByParam<AlbumsOrderBy>(albumsOrderBy, methodParams);

            
            return await BeatsMusicManager.GetMultipleParsedResult<AlbumData>("albums", methodParams);
        }

        /// <summary>
        /// You can retrieve a single album from the collection of available albums.
        /// </summary>
        /// <param name="albumId">The unique ID of the album.</param>
        /// <returns></returns>
        //TODO: add the other two parameters (fields and refs)
        public async Task<SingleRootObject<AlbumData>> GetAlbumById(string albumId)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(albumId), "albumId is null or empty");

            return
                await
                    BeatsMusicManager.GetSingleParsedResult<AlbumData>(GetSingleFirstLevelMethod(albumId), null);
        }

        public async Task<MultipleRootObject<ArtistData>> GetArtistsInAlbumId(string albumId)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(albumId), "albumId is null or empty");

            return
                await
                    BeatsMusicManager.GetMultipleParsedResult<ArtistData>(GetSingleFirstLevelMethod(albumId) + "/artists", null);
        }

        public async Task<MultipleRootObject<TrackData>> GetTracksInAlbum(string albumId)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(albumId), "albumId is null or empty");

            return
               await
                   BeatsMusicManager.GetMultipleParsedResult<TrackData>(GetSingleFirstLevelMethod(albumId) + "/tracks", null);
        }

        public async Task<SingleRootObject<ReviewData>> GetReviewDataForAlbum(string albumId)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(albumId), "albumId is null or empty");

            return
             await
                 BeatsMusicManager.GetSingleParsedResult<ReviewData>(GetSingleFirstLevelMethod(albumId) + "/review", null);
        }

        public async Task<MultipleRootObject<AlbumData>> GetAllCompanionAlbums(string albumId)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(albumId), "albumId is null or empty");

            return
                await
                    BeatsMusicManager.GetMultipleParsedResult<AlbumData>(GetSingleFirstLevelMethod(albumId) + "/companion_albums", null);
        }

        private string GetSingleFirstLevelMethod(string albumId)
        {
            return "albums/" + albumId;
        }
    }




}
