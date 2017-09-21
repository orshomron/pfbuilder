using System;
using System.Globalization;
using System.Windows.Data;

namespace GUI.Converters
{
    public class CheckedToEnabledConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
            {
                throw new InvalidOperationException("CheckedToEnabled must contain the following two values: enum value and number of total ");
            }

            var areSame = System.Convert.ToBoolean(values[0]);
            var leftValue = System.Convert.ToInt32(values[1]);

            if (areSame)
            {
                return true;
            }

            return leftValue > 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
