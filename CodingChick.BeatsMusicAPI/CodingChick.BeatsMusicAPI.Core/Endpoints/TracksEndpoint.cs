using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Tracks;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class TracksEndpoint : BaseEndpoint
    {
        internal TracksEndpoint(BeatsMusicManager beatsMusicManager)
            : base(beatsMusicManager)
        {
        }

        /// <summary>
        ///     You can retrieve a track.
        /// </summary>
        /// <param name="trackId">The unique ID for the track.</param>
        /// <returns>Returns the track as TrackData</returns>
        public async Task<SingleRootObject<TrackData>> GetTrack(string trackId)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(trackId), "trackId is null or empty");
            return
                await
                    BeatsMusicManager.GetSingleParsedResult<TrackData>(string.Format("tracks/{0}", trackId), null,
                        false);
        }

        /// <summary>
        ///     You can retrieve the entire collection of tracks available in Beats Music.
        /// </summary>
        /// <param name="offset">A zero-based integer offset into the results. Default 0</param>
        /// <param name="limit">
        ///     Specifies the maximum number of records to retrieve. The number of results returned will be less
        ///     than or equal to this value. No results are returned if it is set to zero or less. The maximum permitted value is
        ///     200. If a value higher than 200 is specified, no more 200 results will be returned. Default 20.
        /// </param>
        /// <param name="tracksOrderByData">Indicates how the results set should be sorted.  Values (default is TrackPosition)</param>
        /// <returns></returns>
        public async Task<MultipleRootObject<TrackData>> GetTracks(int offset = 0, int limit = 20,
            TracksOrderByData tracksOrderByData = TracksOrderByData.PopularityDesc)
        {
            ValidateIdOffsetLimit(offset, limit);
            var methodParams = new List<KeyValuePair<string, string>>();
            methodParams = AddOffsetAndLimitParams(methodParams, offset, limit);
            methodParams = AddOrderByParam<TracksOrderByData>(tracksOrderByData, methodParams);

            return await BeatsMusicManager.GetMultipleParsedResult<TrackData>("tracks", methodParams);
        }

        /// <summary>
        ///     You can get the tracks list in My Library.
        /// </summary>
        /// <param name="userId">The unique ID for the user.</param>
        /// <param name="offset">A zero-based integer offset into the results. Default 0.</param>
        /// <param name="limit">
        ///     Specifies the maximum number of records to retrieve. The number of results returned will be less
        ///     than or equal to this value. No results are returned if it is set to zero or less. The maximum permitted value is
        ///     150. If a value higher than 150 is specified, no more 150 results will be returned. Default 40.
        /// </param>
        /// <returns></returns>
        public async Task<MultipleRootObject<TrackData>> GetMyLibraryTracks(string userId, int offset = 0,
            int limit = 40)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(userId), "userId field is null");
            ValidateIdOffsetLimit(offset, limit);
            var methodParams = new List<KeyValuePair<string, string>>();
            methodParams = AddOffsetAndLimitParams(methodParams, offset, limit);

            return
                await
                    BeatsMusicManager.GetMultipleParsedResult<TrackData>(
                        string.Format("users/{0}/mymusic/tracks", userId), methodParams, true);
        }
    }
}