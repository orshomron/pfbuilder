using System;
using System.Globalization;
using System.Windows.Data;

namespace GUI.Converters
{
    [ValueConversion(typeof(byte), typeof(string))]
    public class AbilityModifierConverterToInt : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int val;
            if (value is byte)
            {
                val = (byte)value;

            }
            else if (value is int)
            {
                val = (int)value;
            }
            else if (value is string)
            {
                val = int.Parse(value.ToString());
            }
            else
            {
                throw new ArgumentException("Value provided must be a Byte, recieved: " + value.GetType(), "value");
            }
            var modifier = (int)Math.Floor((val - 10) / 2.0);
            return modifier;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(byte), typeof(string))]
    public class AbilityModifierConverter : IValueConverter
    {
        private readonly AbilityModifierConverterToInt _internalConverter = new AbilityModifierConverterToInt();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var modifier = (int)_internalConverter.Convert(value, targetType, parameter, culture);

            if (modifier < 0)
            {
                return modifier.ToString();
            }
            return "+" + modifier;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
