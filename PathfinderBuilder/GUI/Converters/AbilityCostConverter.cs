using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace GUI.Converters
{
    [ValueConversion(typeof(byte), typeof(int))]
    public class AbilityCostConverter : IValueConverter
    {
        private static readonly Dictionary<int, int> PointBuyCosts = new Dictionary<int, int>
        {
            {7,-4},
            {8,-2},
            {9,-1},
            {10,0},
            {11,1},
            {12,2},
            {13,3},
            {14,5},
            {15,7},
            {16,10},
            {17,13},
            {18,17},
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte)
            {
                var val = (byte)value;
                return PointBuyCosts.ContainsKey(val) ? PointBuyCosts[val] : (val > 18 ? 30 : -5);
            }

            throw new ArgumentException("Value provided must be an Integer, recieved: " + value.GetType(), "value");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
