using System;
using System.Windows.Data;

namespace ICMServer.WPF.Converter
{
    public class DeviceAddressConverter : IValueConverter
    {
        static string[] SeparatorTexts = new string[6];

        private void InitSeparatorTexts()
        {
            SeparatorTexts[0] = CulturesHelper.GetTextValue("DeviceAddressUnit1");
            SeparatorTexts[1] = CulturesHelper.GetTextValue("DeviceAddressUnit2");
            SeparatorTexts[2] = CulturesHelper.GetTextValue("DeviceAddressUnit3");
            SeparatorTexts[3] = CulturesHelper.GetTextValue("DeviceAddressUnit4");
            SeparatorTexts[4] = CulturesHelper.GetTextValue("DeviceAddressUnit5");
            SeparatorTexts[5] = CulturesHelper.GetTextValue("DeviceAddressUnit6");
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                string input = value as string;
                string output = "";
                if (string.IsNullOrWhiteSpace(input))
                {
                    string key = parameter as string;
                    if (!string.IsNullOrWhiteSpace(key))
                        output = CulturesHelper.GetTextValue(key);
                    return output;
                }

                InitSeparatorTexts();

                input = input.Trim().Replace("-", "");
                const int MAX_LENGTH = 12;
                int length = Math.Min(MAX_LENGTH, input.Length);
                for (int i = 0; i < length; )
                {
                    string token = input.Substring(i, Math.Min(2, length - i));
                    output += (token.Length == 2) 
                           ? token + SeparatorTexts[i / 2]
                           : token;
                    i += token.Length;
                }

                return output;
            }
            catch (Exception)
            {
                return Binding.DoNothing;
            }
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                InitSeparatorTexts();

                string shortAddr = "";
                string longAddr = (string)value;
                string[] tokens = longAddr.Split(SeparatorTexts, StringSplitOptions.None);
                int i;
                for (i = tokens.Length - 1; i >= 0; --i)
                {
                    if (tokens[i] != "")
                        break;
                }
                int count = Math.Min(i + 1, 6);
                if (count > 0)
                {
                    string[] parts = new string[count];
                    for (i = 0; i < count; ++i)
                    {
                        parts[i] = (tokens[i] != "") ? tokens[i] : "00" ;
                    }
                    shortAddr = string.Join("-", parts);
                }
                return shortAddr;
            }
            catch (Exception)
            {
                return Binding.DoNothing;
            }
        }
    }
}
