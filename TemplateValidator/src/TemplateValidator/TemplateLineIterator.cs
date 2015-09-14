using System.Text.RegularExpressions;

namespace TemplateValidator
{
    internal class TemplateLineIterator
    {
        int index = -1;
        string[] templateLines;
        TemplateLine lastLine;
        internal TemplateLineIterator(string template)
        {
            templateLines = Regex.Split(template, "\r\n|\r|\n");
        }

        internal bool TryGetNext(out TemplateLine line, TemplateLineEvaluationResult lastResult)
        {
            bool result = false;
            line = null;
            if(lastLine != null && lastLine.HasRepeatFlag && lastResult != null)
            {
                if(lastResult.EscapeRepeat == true)
                {
                    index++;
                    line = GetLineForIndex();
                }
                else
                {
                    line = lastLine;
                }
            }
            else
            {
                index++;
                line = GetLineForIndex();
            }
            if (line != null)
                result = true;
            return result;
        }

        TemplateLine GetLineForIndex()
        {
            if (index < templateLines.Length)
            {
                string nextLineValue = null;
                if ((index + 1) < templateLines.Length)
                    nextLineValue = templateLines[index + 1];

                TemplateLine line = new TemplateLine(index + 1, templateLines[index], nextLineValue);
                lastLine = line;
                return line;
            }
            else
                lastLine = null;
                return null;
        }
    }
}
