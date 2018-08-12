namespace TemplateValidator
{
    /// <summary>
    /// Contains the extension method for comparing templates.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Evaluates the target string against a template.<br/>
        /// For a detailed overview of the template validator with examples on how to use it see 
        /// <a href="https://github.com/Stewartarmbrecht/TemplateValidator">this github site: https://github.com/Stewartarmbrecht/TemplateValidator</a>.
        /// </summary>
        /// <param name="target">The string or multiline string to validate.</param>
        /// <param name="template">The template to use to validate the string.</param>
        /// <example>
        /// <br/>Example: Simple validation using a regular expression
        /// <code>
        ///     var target = "My name is Stewart";
        ///     var template = "{{My name is .*}}";
        ///     target.ValidateToTemplate(template);
        /// </code>
        /// <br/>Regular Expressions: Use double braces around the entire line to treat the line as a regular expressions. eg. <br/>
        /// <code>
        /// {{My string .*}}
        /// </code>
        /// <br/>
        /// Multiline Regular Expressions: Use double braces followed by <br/>
        /// <code>
        /// /rl
        /// </code>
        /// <br/>
        /// to repeat the comparison on a variable number of lines.
        /// <br/>
        /// <code>
        /// {{.*}}/rl
        /// </code>
        /// </example>
        /// <exception cref="TemplateValidator.TemplateValidationException">
        /// Thrown when the target does not validate against the provided template.
        /// </exception>
        public static void ValidateToTemplate(this string target, string template)
        {
            var factory = new TemplateValidator.Factory();
            var eval = factory.CreateTemplateEvaluator();
            var results = eval.Evaluate(target, template);
            if(!results.Success)
            {
                throw new TemplateValidator.TemplateValidationException(results);
            }
        }
    }
}
