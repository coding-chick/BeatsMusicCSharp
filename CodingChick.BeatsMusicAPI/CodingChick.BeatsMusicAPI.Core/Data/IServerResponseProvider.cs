using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingChick.BeatsMusicAPI.Core.Data
{
    public interface IServerResponseProvider
    {
        string ServerJson { get; set; }
    }
}
