using Microsoft.Framework.DependencyInjection;
using Xunit;
using System.IO;
using System.Text.RegularExpressions;
using TemplateValidator;
using System;
using Microsoft.Dnx.Runtime.Infrastructure;
using Microsoft.Dnx.Runtime;

namespace xBDD.Test.Features.Helpers
{
    public class ValidateWithTemplate
    {
        private readonly string basePath;

        public ValidateWithTemplate()
        {
            var provider = CallContextServiceLocator.Locator.ServiceProvider;
            var appEnv = provider.GetRequiredService<IApplicationEnvironment>();

            basePath = appEnv.ApplicationBasePath + "\\TestFiles\\";
        }
        void RunScenario(string scenarioName)
        {
            var text = File.ReadAllText(basePath + scenarioName + ".txt");
            string[] artifacts = Regex.Split(text, "\r\n----------\r\n");
            string target = artifacts[0];
            string template = artifacts[1];
            string exceptionMessage = null;
            TemplateValidationException tve = null;
            if (artifacts.Length > 2)
                exceptionMessage = artifacts[2];
            //target.ValidateToTemplate(template);
            try
            {
                target.ValidateToTemplate(template);
            }
            catch (TemplateValidationException ex)
            {
                tve = ex;
            }

            if (exceptionMessage != null)
            {
                if (tve == null)
                    throw new Exception("An exception was not thrown when one was expected.");
                Assert.Equal(exceptionMessage, tve.Message);
            }
        }
        [Fact]
        public void SingleLineMatching()
        {
            RunScenario("SingleLineMatching");
        }
        [Fact]
        public void SingleLineRegexMatching()
        {
            RunScenario("SingleLineRegexMatching");
        }
        [Fact]
        public void SingleLineNonmatching()
        {
            RunScenario("SingleLineNonmatching");
        }

        [Fact]
        public void SingleLineRegexNonmatching()
        {
            RunScenario("SingleLineRegexNonmatching");
        }

        [Fact]
        public void SingleLineWithRegexCharactersMatching()
        {
            RunScenario("SingleLineWithRegexCharactersMatching");
        }
        [Fact]
        public void SingleLineWithRegexCharactersNonmatching()
        {
            RunScenario("SingleLineWithRegexCharactersNonmatching");
        }

        [Fact]
        public void MultilineMatching()
        {
            RunScenario("MultilineMatching");
        }
        [Fact]
        public void MultilineWithEmptyLineMatching()
        {
            RunScenario("MultilineWithEmptyLineMatching");
        }
        [Fact]
        public void MultilineSingleLastLineNonmatching()
        {
            RunScenario("MultilineSingleLastLineNonmatching");
        }

        [Fact]
        public void MultilineSingleFirstLineNonmatching()
        {
            RunScenario("MultilineSingleFirstLineNonmatching");
        }

        [Fact]
        public void MultilineSingleMiddleLineNonmatching()
        {
            RunScenario("MultilineSingleMiddleLineNonmatching");
        }

        [Fact]
        public void MultilineMultipleLineNonmatching()
        {
            RunScenario("MultilineMultipleLineNonmatching");
        }

        [Fact]
        public void MultilineSingleRegexLineMatching()
        {
            RunScenario("MultilineSingleRegexLineMatching");
        }

        [Fact]
        public void MultilineSingleRegexLineNonmatching()
        {
            RunScenario("MultilineSingleRegexLineNonmatching");
        }

        [Fact]
        public void MultilineSingleRegexRepeatingLineMatching()
        {
            RunScenario("MultilineSingleRegexRepeatingLineMatching");
        }

        [Fact]
        public void MultilineSingleLastRegexRepeatingLineMatching()
        {
            RunScenario("MultilineSingleLastRegexRepeatingLineMatching");
        }

        [Fact]
        public void MultilineSingleRegexRepeatingLineNonmatching()
        {
            RunScenario("MultilineSingleRegexRepeatingLineNonmatching");
        }
        [Fact]
        public void MultilineSingleRegexRepeatingLineRegularLastLineNonmatching()
        {
            RunScenario("MultilineSingleRegexRepeatingLineRegularLastLineNonmatching");
        }
        [Fact]
        public void MultilineSingleLastRegexRepeatingLineFirstRepeatingLineNonmatching()
        {
            RunScenario("MultilineSingleLastRegexRepeatingLineFirstRepeatingLineNonmatching");
        }

    }
}
