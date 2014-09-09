using System.Text.RegularExpressions;
using Newtonsoft.Json.Serialization;

namespace CodingChick.BeatsMusicAPI.Core.Base.JsonHelpers
{
    public class UnderscoreResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            string propertyNameNoUnderscore =
                Regex.Replace(
                    propertyName, @"([A-Z])([A-Z][a-z])|([a-z])([A-Z])", "$3_$4");
            return base.ResolvePropertyName(propertyNameNoUnderscore);
        }
    }
}