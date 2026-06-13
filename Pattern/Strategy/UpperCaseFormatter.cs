namespace Pattern.Strategy;

class UpperCaseFormatter : ITextFormatter
{
    public string Format(string text) => text.ToUpper();
}
