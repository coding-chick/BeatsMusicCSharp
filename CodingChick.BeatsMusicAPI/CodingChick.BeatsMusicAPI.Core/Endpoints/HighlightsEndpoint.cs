using System;
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
        internal HighlightsEndpoint(BeatsMusicManager beatsMusicManager) : base(beatsMusicManager)
        {
        }

        /// <summary>
        /// You can retrieve the current Featured content, which has been selected by Beats Music editors.
        /// </summary>
        /// <returns>A collection of ContentData</returns>
        public async Task<MultipleRootObject<ContentData>> GetFeaturedContent()
        {
            return await BeatsMusicManager.GetMultipleParsedResult<ContentData>("discoveries/featured", null);
        }


        public async Task<MultipleRootObject<ContentData>> GetEditorPicksContent()
        {
            return await BeatsMusicManager.GetMultipleParsedResult<ContentData>("discoveries/editor_picks", null);
            
        }
    }
}
