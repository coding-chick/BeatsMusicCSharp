using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Activities;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    //TODO: add comments
    public class ActivitiesEndpoint : BaseEndpoint
    {

        internal ActivitiesEndpoint(BeatsMusicManager beatsMusicManager) : base(beatsMusicManager)
        {
        }

        public async Task<MultipleRootObject<ActivityData>> GetAllActivities()
        {
            return await BeatsMusicManager.GetMultipleParsedResult<ActivityData>("activities", null);
        }

        public async Task<SingleRootObject<ActivityData>> GetActivityById(string activityId)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(activityId), "activityId should contain a value");

            return await BeatsMusicManager.GetSingleParsedResult<ActivityData>("activities/" + activityId, null);
        }
    }
}
