using System.Globalization;

namespace Slugrace.Converters
{
    class DoubleToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {         
            if (!double.TryParse(parameter as string, out double limit))
            {
                limit = 100;
            }

            return (double)value < limit;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!double.TryParse(parameter as string, out double limit))
            {
                limit = 100;
            }

            return (bool)value ? limit : limit + 1;
        }
    }
}
