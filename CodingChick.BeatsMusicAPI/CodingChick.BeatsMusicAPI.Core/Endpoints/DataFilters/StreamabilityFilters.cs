using System.Collections.Generic;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints.DataFilters
{
    public class StreamabilityFilters
    {
        public bool? Streamable { get; set; }
        public bool? FutureStreamable { get; set; }
        public bool? NeverStreamable { get; set; }

        public List<string> BuildParamsFromFilters()
        {
            var streamableParams = new List<string>();
            if (Streamable.HasValue)
            {
                streamableParams.Add("streamable:"+ Streamable.Value.ToString().ToLower());
            }

            if (FutureStreamable.HasValue)
            {
                streamableParams.Add("future_streamable:" + FutureStreamable.Value.ToString().ToLower());
            }

            if (NeverStreamable.HasValue)
            {
                streamableParams.Add("never_streamable:" + NeverStreamable.Value.ToString().ToLower());
            }

            return streamableParams;
        }
    }
}