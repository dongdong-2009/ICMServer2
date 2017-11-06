using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ICMServer.WPF.Converter
{
    class IntToDeviceTypeConverter : IntToEnumConverter<DeviceType> { }
    class IntToOnlineStatusConverter : IntToEnumConverter<OnlineStatus> { }
    class IntToProcessStateConverter : IntToEnumConverter<ProcessState> { }
    class IntToSexConverter : IntToEnumConverter<Sex> { }

    public class IntToEnumConverter<T> : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    T enumValue = (T)value;
                    return enumValue;
                }
                return null;
            }
            catch (Exception)
            {
                return Binding.DoNothing;
            }
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value == null)
                    return null;
                
                return (int)value;
            }
            catch (Exception)
            {
                return Binding.DoNothing;
            }
        }
    }
}
