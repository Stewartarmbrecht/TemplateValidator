namespace TemplateValidator
{
    internal class TemplateEvaluator
    {
        Factory factory;
        LinePairEvaluator linePairEvaluator;
        internal TemplateEvaluator(Factory factory)
        {
            this.factory = factory;
            linePairEvaluator = factory.CreateLinePairEvaluator();
        }

        internal TemplateEvaluationResult Evaluate(string target, string template)
        {
            TemplateEvaluationResult result = new TemplateEvaluationResult();
            var linePairIterator = factory.CreateLinePairIterator(target, template);

            LinePair linePair = null;
            TemplateLineEvaluationResult lastTemplateLineEvaluationResult = null;
            while(linePairIterator.TryGetNext(out linePair, lastTemplateLineEvaluationResult))
            {
                var linePairEvaluationResult = linePairEvaluator.EvaluateLinePair(linePair);
                result.Add(linePairEvaluationResult);
                lastTemplateLineEvaluationResult = linePairEvaluationResult.TemplateLineEvaluationResult;
            }
            return result;
        }
    }
}
