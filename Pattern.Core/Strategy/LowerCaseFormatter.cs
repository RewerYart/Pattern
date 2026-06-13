namespace Pattern.Core.Strategy;

public class LowerCaseFormatter : ITextFormatter
{
    public string Format(string text) => text.ToLower();
}
