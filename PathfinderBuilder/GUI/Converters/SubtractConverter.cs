using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GUI.Converters
{
    [ValueConversion(typeof(double), typeof(double))]
    public class SubtractConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty SubtractAmountProperty =
            DependencyProperty.Register("Threshold", typeof(double), typeof(SubtractConverter), new PropertyMetadata(default(double)));

        public double SubtractAmount
        {
            get { return (double)GetValue(SubtractAmountProperty); }
            set { SetValue(SubtractAmountProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDouble(value) - SubtractAmount;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
