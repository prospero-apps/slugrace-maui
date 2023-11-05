using System.Globalization;

namespace Slugrace.Converters
{
    public class DoubleToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double granularity;

            if (!double.TryParse(parameter as string, out granularity))
            {
                granularity = 1;
            }

            return (int)((double)value / granularity);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double granularity;

            if (!double.TryParse(parameter as string, out granularity))
            {
                granularity = 1;
            }

            return (double)((int)value * granularity);
        }
    }
}
