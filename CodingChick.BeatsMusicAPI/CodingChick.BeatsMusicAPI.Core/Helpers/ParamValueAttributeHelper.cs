using System;
using System.Linq;
using System.Reflection;
using CodingChick.BeatsMusicAPI.Core.Endpoints;

namespace CodingChick.BeatsMusicAPI.Core.Helpers
{
    public class ParamValueAttributeHelper
    {
        public static string GetParamValueOfEnumAttribute<T>(Enum enumMember)
        {
            MemberInfo memberInfo = typeof(T).GetMember(enumMember.ToString())
                                             .FirstOrDefault();

            if (memberInfo != null)
            {
                ParamValueAttribute attribute = (ParamValueAttribute)
                                                memberInfo.GetCustomAttributes(typeof(ParamValueAttribute), false)
                                                          .FirstOrDefault();

                return attribute.Name;
            }

            return null;
        }
    }
}