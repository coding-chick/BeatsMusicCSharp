using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Me;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class MeEndpoint : BaseEndpoint
    {
        internal MeEndpoint(BeatsMusicManager beatsMusicManager) : base(beatsMusicManager)
        {
        }

        /// <summary>
        /// You can retrieve information about an access token, including the user ID.
        /// </summary>
        /// <returns></returns>
        public async Task<SingleRootObject<MeData>> GetMeInfo()
        {
            return await BeatsMusicManager.GetSingleParsedResult<MeData>("me", new List<KeyValuePair<string, string>>(), true);
        }

        
    }
}