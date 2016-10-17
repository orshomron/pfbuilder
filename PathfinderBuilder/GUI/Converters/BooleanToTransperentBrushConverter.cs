using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GUI.Converters
{

    [ValueConversion(typeof(bool), typeof(Brush))]
    public class BooleanToTransperentBrushConverter : IValueConverter
    {
        private static readonly Brush ValidBrush = new SolidColorBrush(Colors.Transparent);
        private static readonly Brush InvalidBrush = new SolidColorBrush(Color.FromArgb(170, 220, 90, 90));

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (bool)value;

            if (val)
            {
                return ValidBrush;
            }
            return InvalidBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
