using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingChick.BeatsMusicAPI.Core.Helpers
{
    public class EnumHelper
    {
        public static IEnumerable<Enum> GetFlags<T>(Enum input)
        {
            foreach (var value in GetValues(input, typeof(T)))
            {
                if (input.HasFlag(value))
                    yield return value;
            }
        }


        public static IEnumerable<Enum> GetValues(Enum input, Type enumType)
        {

            return from field in enumType.GetFields()
                   where field.IsLiteral && !String.IsNullOrEmpty(field.Name)
                   select (Enum)field.GetValue(null);
        }
    }
}