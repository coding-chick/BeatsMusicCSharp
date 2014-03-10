using System.Collections.Generic;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Activities;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class ActivitiesEndpoint
    {
        private readonly BeatsHttpData _beatsHttpData;

        internal ActivitiesEndpoint(BeatsHttpData beatsHttpData)
        {
            _beatsHttpData = beatsHttpData;
            
        }
        public async Task<MultipleRootObject<ActivityData>> GetAllActivities()
        {
            return await _beatsHttpData.GetMultipleParsedResult<ActivityData>("activities", new Dictionary<string, string>());
        }

        public async Task<SingleRootObject<ActivityData>> GetActivityById(string activityId)
        {
            return await _beatsHttpData.GetSingleParsedResult<ActivityData>("activities/" + activityId, new Dictionary<string, string>());
        }
    }
}
