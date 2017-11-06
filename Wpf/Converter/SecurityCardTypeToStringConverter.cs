using ICMServer.Models;
using System;
using System.Windows.Data;

namespace ICMServer.WPF.Converter
{
    class SecurityCardTypeToStringConverter : EnumToStringConverter<SecurityCardType>
    {
        public SecurityCardTypeToStringConverter() : base("None") { }
    }
}
