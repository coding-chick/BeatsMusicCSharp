using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Artists;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using NUnit.Framework;

namespace CodingChick.BeatsMusicAPI.Tests
{
    public class FollowTests : BaseTest
    {
        [Test]
        public async void FollowTest()
        {
            //Arrange
            //users/154708885926576640/follows/eg109673939000754944
            //users/154708885926576640/follows/eg109673939000754944
            //Act
            bool result = await Client.Follow.Follow("154708885926576640", "eg109673939000754944", FollowType.User);
            
            //Assert
            Assert.IsTrue(result);
        }

        
    }
}