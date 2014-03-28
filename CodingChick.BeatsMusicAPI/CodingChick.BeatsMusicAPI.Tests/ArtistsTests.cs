using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Artists;
using CodingChick.BeatsMusicAPI.Core.Data.Playlists;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using NUnit.Framework;

namespace CodingChick.BeatsMusicAPI.Tests
{
    public class ArtistsTests : BaseTest
    {
        [Test]
        public async void GetAllArtistsTest()
        {
            MultipleRootObject<ArtistData> result = await Client.Artists.GetAllArtists(5, 10, ArtistOrderBy.NameAscending);

        }
    }
}
