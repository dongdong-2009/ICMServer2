using System;
using System.Windows.Data;

namespace ICMServer.WPF.Converter
{
    class DetectorToStringConverter : IValueConverter
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
                        case "emergency":
                            key = "EmergencyDetector";
                            break;

                        case "infrared":
                            key = "InfraredDetector";
                            break;

                        case "door":
                            key = "DoorDetector";
                            break;

                        case "window":
                            key = "WindowDetector";
                            break;

                        case "smoke":
                            key = "SmokeDetector";
                            break;

                        case "gas":
                            key = "GasDetector";
                            break;

                        case "area":
                            key = "AreaDetector";
                            break;

                        case "rob":
                            key = "RobDetector";
                            break;
                    }
                    if (key != null)
                    {
                        return CulturesHelper.GetTextValue(key); ;
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
