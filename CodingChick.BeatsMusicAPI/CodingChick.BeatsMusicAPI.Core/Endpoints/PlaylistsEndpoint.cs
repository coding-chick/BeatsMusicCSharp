﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Playlists;
using CodingChick.BeatsMusicAPI.Core.Data.Tracks;
using CodingChick.BeatsMusicAPI.Core.Data.Users;
using CodingChick.BeatsMusicAPI.Core.Endpoints.DataFilters;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    //TODO: add the rest of the api
    public class PlaylistsEndpoint : BaseEndpoint
    {

        internal PlaylistsEndpoint(BeatsHttpData beatsHttpData)
            : base(beatsHttpData)
        {
        }

        /// <summary>
        /// You can create a new playlist for the user.
        /// </summary>
        /// <param name="name">The name of the playlist. Min Length: 1 character. Max Length: 100 characters.</param>
        /// <param name="description">The description of the playlist. Min Length: 0 characters. Max Length: 100 characters</param>
        /// <param name="accessPrivilege">Specifies whether the playlist is publicly or privately available. </param>
        /// <returns>Returns the newly created PlaylistData</returns>
        public async Task<SingleRootObject<PlaylistData>> CreatePlaylist(string name, string description, AccessPrivilege accessPrivilege = AccessPrivilege.Public)
        {
            CheckNameAndDescription(name, description);

            Dictionary<string, string> dataParams = new Dictionary<string, string>()
                {
                    {"name", name},
                    {"description", description},
                    {"access", ParamValueAttributeHelper.GetParamValueOfEnumAttribute<AccessPrivilege>(accessPrivilege)}
                };

            return await BeatsHttpData.PostData<PlaylistData>("playlists", dataParams.ToList());
        }

        private static void CheckNameAndDescription(string name, string description)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(name), "name field is null or empty");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(description), "description field null or empty");
            Contract.Requires<ArgumentOutOfRangeException>(description.Length < 100,
                                                           "description's max length exceed 100 characters");
            Contract.Requires<ArgumentOutOfRangeException>(name.Length < 100, "name's max length exceed 100 characters");
        }

        ///<summary>
        ///You can retrieve a playlist. If the playlist is not public, it must be owned by the user identified by the access token.
        ///</summary>
        ///<param name="playlistId">The unique ID for the playlist.</param>
        ///<returns>Returns the playlist as PlaylistData</returns>
        public async Task<SingleRootObject<PlaylistData>> GetPlaylist(string playlistId)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(playlistId), "playlistId is null or empty");

            return await BeatsHttpData.GetSingleParsedResult<PlaylistData>("playlists/" + playlistId, null, true);
        }

        /// <summary>
        /// You can retrieve the subscribed users of any public playlist. If the playlist is private, only the owner can view its subscribers.
        /// </summary>
        /// <param name="playlistId">The unique ID for the playlist.</param>
        /// <param name="offset">A zero-based integer offset into the results. Default 0.</param>
        /// <param name="limit">Specifies the maximum number of records to retrieve. The number of results returned will be less than or equal to this value. The maximum permitted value is 200. If a value higher than 200 is specified, no more 200 results will be returned. Default 20.</param>
        /// <returns>Returns the list of UserData</returns>
        public async Task<MultipleRootObject<UserData>> GetPlaylistSubscribers(string playlistId, int offset = 0, int limit = 20)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(playlistId), "playlistId is null or empty");
            Contract.Requires<ArgumentOutOfRangeException>(offset >= 0, "offset is set to less then zero");
            Contract.Requires<ArgumentOutOfRangeException>(limit > 0, "limit is set to zero or less");

            return await BeatsHttpData.GetMultipleParsedResult<UserData>("playlists/" + playlistId + "/subscribers", null, true);
        }

        /// <summary>
        /// You can retrieve the specified public playlists in Beats, as well as any private playlists belonging to the user represented by the access token.
        /// </summary>
        /// <param name="playlistIds">An array of playlist IDs.</param>
        /// <param name="offset">A zero-based integer offset into the results. Default 0.</param>
        /// <param name="limit">Specifies the maximum number of records to retrieve. The number of results returned will be less than or equal to this value. No results are returned if it is set to zero or less. The maximum permitted value is 200. If a value higher than 200 is specified, no more 200 results will be returned. Default 20.</param>
        /// <param name="playlistsOrderBy">Indicates how the results set should be ordered. Default is order alphabetically by name.</param>
        /// <returns></returns>
        public async Task<MultipleRootObject<PlaylistData>> GetMultiplePlaylists(string[] playlistIds, int offset = 0,
                                                                                 int limit = 20,
                                                                                 PlaylistsOrderBy playlistsOrderBy =
                                                                                     PlaylistsOrderBy.NameAscending)
        {
            Contract.Requires<ArgumentNullException>(playlistIds != null, "playlistIds array is null");
            Contract.Requires<ArgumentOutOfRangeException>(offset >= 0, "offset is set to less then zero");
            Contract.Requires<ArgumentOutOfRangeException>(limit > 0, "limit is set to zero or less");

            List<KeyValuePair<string, string>> playlistIdsAsParams = new List<KeyValuePair<string, string>>();
            foreach (string playlistId in playlistIds)
            {
                playlistIdsAsParams.Add(new KeyValuePair<string, string>("ids", playlistId));
            }

            return await BeatsHttpData.GetMultipleParsedResult<PlaylistData>("playlists", playlistIdsAsParams, true);
        }

        /// <summary>
        /// You can update an existing playlist for the user.
        /// </summary>
        /// <param name="playlistId">The unique ID for the playlist.</param>
        /// <param name="name">The name the of playlist. Min Length: 1 character. Max Length: 100 characters.</param>
        /// <param name="description">The description of the playlist. Min Length: 0 characters. Max Length: 100 characters.</param>
        /// <param name="access">Specifies whether the playlist is publicly or privately available. Default: public</param>
        /// <returns>The updated playlist as PlaylistData</returns>
        public async Task<SingleRootObject<PlaylistData>> UpdatePlaylist(string playlistId, string name = "",
                                                                         string description = "", AccessPrivilege accessPrivilege = AccessPrivilege.Public)
        {
            Contract.Requires<ArgumentNullException>(playlistId != null, "playlistId field is null");
            CheckNameAndDescription(name, description);

            List<KeyValuePair<string, string>> paramsToUpdate = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("access", 
                        ParamValueAttributeHelper.GetParamValueOfEnumAttribute<AccessPrivilege>(accessPrivilege))
                };

            if (name != string.Empty)
            {
                paramsToUpdate.Add(new KeyValuePair<string, string>("name", name));
            }

            if (description != string.Empty)
            {
                paramsToUpdate.Add(new KeyValuePair<string, string>("description", description));
            }

            return await BeatsHttpData.PutData<PlaylistData>("playlists/" + playlistId, paramsToUpdate.ToList());
        }

        //TODO: think if should return bool or error
        /// <summary>
        /// You can delete an existing playlist for the user.
        /// </summary>
        /// <param name="playlistId">The unique ID for the playlist.</param>
        /// <returns>True if successful; otherwise false.</returns>
        public async Task<bool> DeletePlaylist(string playlistId)
        {
            Contract.Requires<ArgumentNullException>(playlistId != null, "playlistId field is null");

            return await BeatsHttpData.DeleteData("playlists/" + playlistId, null);
        }

        /// <summary>
        /// You can get the tracks for a playlist.
        /// </summary>
        /// <param name="playlistId">The unique ID for the playlist.</param>
        /// <param name="offset">A zero-based integer offset into the results. Default 0.</param>
        /// <param name="limit">Specifies the maximum number of records to retrieve. The number of results returned will be less than or equal to this value. No results are returned if it is set to zero or less. The maximum permitted value is 200. If a value higher than 200 is specified, no more 200 results will be returned. Default 20.</param>
        /// <param name="refType">An array of referenced objects to include in the response. Only the requested ref collections will be returned. Default is to return all refs. Values: artists, album.</param>
        /// <param name="orderBy">Indicates how the results set should be sorted. Default is popularity desc</param>
        /// <param name="filters">Array of streamability filter operations to apply. Streamability refers to whether an entity, such as a track or album, can be streamed. Each entity can be in one of three states: Streamable, FutureStreamable, NeverStreamable</param>
        /// <returns></returns>
        public async Task<MultipleRootObject<TrackData>> GetTracksInPlaylist(string playlistId, int offset = 0, int limit = 20,
            RefType refType = RefType.AllRefs, AlbumsOrderBy orderBy = AlbumsOrderBy.PopularityDesc,
            StreamabilityFilters filters = null)
        {
            Contract.Requires<ArgumentNullException>(playlistId != null, "playlistId field is null");

            var methodParams = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("offset", offset.ToString()),
                    new KeyValuePair<string, string>("limit", limit.ToString()),
                    new KeyValuePair<string, string>("order_by", ParamValueAttributeHelper.GetParamValueOfEnumAttribute<AlbumsOrderBy>(orderBy))
                };

            var refsValues = EnumHelper.GetFlags<RefType>(refType);

            methodParams.AddRange(
                from refsValue in refsValues
                where (RefType)refsValue != RefType.AllRefs
                select new KeyValuePair<string, string>("refs", ParamValueAttributeHelper.GetParamValueOfEnumAttribute<RefType>(refsValue)));

            if (filters != null)
            {
                foreach (string filterParam in filters.BuildParamsFromFilters())
                {
                    methodParams.Add(new KeyValuePair<string, string>("filters", filterParam));
                }
            }

            return
                await
                BeatsHttpData.GetMultipleParsedResult<TrackData>("playlists/" + playlistId + "/tracks", methodParams, true);
        }
    }
}
