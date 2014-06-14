using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Search;
using NUnit.Framework;

namespace CodingChick.BeatsMusicAPI.Tests
{
    public class SearchTests : BaseTest
    {
        private const string AlbumName = "Beat The Drum";
        private const string ArtistName = "Runrig";
        private const string AlbumId = "al4986113";

        [Test]
        public async void SearchByAlbum()
        {
            var result = await Client.Search.SearchByAlbum(AlbumName);

            AssertResponseIsOK(result);
            AssertCollectionHasItems(result.Data);
            Assert.True(result.Data.Any(r => r.Display.Contains(AlbumName)));
            Assert.True(result.Data.Any(r => r.Detail.Contains(ArtistName)));
            Assert.True(result.Data.Any(r => r.Id.Contains(AlbumId)));
        }

        private const string ArtistId = "ar208846";

        [Test]
        public async void SearchByArtist()
        {
            var result = await Client.Search.SearchByArtist(ArtistName);

            AssertResponseIsOK(result);
            AssertCollectionHasItems(result.Data);
            Assert.True(result.Data.Any(r => r.Display.Contains(ArtistName)));
            Assert.True(result.Data.Any(r => r.Id.Contains(ArtistId)));
        }

        
        private const string GenreName = "Beats Hip-Hop";
        private const string GenreDetail = "beatshiphop";
        private const string GenreId = "eg97074088215970048";

        [Test]
        public async void SearchByGenre()
        {
            var result = await Client.Search.SearchByGenre("hip");

            AssertResponseIsOK(result);
            AssertCollectionHasItems(result.Data);
            Assert.True(result.Data.Any(r => r.Display.Contains(GenreName)));
            Assert.True(result.Data.Any(r => r.Detail.Contains(GenreDetail)));
            Assert.True(result.Data.Any(r => r.Id.Contains(GenreId)));
        }

        private const string PlaylistName = "Music for Power Tools";
        private const string PlaylistDetail = "Pitchfork";
        private const string PlaylistId = "pl142123717680431104";

        [Test]
        public async void SearchByPlaylist()
        {
            var result = await Client.Search.SearchByPlaylist(PlaylistName);

            AssertResponseIsOK(result);
            AssertCollectionHasItems(result.Data);
            Assert.True(result.Data.Any(r => r.Display.Contains(PlaylistName)));
            Assert.True(result.Data.Any(r => r.Detail.Contains(PlaylistDetail)));
            Assert.True(result.Data.Any(r => r.Id.Contains(PlaylistId)));
        }

        private const string TrackName = "What's My Name?";
        private const string TrackDetail = "Rihanna";
        private const string TrackId = "tr57535983";

        [Test]
        public async void SearchByTrack()
        {
            var result = await Client.Search.SearchByTrack(TrackName);

            AssertResponseIsOK(result);
            AssertCollectionHasItems(result.Data);
            Assert.True(result.Data.Any(r => r.Display.Contains(TrackName)));
            Assert.True(result.Data.Any(r => r.Detail.Contains(TrackDetail)));
            Assert.True(result.Data.Any(r => r.Id.Contains(TrackId)));
           
        }

        private const string UserDetail = "codingchick";
        private const string UserName = "Efrat Barak";
        private const string UserId = "154708885926576640";

        [Test]
        public async void SearchByUser()
        {
            var result = await Client.Search.SearchByUser(UserDetail);

            AssertResponseIsOK(result);
            AssertCollectionHasItems(result.Data);
            Assert.True(result.Data.Any(r => r.Display.Contains(UserName)));
            Assert.True(result.Data.Any(r => r.Detail.Contains(UserDetail)));
            Assert.True(result.Data.Any(r => r.Id.Contains(UserId)));
        }

    }
}
