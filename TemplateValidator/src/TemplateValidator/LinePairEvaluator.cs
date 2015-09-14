namespace TemplateValidator
{
    internal class LinePairEvaluator
    {
        TemplateLineEvaluator templateLineEvaluator;

        internal LinePairEvaluator(Factory factory)
        {
            this.templateLineEvaluator = factory.CreateTemplateLineEvaluator();
        }
        internal LinePairEvaluationResult EvaluateLinePair(LinePair linePair)
        {
            LinePairEvaluationResult result = new LinePairEvaluationResult();
            if (linePair.TargetLine == null)
                result.Outcome = LinePairEvaluationOutcome.MissNoTarget;
            if (linePair.TemplateLine == null)
                result.Outcome = LinePairEvaluationOutcome.MissNoTemplate;
            if (result.Outcome == LinePairEvaluationOutcome.Match)
            {
                result.TemplateLineEvaluationResult = templateLineEvaluator.EvaluateLinePair(linePair);
                if (result.TemplateLineEvaluationResult.Match == false)
                    result.Outcome = LinePairEvaluationOutcome.Miss;
            }

            result.TargetLine = linePair.TargetLine;
            result.TemplateLine = linePair.TemplateLine;
            return result;
        }
    }
}
