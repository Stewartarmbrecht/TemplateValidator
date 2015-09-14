using System.Text.RegularExpressions;

namespace TemplateValidator
{
    internal class TargetLineIterator
    {
        int index = -1;
        string[] targetLines;

        internal int Count
        {
            get
            {
                return targetLines.Length;
            }
        }

        internal TargetLineIterator(string target)
        {
            targetLines = Regex.Split(target, "\r\n|\r|\n");
        }

        internal bool TryGetNext(out Line line)
        {
            var result = false;
            index++;
            line = null;
            string nextLine = null;
            if ((index + 1) < targetLines.Length)
                nextLine = targetLines[index + 1];
            if(index < targetLines.Length)
            {
                line = new Line(index + 1, targetLines[index], nextLine);
                result = true;
            }
            return result;
        }
    }
}
