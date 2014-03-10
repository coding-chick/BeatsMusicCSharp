using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Data;
using CodingChick.BeatsMusicAPI.Core.Data.Playlists;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class PlaylistsEndpoint
    {
        private readonly BeatsHttpData _beatsHttpData;

        internal PlaylistsEndpoint(BeatsHttpData beatsHttpData)
        {
            _beatsHttpData = beatsHttpData;
        }

        //TODO: Add access privlidges
        public async Task<SingleRootObject<PlaylistData>> CreatePlaylist(string name, string description)
        {
            //TODO: fix this to do something meaningful
            if (name.Length == 0 || name.Length > 100 || description.Length > 100 || description.Length == 0)
            {
                //TODO:Add custom exception
            }

            Dictionary<string, string> dataParams = new Dictionary<string, string>()
                {
                    {"name", name},
                    {"description", description},
                    {"access", "public"}
                };
            
            return await _beatsHttpData.PostData<PlaylistData>("playlists", dataParams);
        }
    }
}
