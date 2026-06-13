using System.Globalization;

namespace Pattern.Strategy;

class TitleCaseFormatter : ITextFormatter
{
    public string Format(string text) =>
        CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
}
