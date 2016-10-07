using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GUI.Converters
{
    public class ReplacedTraitsToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value.ToString();

            if (str.Length == 0)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}