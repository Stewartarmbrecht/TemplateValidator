namespace TemplateValidator
{
    public class LinePairEvaluationResult
    {
        public LinePairEvaluationOutcome Outcome { get; internal set; }
        public Line TargetLine { get; internal set; }
        public Line TemplateLine { get; internal set; }
        internal TemplateLineEvaluationResult TemplateLineEvaluationResult { get; set; }
    }
}
