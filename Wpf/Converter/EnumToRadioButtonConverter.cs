using ICMServer.Models;
using ICMServer.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ICMServer.WPF.Converter
{
    class CloudSolutionToRadioButtonConverter : EnumToRadioButtonConverter<CloudSolution> { }
    class DatabaseBackupOperationToRadioButtonConverter : EnumToRadioButtonConverter<DatabaseBackupOperation> { }
    class ExportAddressBookOperationToRadioButtonConverter : EnumToRadioButtonConverter<ExportAddressBookOperation> { }
    class ExportCardListOperationToRadioButtonConverter : EnumToRadioButtonConverter<ExportCardListOperation> { }
    class MessageTypeToRadioButtonConverter : EnumToRadioButtonConverter<MessageType> { }
    class ProcessStateToRadioButtonConverter : EnumToRadioButtonConverter<ProcessState> { }
    class SelectDeviceOptionToRadioButtonConverter : EnumToRadioButtonConverter<SelectDeviceOption> { }
    class SexToRadioButtonConverter : EnumToRadioButtonConverter<Sex> { }

    public class EnumToRadioButtonConverter<T> : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                bool result = false;

                if (value != null && parameter != null)
                {
                    string checkValue = value.ToString();
                    string targetValue = parameter.ToString();
                    result = checkValue.Equals(targetValue,
                        StringComparison.InvariantCultureIgnoreCase);
                }
                else if (parameter == null)
                {
                    result = true;
                }

                return result;
            }
            catch (Exception)
            {
                return Binding.DoNothing;
            }
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && (bool)value)
            {
                if (parameter != null)
                {
                    string targetValue = parameter.ToString();
                    return Enum.Parse(typeof(T), targetValue);
                }
                else
                {
                    return null;
                }
            }
            return Binding.DoNothing;
        }
    }
}
