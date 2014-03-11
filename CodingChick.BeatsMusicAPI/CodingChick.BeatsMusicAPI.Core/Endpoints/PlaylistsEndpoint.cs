using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Playlists;
using CodingChick.BeatsMusicAPI.Core.Data.Users;
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
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(name), "name field is null or empty");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(description), "description field null or empty");
            Contract.Requires<ArgumentOutOfRangeException>(description.Length < 100, "description's max length exceed 100 characters");
            Contract.Requires<ArgumentOutOfRangeException>(name.Length < 100, "name's max length exceed 100 characters");

            Dictionary<string, string> dataParams = new Dictionary<string, string>()
                {
                    {"name", name},
                    {"description", description},
                    {"access", ParamValueAttributeHelper.GetParamValueOfEnumAttribute<AccessPrivilege>(accessPrivilege)}
                };

            return await BeatsHttpData.PostData<PlaylistData>("playlists", dataParams.ToList());
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
        public async Task<MultipleRootObject<UserData>> GetPlaylistSubscribers(string playlistId, int offset = 0, int limit= 20)
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
    }
}
