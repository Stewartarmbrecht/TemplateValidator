using System.Collections.Generic;
using System.Linq;

namespace TemplateValidator
{
    internal class TemplateEvaluationResult
    {
        internal TemplateEvaluationResult()
        {
            results = new List<LinePairEvaluationResult>();
        }
        List<LinePairEvaluationResult> results;
        internal void Add(LinePairEvaluationResult linePairEvaluationResult)
        {
            results.Add(linePairEvaluationResult);
        }

        internal List<LinePairEvaluationResult> GetMisses()
        {
            return results
                .Where(x => IsMiss(x))
                .ToList();
        }

        static bool IsMiss(LinePairEvaluationResult x)
        {
            return x.Outcome == LinePairEvaluationOutcome.Miss ||
                   x.Outcome == LinePairEvaluationOutcome.MissNoTarget ||
                   x.Outcome == LinePairEvaluationOutcome.MissNoTemplate;
        }

        internal bool Success
        {
            get
            {
                return results
                    .Where(x => IsMiss(x))
                    .Count() == 0;
            }
        }
    }
}
