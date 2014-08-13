using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Ratings;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class RatingsEndpoint : BaseEndpoint
    {
        internal RatingsEndpoint(BeatsMusicManager beatsMusicManager)
            : base(beatsMusicManager)
        {
        }

        /// <summary>
        ///     Retrieve a single rating from a user
        /// </summary>
        /// <param name="userId">The unique ID for the user.</param>
        /// <param name="ratingId">The unique ID for the rating.</param>
        /// <returns></returns>
        public async Task<SingleRootObject<RatingData>> GetRating(string userId, string ratingId)
        {
            ValidateUserIdRatingId(userId, ratingId);

            return await BeatsMusicManager.GetSingleParsedResult<RatingData>(GetMethodName(userId, ratingId), null, true);
        }

        /// <summary>
        ///     Retrieve all the ratings from a user
        /// </summary>
        /// <param name="userId">The unique ID for the user.</param>
        /// <param name="offset">A zero-based integer offset into the results. Default 0.</param>
        /// <param name="limit">
        ///     Specifies the maximum number of records to retrieve. The number of results returned will be less
        ///     than or equal to this value. No results are returned if it is set to zero or less. The maximum permitted value is
        ///     200. If a value higher than 200 is specified, no more 200 results will be returned. Default 20.
        /// </param>
        /// <returns></returns>
        public async Task<MultipleRootObject<RatingData>> GetAllRatings(string userId, int offset = 0, int limit = 20)
        {
            ValidateUserId(userId);
            ValidateIdOffsetLimit(offset, limit);

            var methodParams = new List<KeyValuePair<string, string>>();
            methodParams = AddOffsetAndLimitParams(methodParams, offset, limit);

            return
                await
                    BeatsMusicManager.GetMultipleParsedResult<RatingData>(GetMethodName(userId), methodParams, true);
        }

        /// <summary>
        ///     You can update an existing rating for the user
        /// </summary>
        /// <param name="userId">The unique ID for the user.</param>
        /// <param name="ratingId">The unique ID for the rating.</param>
        /// <param name="newRating">Rating to update the rating with.</param>
        /// <returns>Updating rating as RatingData</returns>
        public async Task<SingleRootObject<RatingData>> UpdateRating(string userId, string ratingId, Rating newRating)
        {
            ValidateUserIdRatingId(userId, ratingId);

            var dataParams = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("rating", ((int) newRating).ToString())
            };
            return
                await
                    BeatsMusicManager.PutData<RatingData>(GetMethodName(userId, ratingId), dataParams);
        }

        /// <summary>
        ///     You can delete an existing rating for the user
        /// </summary>
        /// <param name="userId">The unique ID for the user.</param>
        /// <param name="ratingId">The unique ID for the rating.</param>
        /// <returns>True if successful; otherwise false.</returns>
        public async Task<bool> DeleteRating(string userId, string ratingId)
        {
            ValidateUserIdRatingId(userId, ratingId);

            return await BeatsMusicManager.DeleteData(string.Format("users/{0}/ratings/{1}", userId, ratingId), null);
        }

        /// <summary>
        ///     Get the method for the Rating endpoint
        /// </summary>
        /// <param name="userId">The unique ID for the user.</param>
        /// <param name="ratingId">The unique ID for the rating.</param>
        /// <returns></returns>
        private static string GetMethodName(string userId, string ratingId = "")
        {
            return string.Format("users/{0}/ratings{1}", userId, string.IsNullOrWhiteSpace(ratingId) ? "" : string.Format("/{0}", ratingId));
        }

        /// <summary>
        ///     Validate the userId and ratingId fields
        /// </summary>
        /// <param name="userId">The unique ID for the user.</param>
        /// <param name="ratingId">The unique ID for the rating.</param>
        private static void ValidateUserIdRatingId(string userId, string ratingId)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(ratingId),
                "ratingId field is null or empty");
            ValidateUserId(userId);
        }

        /// <summary>
        ///     Validate the userId field
        /// </summary>
        /// <param name="userId">The unique ID for the user.</param>
        private static void ValidateUserId(string userId)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrWhiteSpace(userId), "userId field is null or empty");
        }
    }
}