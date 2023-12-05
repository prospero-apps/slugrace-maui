using Slugrace.ViewModels;
using System.Globalization;

namespace Slugrace.Converters;

internal class SelectedSlugToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if ((value as SlugViewModel) == null)
        {
            return false;
        }
        else
        {
            return (value as SlugViewModel).Name == parameter.ToString();
        }        
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
