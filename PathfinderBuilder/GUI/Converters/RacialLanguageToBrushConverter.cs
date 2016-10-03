using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GUI.Converters
{

    [ValueConversion(typeof(bool), typeof(Brush))]
    public class RacialLanguageToBrushConverter : IValueConverter
    {
        private static readonly Brush RacialBrush = new SolidColorBrush(Colors.LightGreen);
        private static readonly Brush NormalBrush = new SolidColorBrush(Colors.Transparent);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isRacial = (bool)value;

            if (isRacial)
            {
                return RacialBrush;
            }

            return NormalBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
