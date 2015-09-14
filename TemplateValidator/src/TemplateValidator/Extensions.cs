namespace TemplateValidator
{
    public static class Extensions
    {
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
