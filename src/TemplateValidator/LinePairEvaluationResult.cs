namespace TemplateValidator
{
    /// <summary>
    /// The result of a comparison of two lines in the target and template at the same position.
    /// </summary>
    public class LinePairEvaluationResult
    {
        /// <summary>
        /// The evaluation outcome.
        /// </summary>
        /// <value></value>
        public LinePairEvaluationOutcome Outcome { get; internal set; }
        /// <summary>
        /// The target line.
        /// </summary>
        /// <value></value>
        public Line TargetLine { get; internal set; }
        /// <summary>
        /// The template line the target line was paired with.
        /// </summary>
        /// <value></value>
        public Line TemplateLine { get; internal set; }
        internal TemplateLineEvaluationResult TemplateLineEvaluationResult { get; set; }
    }
}
