﻿using Newtonsoft.Json;

namespace CodingChick.BeatsMusicAPI.Core.Data
{
    public abstract class RootObject<T> 
    {
        public string Code { get; set; }

        public bool HasErrors
        {
            get { return Code.ToLower() != "ok"; }
        }

    }
}