using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ICMServer.WPF.Converter
{
    class DeviceTypeToStringConverter : EnumToStringConverter<DeviceType> { }
    class MessageTypeToStringConverter : EnumToStringConverter<MessageType> { }
    class OnlineStatusToStringConverter : EnumToStringConverter<OnlineStatus> { }
    class UpgradeFileTypeToStringConverter : EnumToStringConverter<UpgradeFileType> { }
    class UpgradeStatusToStringConverter : EnumToStringConverter<UpgradeStatus> { }
    class OpenDoorResultToStringConverter : EnumToStringConverter<OpenDoorResult> { }

    public class EnumToStringConverter<T> : IValueConverter
    {
        private readonly string _nullKey;

        public EnumToStringConverter(string nullKey = null)
        {
            _nullKey = nullKey;
        }

        public virtual object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                string key = _nullKey;
                if (value != null)
                {
                    T enumValue = (T)value;
                    key = enumValue.ToString();
                }
                return CulturesHelper.GetTextValue(key);
            }
            catch (Exception)
            {
                return Binding.DoNothing;
            }
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
