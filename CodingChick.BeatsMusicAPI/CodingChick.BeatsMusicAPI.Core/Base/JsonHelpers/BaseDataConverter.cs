using System;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Albums;
using CodingChick.BeatsMusicAPI.Core.Data.Playlists;
using Newtonsoft.Json.Linq;

namespace CodingChick.BeatsMusicAPI.Core.Base.JsonHelpers
{
    public class BaseDataConverter : JsonCreationConverter<BaseConvertedData>
    {
        protected override BaseConvertedData Create(Type objectType, JObject jObject)
        {
            if (FieldExists("essential", jObject))
            {
                return new AlbumData();
            }
            else if (FieldExists("user_display_name", jObject))
            {
                return new PlaylistData();
            }
            else
            {
                return new BaseConvertedData();
            }
        }

        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName] != null;
        }
    }
}