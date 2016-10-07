using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace GUI.Converters
{
    [ValueConversion(typeof(double), typeof(Brush))]
    public class TooLowToBrushConverter : DependencyObject, IValueConverter
    {
        private static readonly Brush ValidBrush = new SolidColorBrush(Colors.White);
        private static readonly Brush InvalidBrush = new SolidColorBrush(Color.FromArgb(120, 255, 0, 0));

        public static readonly DependencyProperty ThresholdProperty =
            DependencyProperty.Register("Threshold", typeof(double), typeof(TooLowToBrushConverter), new PropertyMetadata(default(double)));

        public double Threshold
        {
            get { return (double)GetValue(ThresholdProperty); }
            set { SetValue(ThresholdProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (System.Convert.ToDouble(value) >= Threshold)
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

    [ValueConversion(typeof(double), typeof(Brush))]
    public class TooHighToBrushConverter : DependencyObject, IValueConverter
    {
        private static readonly Brush ValidBrush = new SolidColorBrush(Colors.White);
        private static readonly Brush InvalidBrush = new SolidColorBrush(Color.FromArgb(120, 255, 0, 0));

        public static readonly DependencyProperty ThresholdProperty =
            DependencyProperty.Register("Threshold", typeof(double), typeof(TooHighToBrushConverter), new PropertyMetadata(default(double)));

        public double Threshold
        {
            get { return (double)GetValue(ThresholdProperty); }
            set { SetValue(ThresholdProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (System.Convert.ToDouble(value) <= Threshold)
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
