using System.Globalization;

namespace Slugrace.Converters;

internal class ZeroToEmptyStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        int enteredNumber = int.Parse(value.ToString());

        if (enteredNumber == 0 )
        {
            return string.Empty;
        }
        else
        {
            return enteredNumber.ToString();
        }          
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string enteredValue = value.ToString();

        if (string.IsNullOrEmpty(enteredValue))
        {
            return 0;
        }
        else
        {
            bool isNumber = int.TryParse(enteredValue, out int number);

            if (isNumber)
            {
                return number;
            }
            else
            {
                enteredValue = enteredValue[..^1];
                return enteredValue.Length == 0 ? 0 : int.Parse(enteredValue);
            }            
        }
    }
}
