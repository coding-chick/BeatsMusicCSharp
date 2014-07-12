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
        private readonly BeatsMusicManager _beatsMusicManager;

        internal BaseEndpoint(BeatsMusicManager beatsMusicManager)
        {
            _beatsMusicManager = beatsMusicManager;
        }

        internal BeatsMusicManager BeatsMusicManager
        {
            get { return _beatsMusicManager; }
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
            var methodParams = new List<KeyValuePair<string, string>>();
            methodParams = AddOffsetAndLimitParams(methodParams, offset, limit);
            methodParams = AddOrderByParam<AlbumsOrderBy>(orderBy, methodParams);

            AddFlagedEnumValues<PlaylistRefType>(playlistRefType, methodParams);
            return methodParams;
        }

        protected List<KeyValuePair<string, string>> AddOrderByParam<T>(Enum orderBy, List<KeyValuePair<string, string>> methodParams)
        {
            methodParams.Add(new KeyValuePair<string, string>("order_by",
                ParamValueAttributeHelper.GetParamValueOfEnumAttribute<T>(
                    orderBy)));

            return methodParams;
        }

        protected List<KeyValuePair<string, string>> AddFlagedEnumValues<T>(Enum enumRefType, List<KeyValuePair<string, string>> methodParams)
        {
            IEnumerable<Enum> refsValues = EnumHelper.GetFlags<T>(enumRefType);

            methodParams.AddRange(
                refsValues.Where(refsValue => !refsValue.HasFlag(PlaylistRefType.AllRefs))
                          .Select(refsValue => new KeyValuePair<string, string>("refs", 
                              ParamValueAttributeHelper.GetParamValueOfEnumAttribute<T>(refsValue))));

            return methodParams;
        }

        protected List<KeyValuePair<string, string>> AddOffsetAndLimitParams(List<KeyValuePair<string, string>> methodParams, 
            int offset, int limit)
        {
            methodParams.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
            methodParams.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
            return methodParams;
        }


        protected void ValidateIdOffsetLimit(int offset, int limit)
        {
            Contract.Requires<ArgumentOutOfRangeException>(offset >= 0, "offset is set to less then zero");
            Contract.Requires<ArgumentOutOfRangeException>(limit > 0, "limit is set to zero or less");
        }
    }
}