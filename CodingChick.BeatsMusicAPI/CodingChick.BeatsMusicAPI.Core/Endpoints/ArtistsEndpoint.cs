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
using CodingChick.BeatsMusicAPI.Core.Endpoints.DataFilters;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class ArtistsEndpoint : BaseEndpoint
    {
        internal ArtistsEndpoint(BeatsHttpData beatsHttpData) : base(beatsHttpData)
        {
        }

        /// <summary>
        /// You can retrieve the entire collection of artists available in Beats Music.
        /// </summary>
        /// <param name="artistsIds">Array of unique artist IDs.</param>
        /// <returns></returns>
        public async Task<MultipleRootObject<ArtistData>> GetMultipleArtistsByIds(string[] artistsIds)
        {
            var methodParams = new List<KeyValuePair<string, string>>();
            methodParams.AddRange(from artistId in artistsIds select new KeyValuePair<string, string>("ids", artistId));

            return await BeatsHttpData.GetMultipleParsedResult<ArtistData>("artists", methodParams);
        }

        /// <summary>
        /// You can retrieve the entire collection of artists available in Beats Music.
        /// </summary>
        /// <param name="offset">A zero-based integer offset into the results. Default 0.</param>
        /// <param name="limit">Specifies the maximum number of records to retrieve. The number of results returned will be less than or equal to this value. No results are returned if it is set to zero or less. The maximum permitted value is 200. If a value higher than 200 is specified, no more 200 results will be returned. Default 20.</param>
        /// <param name="orderBy">Indicates how the results set should be sorted. Default is popularity desc</param>
        /// <param name="filters">Streamability refers to whether an entity, such as a track or album, can be streamed. Each entity can be in one of three states: Streamable, FutureStreamable, NeverStreamable</param>
        /// <returns></returns>
        public async Task<MultipleRootObject<ArtistData>> GetAllArtists(int offset = 0, int limit = 20,
            ArtistOrderBy orderBy = ArtistOrderBy.PopularityDescending, StreamabilityFilters filters = null)
        {
            ValidateIdOffsetLimit(offset, limit);
            var methodParams = AddOffsetAndLimitParams(offset, limit);
            AddStreamabilityFilterParams(filters, methodParams);
            
            methodParams.Add(new KeyValuePair<string, string>("order_by", 
                ParamValueAttributeHelper.GetParamValueOfEnumAttribute<ArtistOrderBy>(orderBy)));

            return await BeatsHttpData.GetMultipleParsedResult<ArtistData>("artists", methodParams);
        }


        /// <summary>
        /// You can retrieve a single artist from the collection of artists available in Beats Music.
        /// </summary>
        /// <param name="artistId">The unique ID of the artist.</param>
        /// <returns></returns>
        public async Task<SingleRootObject<ArtistData>> GetSingleArtist(string artistId)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(artistId), "artistId field is null");

            return await BeatsHttpData.GetSingleParsedResult<ArtistData>(string.Format("artists/{0}", artistId), null);
        }

        public async Task<MultipleRootObject<AlbumData>> GetAlbumsByArtist(string artistId, int offset = 0,
                                                                           int limit = 20,
                                                                           AlbumRefType refType = AlbumRefType.AllRefs,
                                                                           AlbumsOrderBy orderBy =
                                                                               AlbumsOrderBy.PopularityDesc)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(artistId), "artistId field is null");

            this.ValidateIdOffsetLimit(offset, limit);
            var methodParams = this.AddOffsetAndLimitParams(offset, limit);
            AddFlagedEnumValues<AlbumRefType>(refType, methodParams);
            methodParams.Add(new KeyValuePair<string, string>("order_by", ParamValueAttributeHelper.GetParamValueOfEnumAttribute<AlbumsOrderBy>(orderBy)));

            return
                await
                BeatsHttpData.GetMultipleParsedResult<AlbumData>(string.Format("artists/{0}/albums", artistId),
                                                                 methodParams);
        }                                                                                                                                                                                                                                      






    }
}
