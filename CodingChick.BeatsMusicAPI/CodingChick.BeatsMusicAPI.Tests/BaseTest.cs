using System.Collections.Generic;
using System.Linq;
using CodingChick.BeatsMusicAPI.Core;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Search;
using NUnit.Framework;

namespace CodingChick.BeatsMusicAPI.Tests
{
    [TestFixture]
    public class BaseTest
    {


        private const string ClientId = "<your beats music client ID here>";
        private const string ClientSecret = "<your beats music client secret here> ";
        private const string RedirectUrl = @"<your beats music redirect uri here> ";
        private const string Token = @"<to run unit tests you must supply a valid token every 60 minutes>";
        private const string Code = @"<to run unit tests you must supply a valid token every 60 minutes>";


        private BeatsMusicClient _client;
        public BeatsMusicClient Client { get { return _client; }}

        protected string[] TestStrings
        {
            get { return new[] { "foo", "hello", "world"}; }
        }

        [SetUp]
        public void Setup()
        {
            _client = new BeatsMusicClient(ClientId, RedirectUrl, ClientSecret);
            _client.ReadWriteAccessToken = Token;
            _client.Code = Code;
        }

        protected void AssertCollectionHasItems<T>(List<T> list)
        {
            Assert.IsTrue(list.Any());
        }

        protected void AssertResponseIsOK<T>(MultipleRootObject<T> result)
        {
            Assert.IsFalse(result.HasErrors);
        }

        protected void AssertResponseIsOK<T>(SingleRootObject<T> result)
        {
            Assert.IsFalse(result.HasErrors);
        }
    }
}