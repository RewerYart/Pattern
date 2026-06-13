namespace Pattern.Strategy;

class TextEditor(ITextFormatter formatter)
{
    private ITextFormatter _formatter = formatter;

    public void SetFormatter(ITextFormatter formatter) => _formatter = formatter;
    public string FormatText(string text) => _formatter.Format(text);
}
