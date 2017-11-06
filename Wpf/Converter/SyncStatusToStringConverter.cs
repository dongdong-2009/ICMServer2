using System;
using System.Windows.Data;

namespace ICMServer.WPF.Converter
{
    class SyncStatusToStringConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    int? syncStatus = (int?)value;
                    string key = (syncStatus == null || syncStatus == 0)
                               ? "Unsynced"
                               : "Synced";
                    return CulturesHelper.GetTextValue(key); 
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
