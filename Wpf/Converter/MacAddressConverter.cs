using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace ICMServer.WPF.Converter
{
    public class MacAddressConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value as string))
                return Binding.DoNothing;
            try
            {
                string longAddr = "";
                string shortAddr = (string)value;
                List<string> parts = new List<string>();
                for (int i = 0; i < 12; i += 2)
                    parts.Add(shortAddr.Substring(i, 2));
                longAddr = string.Join(":", parts.ToArray());
                return longAddr;
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
