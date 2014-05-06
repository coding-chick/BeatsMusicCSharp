using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Audio;
using CodingChick.BeatsMusicAPI.Core.Endpoints;
using NUnit.Framework;

namespace CodingChick.BeatsMusicAPI.Tests
{
    public class AudioTests : BaseTest
    {
        [Test]
        public async void GetAudioStreamingInfoTest()
        {
            SingleRootObject<AudioData> result = await Client.Audio.GetAudioStreamingInfo("tr61032803", Bitrate.Highest, true);

            base.AssertResponseIsOK(result);
            Assert.IsTrue(result.Data.Refs.Track.Id == "tr61032803");
        }
    }
}