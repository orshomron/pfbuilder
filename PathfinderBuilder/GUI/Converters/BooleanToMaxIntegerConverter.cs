using System;
using System.Globalization;
using System.Windows.Data;

namespace GUI.Converters
{
    public class BooleanToMaxIntegerConverter : IValueConverter
    {
        public int FalseValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = (bool)value;

            return b ? parameter : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
