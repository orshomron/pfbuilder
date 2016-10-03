using System;
using System.Globalization;
using System.Windows.Data;
using PathfinderBuilder;

namespace GUI.Converters
{
    class RaceToRaceNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var race = value as Race;
            return race.RaceName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
