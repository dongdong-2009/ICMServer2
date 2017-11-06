using System;
using System.Windows.Data;

namespace ICMServer.WPF.Converter
{
    class StringResourceIDToStringConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                string key = value as string;
                if (key != null)
                {
                    string result = CulturesHelper.GetTextValue(key);
                    if (string.IsNullOrEmpty(result))
                        return key;
                    return result;
                }
                return "";
            }
            catch (Exception)
            {
                return Binding.DoNothing;
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
