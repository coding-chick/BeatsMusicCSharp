using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Ratings;
using NUnit.Framework;

namespace CodingChick.BeatsMusicAPI.Tests
{
    public class RatingTests : BaseTest
    {

        [Test]
        public async void RetrieveRatings()
        {
            var result = await Client.Ratings.GetAllRatings("195706866226036992");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreNotEqual(0, result.Data.Count);
            
        }

    }
}