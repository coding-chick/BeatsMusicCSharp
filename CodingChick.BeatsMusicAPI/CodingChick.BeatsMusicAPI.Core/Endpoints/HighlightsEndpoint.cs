﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Content;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class HighlightsEndpoint : BaseEndpoint
    {
        internal HighlightsEndpoint(BeatsHttpData beatsHttpData) : base(beatsHttpData)
        {
        }

        /// <summary>
        /// You can retrieve the current Featured content, which has been selected by Beats Music editors.
        /// </summary>
        /// <returns>A collection of ContentData</returns>
        public async Task<MultipleRootObject<ContentData>> GetFeaturedContent()
        {
            return await BeatsHttpData.GetMultipleParsedResult<ContentData>("discoveries/featured", null);
        }
    }
}