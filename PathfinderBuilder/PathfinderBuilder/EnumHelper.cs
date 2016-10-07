using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PathfinderBuilder
{
    public static class EnumHelper
    {
        public static string GetDescription(Type enumType, object enumValue)
        {
            var descriptionAttribute = enumType
              .GetField(enumValue.ToString())
              .GetCustomAttributes(typeof(DisplayAttribute), false)
              .FirstOrDefault() as DisplayAttribute;


            return descriptionAttribute != null
              ? descriptionAttribute.ShortName
              : enumValue.ToString();
        }

        public static IEnumerable<object> GetFlags(this ulong input, Type enumType)
        {
            return Enum.GetValues(enumType).Cast<ulong>().Where(u => (u & input) != 0).Cast<object>();
        }
    }
}
