using System.Globalization;

namespace Pattern.Core.Strategy;

public class TitleCaseFormatter : ITextFormatter
{
    public string Format(string text) =>
        CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
}
