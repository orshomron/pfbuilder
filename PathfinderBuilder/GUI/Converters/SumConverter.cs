using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using ConvertClass = System.Convert;

namespace GUI.Converters
{

    public class SumConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var sum = values.Select(ConvertClass.ToInt32).Sum();
            return sum.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
