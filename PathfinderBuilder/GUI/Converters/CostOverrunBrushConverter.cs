using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using PathfinderBuilder;

namespace GUI.Converters
{

    [ValueConversion(typeof(object[]), typeof(Brush))]
    public class CostOverrunBrushConverter : IMultiValueConverter
    {
        private static readonly Brush ValidBrush = new SolidColorBrush(Colors.White);
        private static readonly Brush InvalidBrush = new SolidColorBrush(Color.FromArgb(120, 255, 0, 0));

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
            {
                throw new ArgumentException("values must contain exactly the current total cost and the point buy option selected", "values");
            }

            int val = int.Parse(values[0].ToString());
            var option = values[1] is PointBuyOptions ? (PointBuyOptions)values[1] : PointBuyOptions.Other;

            if (val == int.MinValue)
            {
                throw new ArgumentException("Value provided must be an Integer, recieved: " + values[0].GetType(), "value");
            }

            if (val <= (int)option)
            {
                return ValidBrush;
            }
            return InvalidBrush;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
