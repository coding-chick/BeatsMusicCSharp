using System.Linq;
using NUnit.Framework;

namespace CodingChick.BeatsMusicAPI.Tests
{
    public class GenreTests : BaseTest
    {
        [Test]
        public async void GetGenreByIdTest()
        {
            //Arrange

            //Act
            var result = await Client.Genre.GetGenreById("eg77531656377860096");

            //Assert
            AssertResponseIsOK(result);
            Assert.IsTrue(result.Data.Id == "eg77531656377860096");
        }

        [Test]
        public async void GetAllGenresTest()
        {
            //Arrange

            //Act
            var result = await Client.Genre.GetAllGenres(200);

            //Assert
            AssertResponseIsOK(result);
            AssertCollectionHasItems(result.Data);
            Assert.IsTrue(result.Data.Any(a => a.Id == "eg77531656377860096"));
        }

        [Test]
        public async void GetEditorPicksInGenreTest()
        {
            //Arrange

            //Act
            var result = await Client.Genre.GetEditorPicksInGenre("eg77531656377860096");

            //Assert
            AssertResponseIsOK(result);
            AssertCollectionHasItems(result.Data);
        }
    }
}