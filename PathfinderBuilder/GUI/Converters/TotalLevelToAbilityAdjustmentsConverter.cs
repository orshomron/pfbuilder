using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace GUI.Converters
{
    public class TotalLevelToAbilityAdjustmentsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2)
            {
                throw new InvalidOperationException("Values must contain at least the total level and current attribute bonus in order to work");
            }

            var i = System.Convert.ToInt32(values[0]);
            var current = System.Convert.ToInt32(values[1]);
            var totalUsage = values.Skip(2).Sum(System.Convert.ToInt32);

            return (byte)Math.Max(0, current + Math.Floor(i / 4.0) - totalUsage);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
