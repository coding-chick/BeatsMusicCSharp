using System;

namespace CodingChick.BeatsMusicAPI.Core.Helpers
{
    public class ParamValueAttribute : Attribute
    {
        public ParamValueAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}