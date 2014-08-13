using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints.Enums
{
    public enum ImageSize
    {
        /// <summary>
        /// 180x80
        /// </summary>
         [ParamValue("thumb")]
        Thumbnail = 0,
        /// <summary>
        /// 190x230
         /// </summary>
         [ParamValue("small")]
        Small = 1,
        /// <summary>
        /// 375x250
         /// </summary>
         [ParamValue("medium")]
        Medium =2,
        /// <summary>
        /// 750x500
         /// </summary>
         [ParamValue("large")]
        Large = 3
    }
}
