using System.Text.RegularExpressions;

namespace TemplateValidator
{
    internal class TemplateLineEvaluator
    {
        internal TemplateLineEvaluationResult EvaluateLinePair(LinePair linePair)
        {
            TemplateLineEvaluationResult result = new TemplateLineEvaluationResult();
            if(linePair.TemplateLine.IsRegexPattern)
            {
                if (Regex.IsMatch(linePair.TargetLine.LineValue, linePair.TemplateLine.LineValue))
                    result.Match = true;
            }
            else
            {
                if (linePair.TargetLine.LineValue == linePair.TemplateLine.LineValue)
                    result.Match = true;
            }
            if (linePair.TemplateLine.HasRepeatFlag && linePair.TemplateLine.NextLine != null && linePair.TargetLine.NextLineValue != null)
            {
                if(linePair.TemplateLine.NextLine.IsRegexPattern)
                {
                    if(Regex.IsMatch(linePair.TargetLine.NextLineValue, linePair.TemplateLine.NextLine.LineValue))
                        result.EscapeRepeat = true;
                }
                else
                {
                    if (linePair.TargetLine.NextLineValue == linePair.TemplateLine.NextLine.LineValue)
                        result.EscapeRepeat = true;
                }
            }
            return result;
        }
    }
}
