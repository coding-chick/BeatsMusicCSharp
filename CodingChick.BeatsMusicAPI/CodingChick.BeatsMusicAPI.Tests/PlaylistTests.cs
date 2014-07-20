using System.Collections.Generic;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Playlists;
using NUnit.Framework;

namespace CodingChick.BeatsMusicAPI.Tests
{
    public class PlaylistTests : BaseTest
    {
        private void DeletePlaylist(string id)
        {
            Client.Playlists.DeletePlaylist(id);
        }

        [Test]
        public async void AddTracksToPlaylist()
        {
            const string PlaylistName = "foo";
            const string PlaylistDescription = "bar";

            SingleRootObject<PlaylistData> originalPlaylist =
                await Client.Playlists.CreatePlaylist(PlaylistName, PlaylistDescription);

            AssertResponseIsOK(originalPlaylist);

            var trackIds = new List<string> {"tr58141709", "tr63366021", "tr50508231"};
            bool tracksAdded = await Client.Playlists.AddTracksToPlaylist(originalPlaylist.Data.Id, trackIds);

            Assert.IsTrue(tracksAdded);
            SingleRootObject<PlaylistData> updatedPlaylistData =
                await Client.Playlists.GetPlaylist(originalPlaylist.Data.Id);
            AssertResponseIsOK(updatedPlaylistData);
            PlaylistData updatedPlaylist = updatedPlaylistData.Data;
            Assert.AreEqual(trackIds.Count, updatedPlaylist.TotalTracks);
            DeletePlaylist(originalPlaylist.Data.Id);
        }

        [Test]
        public async void UpdateTracksInPlaylist()
        {
            const string PlaylistName = "foo";
            const string PlaylistDescription = "bar";

            SingleRootObject<PlaylistData> originalPlaylist =
                await Client.Playlists.CreatePlaylist(PlaylistName, PlaylistDescription);

            AssertResponseIsOK(originalPlaylist);

            var trackIds = new List<string> { "tr58141709", "tr63366021", "tr50508231" };
            bool tracksUpdated = await Client.Playlists.UpdateTracksInPlaylist(originalPlaylist.Data.Id, trackIds);

            Assert.IsTrue(tracksUpdated);
            SingleRootObject<PlaylistData> updatedPlaylistData =
                await Client.Playlists.GetPlaylist(originalPlaylist.Data.Id);
            AssertResponseIsOK(updatedPlaylistData);
            PlaylistData updatedPlaylist = updatedPlaylistData.Data;
            Assert.AreEqual(trackIds.Count, updatedPlaylist.TotalTracks);
            DeletePlaylist(originalPlaylist.Data.Id);
        }

        [Test]
        public async void CreatePlaylist()
        {
            const string PlaylistName = "foo";
            const string PlaylistDescription = "bar";

            SingleRootObject<PlaylistData> result =
                await Client.Playlists.CreatePlaylist(PlaylistName, PlaylistDescription);

            AssertResponseIsOK(result);
            Assert.IsTrue(result.Data.Name == PlaylistName);
            Assert.IsTrue(result.Data.Description == PlaylistDescription);

            DeletePlaylist(result.Data.Id);
        }
    }
}