using System;
using CodingChick.BeatsMusicAPI.Core.Data.Albums;
using CodingChick.BeatsMusicAPI.Core.Data.Playlists;
using Newtonsoft.Json.Linq;

namespace CodingChick.BeatsMusicAPI.Core.Data
{
    public class BaseDataConverter : JsonCreationConverter<BaseData>
    {
        protected override BaseData Create(Type objectType, JObject jObject)
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
                return new BaseData();
            }
        }

        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName] != null;
        }
    }
}