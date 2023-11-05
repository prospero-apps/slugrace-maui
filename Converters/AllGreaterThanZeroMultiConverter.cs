using System.Globalization;

namespace Slugrace.Converters
{
    class AllGreaterThanZeroMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || !targetType.IsAssignableFrom(typeof(bool)))
            {
                return false;
            }

            foreach (object value in values)
            {                
                if (value is not (double or int))
                {
                    return false;
                }
                else if (value is int i)
                {
                    if (i <= 0)
                    { 
                        return false;
                    }
                }
                else if (value is double d)
                {
                    if (d <= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
