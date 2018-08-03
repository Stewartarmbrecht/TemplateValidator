using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateValidator
{
    /// <summary>
    /// Exception thrown when a template validation fails.
    /// </summary>
    public class TemplateValidationException : Exception
    {
        internal TemplateValidationException(TemplateEvaluationResult result)
        {
            StringBuilder sb = new StringBuilder();
            Misses = result.GetMisses();
        }
        /// <summary>
        /// List of line evaluation pairs that did not match.
        /// </summary>
        /// <value>List of <see cref="TemplateValidator.LinePairEvaluationResult"/></value>
        public List<LinePairEvaluationResult> Misses { get; private set; }

        string message;
        
        /// <summary>
        /// Message for the exception that highlights the miss matches.
        /// </summary>
        /// <value></value>
        public override string Message
        {
            get
            {
                if (message == null)
                    message = FormatMessage();
                return message;
            }
        }

        private string FormatMessage()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var miss in Misses)
            {
                switch (miss.Outcome)
                {
                    case LinePairEvaluationOutcome.Miss:
                        sb.AppendLine("Line " + miss.TargetLine.LineNumber + " did not match template line " + miss.TemplateLine.LineNumber + " (Value/Template)");
                        sb.AppendLine("\t" + miss.TargetLine.LineValue);
                        sb.AppendLine("\t" + miss.TemplateLine.LineValue);
                        break;
                    case LinePairEvaluationOutcome.MissNoTemplate:
                        sb.AppendLine("Extra target line at line number " + miss.TargetLine.LineNumber + " (Target)");
                        sb.AppendLine("\t" + miss.TargetLine.LineValue);
                        break;
                    case LinePairEvaluationOutcome.MissNoTarget:
                        sb.AppendLine("Extra template line at line number " + miss.TemplateLine.LineNumber + " (Template)");
                        sb.AppendLine("\t" + miss.TemplateLine.LineValue);
                        break;
                    default:
                        break;
                }
            }
            return sb.ToString();
        }
    }
}
