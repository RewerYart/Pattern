namespace Pattern.Strategy;

class LowerCaseFormatter : ITextFormatter
{
    public string Format(string text) => text.ToLower();
}
