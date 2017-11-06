using System;
using System.Windows.Data;

namespace ICMServer.WPF.Converter
{
    class AlarmEventToStringConverter : IValueConverter
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
                        case "enable":
                            key = "Arming";
                            break;

                        case "disable":
                            key = "Disarmed";
                            break;

                        case "trig":
                            key = "EventTrigger";
                            break;

                        case "unalarm":
                            key = "EventLifted";
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
