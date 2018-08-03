namespace TemplateValidator
{
    internal class TemplateLine : Line
    {
        internal TemplateLine(int lineNumber, string lineValue, string nextLineValue)
            : base(lineNumber, lineValue, nextLineValue)
        {
            if (lineValue.StartsWith("{{"))
                IsRegexPattern = true;

            if (lineValue.EndsWith("/rl"))
                HasRepeatFlag = true;

            int startIndex = (IsRegexPattern ? 2 : 0);
            int lengthRemove = (IsRegexPattern ? (HasRepeatFlag ? 7 : 4) : 0);
            LineValue = lineValue.Substring(startIndex, lineValue.Length - lengthRemove);

            if (nextLineValue != null)
                NextLine = new TemplateLine(lineNumber + 1, nextLineValue, null);
        }
        internal TemplateLine NextLine { get; private set; }
        internal bool IsRegexPattern { get; private set; }
        internal bool HasRepeatFlag { get; private set; }
    }
}
