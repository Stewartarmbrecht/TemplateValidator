namespace TemplateValidator
{
    /// <summary>
    /// The potential outcomes for a paired or unpaired line in a target or template.
    /// </summary>
    public enum LinePairEvaluationOutcome
    {
        ///<summary>The lines matched.</summary>
        Match,
        ///<summary>The lines did not matched.</summary>
        Miss,
        ///<summary>The target line did not pair to a line in the template.</summary>
        MissNoTemplate,
        ///<summary>The template line did not pair to a line in the target.</summary>
        MissNoTarget
    }
}
