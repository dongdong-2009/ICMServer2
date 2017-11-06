using System;
using System.Windows.Data;

namespace ICMServer.WPF.Converter
{
    class CallStatusToStringConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    string key = null;
                    switch ((string)value)
                    {
                        case "start":
                            key = "CallStart";
                            break;

                        case "end":
                            key = "CallEnd";
                            break;

                        case "noack":
                            key = "NoAck";
                            break;
                    }
                    if (key != null)
                    {
                        return CulturesHelper.GetTextValue(key); 
                    }
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
