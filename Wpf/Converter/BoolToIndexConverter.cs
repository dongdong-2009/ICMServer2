using System;
using System.Globalization;
using System.Windows.Data;

namespace ICMServer.WPF.Converter
{
    public sealed class BoolToIndexConverter : BooleanConverter<int>
    {
        public BoolToIndexConverter() : base(0, 1)
        { }
    }
}
