using System;
using System.Collections.Generic;
using System.Linq;

namespace EtsyApi.Extensions
{
    public static class EnumExtensions
    {
        public static TEnum[] GetNonEmptyFlags<TEnum>(this TEnum flagsEnumValue) where TEnum : Enum
        {
            return Enum
                .GetValues(typeof(TEnum))
                .Cast<TEnum>()
                //.Where(e => flagsEnumValue.HasFlag(e))
                .Where(v => !Equals((int)(object)v, 0) && flagsEnumValue.HasFlag(v))
                .ToArray();
        }

        public static string ToIncludeString<TEnum>(this TEnum flags) where TEnum : Enum
        {
            var dd = flags.GetNonEmptyFlags();

            return string.Join(",", dd);
        }

        public static IEnumerable<T> GetUniqueFlags<T>(this T flags) where T : Enum
        {
            ulong flag = 1;
            foreach (var value in Enum.GetValues(flags.GetType()).Cast<T>())
            {
                ulong bits = Convert.ToUInt64(value);
                while (flag < bits)
                {
                    flag <<= 1;
                }

                if (flag == bits && flags.HasFlag(value as Enum))
                {
                    yield return value;
                }
            }
        }
    }
}
