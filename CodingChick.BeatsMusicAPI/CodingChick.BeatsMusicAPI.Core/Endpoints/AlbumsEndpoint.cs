using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Albums;
using CodingChick.BeatsMusicAPI.Core.Data.Artists;
using CodingChick.BeatsMusicAPI.Core.Data.Reviews;
using CodingChick.BeatsMusicAPI.Core.Data.Tracks;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class AlbumsEndpoint : BaseEndpoint
    {

        internal AlbumsEndpoint(BeatsHttpData beatsHttpData) : base(beatsHttpData)
        {
        }

        public async Task<MultipleRootObject<AlbumData>> GetAlbumCollection(OrderBy orderBy, int limit, int offset)
        {
            var orderByParamValue = ParamValueAttributeHelper.GetParamValueOfEnumAttribute<OrderBy>(orderBy);

            if (limit > 200)
                limit = 200;

            Dictionary<string, string> searchParams = new Dictionary<string, string>()
                {
                    {"order_by", orderByParamValue},
                    {"limit", limit.ToString()},
                    {"offset", offset.ToString()}
                };

            return await BeatsHttpData.GetMultipleParsedResult<AlbumData>("albums", searchParams);
        }

        public async Task<SingleRootObject<AlbumData>> GetAlbumById(string albumId)
        {
            //Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(albumId));

            return
                await
                    BeatsHttpData.GetSingleParsedResult<AlbumData>(GetSingleFirstLevelMethod(albumId), null);
        }

        public async Task<MultipleRootObject<ArtistData>> GetArtistsInAlbumId(string albumId)
        {
            return
                await
                    BeatsHttpData.GetMultipleParsedResult<ArtistData>(GetSingleFirstLevelMethod(albumId) + "/artists", null);
        }

        public async Task<MultipleRootObject<TrackData>> GetTracksInAlbum(string albumId)
        {
            return
               await
                   BeatsHttpData.GetMultipleParsedResult<TrackData>(GetSingleFirstLevelMethod(albumId) + "/tracks", null);
        }

        public async Task<SingleRootObject<ReviewData>> GetReviewDataForAlbum(string albumId)
        {
            return
             await
                 BeatsHttpData.GetSingleParsedResult<ReviewData>(GetSingleFirstLevelMethod(albumId) + "/review", null);
        }

        public async Task<MultipleRootObject<AlbumData>> GetAllCompanionAlbums(string albumId)
        {
            return
                await
                    BeatsHttpData.GetMultipleParsedResult<AlbumData>(GetSingleFirstLevelMethod(albumId) + "/companion_albums", null);
        }

        private string GetSingleFirstLevelMethod(string albumId)
        {
            return "albums/" + albumId;
        }
    }




}
