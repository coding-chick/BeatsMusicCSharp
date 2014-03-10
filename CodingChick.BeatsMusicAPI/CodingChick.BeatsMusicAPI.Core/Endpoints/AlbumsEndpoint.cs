using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Albums;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    //TODO: add the rest of the API
    public class AlbumsEndpoint 
    {
        private readonly BeatsHttpData _beatsHttpData;

        internal AlbumsEndpoint(BeatsHttpData beatsHttpData)
        {
            _beatsHttpData = beatsHttpData;
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

            return await _beatsHttpData.GetMultipleParsedResult<AlbumData>("albums", searchParams);
        }
    }

  
}
