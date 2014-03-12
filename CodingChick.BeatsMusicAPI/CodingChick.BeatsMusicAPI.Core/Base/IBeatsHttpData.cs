using System.Collections.Generic;
using System.Threading.Tasks;
using CodingChick.BeatsMusicAPI.Core.Data;

namespace CodingChick.BeatsMusicAPI.Core.Base
{
    internal interface IBeatsHttpData
    {
        Task<MultipleRootObject<T>> GetMultipleParsedResult<T>(string methodName, List<KeyValuePair<string, string>> methodParams, bool useToken = false);
        Task<SingleRootObject<T>> GetSingleParsedResult<T>(string methodName, List<KeyValuePair<string, string>> methodParams, bool useToken = false);
        Task<SingleRootObject<T>> PostData<T>(string methodName, List<KeyValuePair<string, string>> dataParams);
        Task<SingleRootObject<T>> PutData<T>(string methodName, List<KeyValuePair<string, string>> dataParams);
        Task<bool> DeleteData(string methodName, List<KeyValuePair<string, string>> dataParams);
    }
}