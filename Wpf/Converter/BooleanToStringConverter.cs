namespace ICMServer.WPF.Converter
{
    public sealed class BooleanToStringConverter : BooleanConverter<string>
    {
        public BooleanToStringConverter() : base("True", "False")
        { }
    }
}
