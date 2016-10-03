using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace GUI.Converters
{

    [ValueConversion(typeof(bool[]), typeof(Brush))]
    public class ArchtypeAvailablilityUsageToBrushConverter : IMultiValueConverter
    {
        private static readonly Brush UsedBrush = new SolidColorBrush(Colors.LimeGreen);
        private static readonly Brush NormalBrush = new SolidColorBrush(Colors.Transparent);
        private static readonly Brush UnavailableBrush = new SolidColorBrush(Colors.DimGray);

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var isUsed = (bool)values[0];
            var canBeAdded = (bool)values[1];

            if (isUsed)
            {
                return UsedBrush;
            }
            if (canBeAdded)
            {
                return NormalBrush;
            }
            return UnavailableBrush;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
