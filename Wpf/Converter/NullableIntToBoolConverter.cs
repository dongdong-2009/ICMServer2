using System;
using System.Globalization;
using System.Windows.Data;

namespace ICMServer.WPF.Converter
{
    public class NullableIntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                int intValue = (int)value;
                if (intValue != 0)
                    return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
