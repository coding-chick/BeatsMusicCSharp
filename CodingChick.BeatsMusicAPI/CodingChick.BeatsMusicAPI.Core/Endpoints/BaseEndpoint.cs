using CodingChick.BeatsMusicAPI.Core.Base;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class BaseEndpoint
    {
        private readonly BeatsHttpData _beatsHttpData;

        internal BaseEndpoint(BeatsHttpData beatsHttpData)
        {
            _beatsHttpData = beatsHttpData;
        }

        internal BeatsHttpData BeatsHttpData
        {
            get { return _beatsHttpData; }
        }
    }
}