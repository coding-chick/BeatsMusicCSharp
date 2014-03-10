using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChick.BeatsMusicAPI.Core.Helpers
{
    public class HttpUtilityHelper
    {
        public static string ToQueryString(Dictionary<string, string> queryParams)
        {
            StringBuilder queryParamsBuilder = new StringBuilder();

            foreach (KeyValuePair<string, string> keyValuePair in queryParams)
            {
                queryParamsBuilder.Append(string.Format("&{0}={1}", Uri.EscapeDataString(keyValuePair.Key),
                                                 Uri.EscapeDataString(keyValuePair.Value)));
            }

            return queryParamsBuilder.ToString();
        }

        public static string CreateFullAddess(string baseAddress, string method, Dictionary<string, string> queryParams)
        {
            StringBuilder addressCallBuilder = new StringBuilder();
            addressCallBuilder.Append(baseAddress);
            addressCallBuilder.Append(method);
            addressCallBuilder.Append("?");
            addressCallBuilder.Append(ToQueryString(queryParams));

            return addressCallBuilder.ToString();
        }
    }

}
