namespace TemplateValidator
{
    internal class LinePairIterator
    {
        TemplateLineIterator templateIterator;
        TargetLineIterator targetIterator;
        Factory factory;

        internal LinePairIterator(TemplateLineIterator templateIterator, TargetLineIterator targetIterator, Factory factory)
        {
            this.templateIterator = templateIterator;
            this.targetIterator = targetIterator;
            this.factory = factory;
        }

        internal bool TryGetNext(out LinePair linePair, TemplateLineEvaluationResult lastTemplateLineEvaluationResult)
        {
            bool result = false;
            Line targetLine = null;
            TemplateLine templateLine = null;
            bool targetLineReturned = targetIterator.TryGetNext(out targetLine);
            //If there are no more target lines then escape the current repeating line if there is one 
            //and move through the rest of the template.
            if (targetLineReturned == false && lastTemplateLineEvaluationResult != null)
                lastTemplateLineEvaluationResult.EscapeRepeat = true;
            bool templateLineReturned = templateIterator.TryGetNext(out templateLine, lastTemplateLineEvaluationResult);

            if (targetLineReturned || templateLineReturned)
            {
                result = true;
                linePair = factory.CreateLinePair(targetLine, templateLine);
            }
            else
            {
                linePair = null;
            }
            return result;
        }
    }
}
