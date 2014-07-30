using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Tracks;
using NUnit.Framework;

namespace CodingChick.BeatsMusicAPI.Tests
{
    public class TracksTest : BaseTest
    {
        [Test]
        public async void GetTrack()
        {
            SingleRootObject<TrackData> data = await Client.Tracks.GetTrack("tr82071543");

            AssertResponseIsOK(data);
            Assert.IsNotNull(data.Data);

        }

        [Test]
        public async void GetTracks()
        {
            var expected = 23;
            MultipleRootObject<TrackData> data = await Client.Tracks.GetTracks(0, expected);

            AssertResponseIsOK(data);
            Assert.AreEqual(expected, data.Data.Count);
        }

        [Test]
        public async void GetMyLibraryTracks()
        {
            MultipleRootObject<TrackData> data = await Client.Tracks.GetMyLibraryTracks("195706866226036992");

            AssertResponseIsOK(data);
            Assert.IsNotNull(data.Data);
            Assert.AreNotEqual(0,data.Data.Count);

        }
    }
}
