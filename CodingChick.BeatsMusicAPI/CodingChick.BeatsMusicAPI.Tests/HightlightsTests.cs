using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Content;
using NUnit.Framework;

namespace CodingChick.BeatsMusicAPI.Tests
{
    public class HightlightsTests : BaseTest
    {
        [Test]
        public async void GetFeaturedContentTest()
        {
            MultipleRootObject<ContentData> result = await Client.Highlights.GetFeaturedContent();

            AssertResponseIsOK(result);
            AssertCollectionHasItems(result.Data);
        }

        [Test]
        public async void GetEditorPicksContentTest()
        {
            MultipleRootObject<ContentData> result = await Client.Highlights.GetEditorPicksContent();

            AssertResponseIsOK(result);
            AssertCollectionHasItems(result.Data);
        }
    }
}