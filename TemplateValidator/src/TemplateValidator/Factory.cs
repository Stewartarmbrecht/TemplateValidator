namespace TemplateValidator
{
    internal class Factory
    {
        internal LinePairIterator CreateLinePairIterator(string target, string template)
        {
            var targetIterator = new TargetLineIterator(target);
            var templateIterator = new TemplateLineIterator(template);
            return new LinePairIterator(templateIterator, targetIterator, this);
        }

        internal TemplateEvaluator CreateTemplateEvaluator()
        {
            return new TemplateEvaluator(this);
        }

        internal LinePairEvaluator CreateLinePairEvaluator()
        {
            return new LinePairEvaluator(this);
        }

        internal TemplateLineEvaluator CreateTemplateLineEvaluator()
        {
            return new TemplateLineEvaluator();
        }

        internal LinePair CreateLinePair(Line targetLine, TemplateLine templateLine)
        {
            return new LinePair(targetLine, templateLine);
        }
    }
}
