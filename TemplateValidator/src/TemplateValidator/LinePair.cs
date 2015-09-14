namespace TemplateValidator
{
    internal class LinePair
    {
        internal LinePair(Line targetLine, TemplateLine templateLine)
        {
            TargetLine = targetLine;
            TemplateLine = templateLine;
        }

        internal Line TargetLine { get; private set; }
        internal TemplateLine TemplateLine { get; private set; }
    }
}
