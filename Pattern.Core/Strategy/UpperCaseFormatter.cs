namespace Pattern.Core.Strategy;

public class UpperCaseFormatter : ITextFormatter
{
    public string Format(string text) => text.ToUpper();
}
