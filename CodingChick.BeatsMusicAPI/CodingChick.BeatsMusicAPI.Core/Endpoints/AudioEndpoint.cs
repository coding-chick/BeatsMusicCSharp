using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Audio;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class AudioEndpoint : BaseEndpoint
    {
        internal AudioEndpoint(BeatsHttpData beatsHttpData) : base(beatsHttpData)
        {
        }

        
        //TODO: finish the method params with the bitrate
        /// <summary>
        /// Gets the required information to play a track for a user by streaming the specified audio asset.
        /// </summary>
        /// <param name="trackId">The unique ID of the track.</param>
        /// <param name="bitrate">The desired bitrate of an audio asset. Values: lowest, highest</param>
        /// <param name="aquire">Determines whether to automatically acquire streaming rights. Values: true, false. Default false. This parameter should only be used when: The user explicitly clicks on a play button. The audio stream request returns a StreamContention error.</param>
        /// <returns></returns>
        public async Task<SingleRootObject<AudioData>> GetAudioStreamingInfo(string trackId, Bitrate bitrate,
            bool aquire = false)
        {
            var methodParams = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("aquire", Convert.ToInt32(aquire).ToString())
            };

            return await BeatsHttpData.GetSingleParsedResult<AudioData>(string.Format("tracks/{0}/audio", trackId), methodParams,
                true);
        }
    }

    public enum Bitrate
    {
        Lowest,
        Highest
    }
}
