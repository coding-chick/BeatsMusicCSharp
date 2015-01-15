using System;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using NUnit.Framework;

namespace CodingChick.BeatsMusicAPI.Tests
{
    public class ImagesTests : BaseTest
    {
        [Test]
        public async void GetArtistImage()
        {
            var imageUri = await Client.Images.GetArtistImageUri("ar1033754", ImageSize.Large);
            Assert.IsNotNull(imageUri);
        }

        [Test]
        public async void GetAlbumImage()
        {
            var imageUri = await Client.Images.GetAlbumImageUri("al74961607", ImageSize.Large);
            Assert.IsNotNull(imageUri);
        }

        [Test]
        public async void GetTrackImage()
        {
            var imageUri = await Client.Images.GetTrackImageUri("tr82071543", ImageSize.Large);
            Assert.IsNotNull(imageUri);
        }

        [Test]
        public async void GetPlaylistImage()
        {
            var imageUri = await Client.Images.GetPlaylistImageUri("pl266636933575344128", ImageSize.Large);
            Assert.IsNotNull(imageUri);
        }
    }
}