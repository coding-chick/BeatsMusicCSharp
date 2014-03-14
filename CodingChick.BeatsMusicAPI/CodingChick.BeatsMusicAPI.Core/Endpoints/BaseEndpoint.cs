using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using CodingChick.BeatsMusicAPI.Core.Base;
using CodingChick.BeatsMusicAPI.Core.Endpoints.DataFilters;
using CodingChick.BeatsMusicAPI.Core.Endpoints.Enums;
using CodingChick.BeatsMusicAPI.Core.Helpers;

namespace CodingChick.BeatsMusicAPI.Core.Endpoints
{
    public class BaseEndpoint
    {
        private readonly BeatsHttpData _beatsHttpData;

        internal BaseEndpoint(BeatsHttpData beatsHttpData)
        {
            _beatsHttpData = beatsHttpData;
        }

        internal BeatsHttpData BeatsHttpData
        {
            get { return _beatsHttpData; }
        }


        protected void AddStreamabilityFilterParams(StreamabilityFilters filters, List<KeyValuePair<string, string>> methodParams)
        {
            if (filters != null)
            {
                foreach (string filterParam in filters.BuildParamsFromFilters())
                {
                    methodParams.Add(new KeyValuePair<string, string>("filters", filterParam));
                }
            }
        }

        protected List<KeyValuePair<string, string>> CreateMethodParams(int offset, int limit, PlaylistRefType playlistRefType, AlbumsOrderBy orderBy)
        {
            var methodParams = AddOffsetAndLimitParams(offset, limit);
            methodParams.Add(new KeyValuePair<string, string>("order_by",
                                             ParamValueAttributeHelper.GetParamValueOfEnumAttribute<AlbumsOrderBy>(
                                                 orderBy)));

            AddFlagedEnumValues<PlaylistRefType>(playlistRefType, methodParams);
            return methodParams;
        }

        protected void AddFlagedEnumValues<T>(Enum enumRefType, List<KeyValuePair<string, string>> methodParams)
        {
            IEnumerable<Enum> refsValues = EnumHelper.GetFlags<T>(enumRefType);

            methodParams.AddRange(
                refsValues.Where(refsValue => !refsValue.HasFlag(PlaylistRefType.AllRefs))
                          .Select(refsValue => new KeyValuePair<string, string>("refs", 
                              ParamValueAttributeHelper.GetParamValueOfEnumAttribute<T>(refsValue))));
        }

        protected List<KeyValuePair<string, string>> AddOffsetAndLimitParams(int offset, int limit)
        {
            var methodParams = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("offset", offset.ToString()),
                    new KeyValuePair<string, string>("limit", limit.ToString())
                   
                };
            return methodParams;
        }


        protected void ValidateIdOffsetLimit(int offset, int limit)
        {
            Contract.Requires<ArgumentOutOfRangeException>(offset >= 0, "offset is set to less then zero");
            Contract.Requires<ArgumentOutOfRangeException>(limit > 0, "limit is set to zero or less");
        }
    }
}