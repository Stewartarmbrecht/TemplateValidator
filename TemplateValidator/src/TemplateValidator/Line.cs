namespace TemplateValidator
{
    public class Line
    {
        internal Line(int lineNumber, string lineValue, string nextLineValue)
        {
            LineNumber = lineNumber;
            LineValue = lineValue;
            NextLineValue = nextLineValue;
        }
        public int LineNumber { get; private set; }
        public string LineValue { get; internal set; }

        public string NextLineValue { get; private set; }
    }
}
