using ICMServer.Models;
using System;
using System.Windows.Data;

namespace ICMServer.WPF.Converter
{
    class ProcessStateToStringConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                ProcessState state = value != null ? (ProcessState)value : ProcessState.Unprocessed;

                string key = state.ToString();
                return CulturesHelper.GetTextValue(key); ;
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
