using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Activities;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class ActivitiesEndpoint : BaseEndpoint
    {

        internal ActivitiesEndpoint(BeatsHttpData beatsHttpData) : base(beatsHttpData)
        {
        }

        public async Task<MultipleRootObject<ActivityData>> GetAllActivities()
        {
            return await BeatsHttpData.GetMultipleParsedResult<ActivityData>("activities", null);
        }

        public async Task<SingleRootObject<ActivityData>> GetActivityById(string activityId)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(activityId));
            return await BeatsHttpData.GetSingleParsedResult<ActivityData>("activities/" + activityId, new Dictionary<string, string>());
        }
    }
}
