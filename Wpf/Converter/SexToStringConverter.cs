using ICMServer.Models;
using System;
using System.Windows.Data;

namespace ICMServer.WPF.Converter
{
    class SexToStringConverter : EnumToStringConverter<Sex>
    {
        public SexToStringConverter() : base("None") { }
    }
}
