namespace TemplateValidator
{
    /// <summary>
    /// Represents a line comparison result.
    /// </summary>
    public class Line
    {
        internal Line(int lineNumber, string lineValue, string nextLineValue)
        {
            LineNumber = lineNumber;
            LineValue = lineValue;
            NextLineValue = nextLineValue;
        }
        /// <summary>
        /// The ordinal position of the line.
        /// </summary>
        /// <value></value>
        public int LineNumber { get; private set; }
        
        /// <summary>
        /// The text for the line.
        /// </summary>
        /// <value></value>
        public string LineValue { get; internal set; }

        /// <summary>
        /// The value of the next line in the source.
        /// </summary>
        /// <value></value>
        public string NextLineValue { get; private set; }
    }
}
