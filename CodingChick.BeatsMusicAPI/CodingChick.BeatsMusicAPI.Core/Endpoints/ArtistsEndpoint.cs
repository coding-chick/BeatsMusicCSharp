using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Albums;
using CodingChick.BeatsMusicAPI.Core.Data.Artists;
using CodingChick.BeatsMusicAPI.Core.Data.Playlists;
using CodingChick.BeatsMusicAPI.Core.Data.Tracks;
using CodingChick.BeatsMusicAPI.Core.Endpoints.DataFilters;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class ArtistsEndpoint : BaseEndpoint
    {
        internal ArtistsEndpoint(BeatsMusicManager beatsMusicManager)
            : base(beatsMusicManager)
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

            return await BeatsMusicManager.GetMultipleParsedResult<ArtistData>("artists", methodParams);
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
            var methodParams = new List<KeyValuePair<string, string>>();
            methodParams = AddOffsetAndLimitParams(methodParams, offset, limit);
            methodParams = AddOrderByParam<ArtistOrderBy>(orderBy, methodParams);            
            AddStreamabilityFilterParams(filters, methodParams);

            return await BeatsMusicManager.GetMultipleParsedResult<ArtistData>("artists", methodParams);
        }


        /// <summary>
        /// You can retrieve a single artist from the collection of artists available in Beats Music.
        /// </summary>
        /// <param name="artistId">The unique ID of the artist.</param>
        /// <returns></returns>
        public async Task<SingleRootObject<ArtistData>> GetSingleArtist(string artistId)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(artistId), "artistId field is null");

            return await BeatsMusicManager.GetSingleParsedResult<ArtistData>(string.Format("artists/{0}", artistId), null);
        }

        /// <summary>
        /// You can get a list of albums for an artist.
        /// </summary>
        /// <param name="artistId">The unique ID for the artist.</param>
        /// <param name="offset">A zero-based integer offset into the results. Default 0.</param>
        /// <param name="limit">Specifies the maximum number of records to retrieve. The number of results returned will be less than or equal to this value. No results are returned if it is set to zero or less. The maximum permitted value is 200. If a value higher than 200 is specified, no more 200 results will be returned. Default 20.</param>
        /// <param name="refType">An array of referenced objects to include in the response. Only the requested ref collections will be returned. Default is to return all refs.</param>
        /// <param name="orderBy">Indicates how the results set should be sorted. Default is popularity desc</param>
        /// <returns></returns>
        public async Task<MultipleRootObject<AlbumData>> GetAlbumsByArtist(string artistId, int offset = 0,
                                                                           int limit = 20,
                                                                           AlbumRefType refType = AlbumRefType.AllRefs,
                                                                           AlbumsOrderBy orderBy =
                                                                               AlbumsOrderBy.PopularityDesc)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(artistId), "artistId field is null");

            this.ValidateIdOffsetLimit(offset, limit);
            var methodParams = new List<KeyValuePair<string, string>>();
            methodParams = AddOffsetAndLimitParams(methodParams, offset, limit);
            methodParams = AddFlagedEnumValues<AlbumRefType>(refType, methodParams);
            methodParams = AddOrderByParam<AlbumsOrderBy>(orderBy, methodParams);

            return
                await
                BeatsMusicManager.GetMultipleParsedResult<AlbumData>(string.Format("artists/{0}/albums", artistId),
                                                                 methodParams);
        }

        /// <summary>
        /// You can get a list of albums, for the specified artist, that appear in Essentials in the Highlights section of your Beats Music app.
        /// </summary>
        /// <param name="artistId">The unique ID for the artist.</param>
        /// <returns></returns>
        public async Task<MultipleRootObject<AlbumData>> GetEssentialAlbumsByArtist(string artistId)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(artistId), "artistId field is null");

            return
             await
             BeatsMusicManager.GetMultipleParsedResult<AlbumData>(string.Format("artists/{0}/essential_albums", artistId),
                                                              null);
        }

        /// <summary>
        /// Gets a list of tracks on which the given artist was credited (uses the same options and response as the tracks collection)
        /// </summary>
        /// <param name="artistId">The unique ID for the artist.</param>
        /// <param name="offset">Integer offset into results, 0 based. Defaults to 0.</param>
        /// <param name="limit">Specifies the maximum number of records to retrieve. You are guaranteed to get at most this number of results, but you may get fewer. If set to zero or less, returns nothing. Maximum value is 200 (i.e., if greater than 200, returns only 200). If not supplied, defaults to 20.</param>
        /// <param name="tracksRef">An array of referenced objects to include in the response. Only the requested ref collections will be returned. Default is to return all refs. </param>
        /// <param name="orderByData">Indicates how the results set should be sorted.Default value is TrackPosition</param>
        /// <param name="genereIdsFilters">Array of filter operations to apply. Valid filters: genres</param>
        /// <param name="streamabilityFilters">Array of streamability filter operations to apply. Streamability refers to whether an entity, such as a track or album, can be streamed.</param>
        /// <returns></returns>
        public async Task<MultipleRootObject<TrackData>> GetTracksByArtist(string artistId, int offset = 0,
            int limit = 20, TracksRefType tracksRef = TracksRefType.AllRefs,
            TracksOrderByData orderByData = TracksOrderByData.TrackPosition,
            string[] genereIdsFilters = null, StreamabilityFilters streamabilityFilters = null)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(artistId), "artistId field is null");
            this.ValidateIdOffsetLimit(offset, limit);

            var dataParams = new List<KeyValuePair<string, string>>();
            dataParams = AddOffsetAndLimitParams(dataParams, offset, limit);
            dataParams = this.AddFlagedEnumValues<TracksRefType>(tracksRef, dataParams);

            dataParams = AddOrderByParam<TracksOrderByData>(orderByData, dataParams);

            if (genereIdsFilters != null && genereIdsFilters.Any())
            {
                foreach (string genereId in genereIdsFilters)
                {
                    dataParams.Add(new KeyValuePair<string, string>("filters:", string.Format("genre:{0}", genereId)));
                }
            }

            this.AddStreamabilityFilterParams(streamabilityFilters, dataParams);

            return
                await
                    BeatsMusicManager.GetMultipleParsedResult<TrackData>(string.Format("artists/{0}/tracks", artistId),
                        dataParams);
        }


        /// <summary>
        /// You can get a list of playlists associated with an artist.
        /// </summary>
        /// <param name="artistId">The unique ID for the artist.</param>
        /// <param name="offset">A zero-based integer offset into the results. Default 0.</param>
        /// <param name="limit">Specifies the maximum number of records to retrieve. The number of results returned will be less than or equal to this value. No results are returned if it is set to zero or less. The maximum permitted value is 200. If a value higher than 200 is specified, no more 200 results will be returned. Default 20.</param>
        /// <param name="playlistsOrderBy">Indicates how the results set should be sorted. </param>
        /// <returns></returns>
        public async Task<MultipleRootObject<PlaylistData>> GetPlaylistsByArtist(string artistId, int offset = 0,
            int limit = 20, PlaylistsOrderBy playlistsOrderBy = PlaylistsOrderBy.NameAscending)
        {
            var dataParams = new List<KeyValuePair<string, string>>();
            dataParams = AddOffsetAndLimitParams(dataParams, offset, limit);
            dataParams = AddOrderByParam<PlaylistsOrderBy>(playlistsOrderBy, dataParams);

            return
                await
                    BeatsMusicManager.GetMultipleParsedResult<PlaylistData>(
                        string.Format("artists/{0}/playlists", artistId), dataParams);
        }


    }
}
