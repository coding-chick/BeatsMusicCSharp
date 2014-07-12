using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data.Content;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    //TODO: wonder why this enpoint doesn't seem to work
    public class FollowEndpoint : BaseEndpoint
    {
        internal FollowEndpoint(BeatsMusicManager beatsMusicManager) : base(beatsMusicManager)
        {
        }

        public async Task<bool> Follow(string userId, string followId, FollowType entityTypeToFollow)
        {
            string typeToFollow = ParamValueAttributeHelper.GetParamValueOfEnumAttribute<FollowType>(entityTypeToFollow);

            var result =
                await
                    BeatsMusicManager.PutData<ContentData>(
                        string.Format("{0}/{1}/follows/{2}", typeToFollow, userId, followId), null);

            return result.Code.ToLower() == "ok";
        }
    }
}