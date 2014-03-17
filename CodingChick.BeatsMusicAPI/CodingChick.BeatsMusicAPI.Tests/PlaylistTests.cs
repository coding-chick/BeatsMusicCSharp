using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Playlists;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using NUnit.Framework;

namespace CodingChick.BeatsMusicAPI.Tests
{
    public class PlaylistTests : BaseTest
    {
        [Test]
        public async void CreatePlaylist()
        {
            const string PlaylistName = "foo";
            const string PlaylistDescription = "bar";

            SingleRootObject<PlaylistData> result = await Client.Playlists.CreatePlaylist(PlaylistName, PlaylistDescription);

            AssertResponseIsOK(result);
            Assert.IsTrue(result.Data.Name == PlaylistName);
            Assert.IsTrue(result.Data.Description == PlaylistDescription);

            DeletePlaylist(result.Data.Id);
        }

        private void DeletePlaylist(string id)
        {
            Client.Playlists.DeletePlaylist(id);
        }
    }
}
