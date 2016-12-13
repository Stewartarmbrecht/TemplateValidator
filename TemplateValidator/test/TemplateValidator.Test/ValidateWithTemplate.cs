using System.IO;
using System.Text.RegularExpressions;
using TemplateValidator;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace xBDD.Test.Features.Helpers
{
    [TestClass]
    public class ValidateWithTemplate
    {
        private readonly string basePath;

        public ValidateWithTemplate()
        {
            //var provider = CallContextServiceLocator.Locator.ServiceProvider;
            //var appEnv = provider.GetRequiredService<IApplicationEnvironment>();

            //basePath = appEnv.ApplicationBasePath + "\\TestFiles\\";
            basePath = ".\\TestFiles\\";
        }
        void RunScenario(string text)
        {
            //var text = File.ReadAllText(basePath + scenarioName + ".txt");
            text = text.Substring(2);
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
                Assert.AreEqual(exceptionMessage, tve.Message);
            }
            else
            {
                if(tve != null)
                    throw new Exception($"An exception was thrown when one was not expected.  Message: {tve.Message}");
            }
        }
        [TestMethod]
        public void SingleLineMatching()
        {
            string test = @"
Hello my name is Stewart
----------
Hello my name is Stewart";
            RunScenario(test);
        }
        [TestMethod]
        public void SingleLineRegexMatching()
        {
            string test = @"
Hello my name is Stewart
----------
{{Hello my name is .*}}";
            RunScenario(test);
        }
        [TestMethod]
        public void SingleLineNonmatching()
        {
            string test = @"
Hello my name is not Stewart
----------
Hello my name is Stewart
----------
Line 1 did not match template line 1 (Value/Template)
	Hello my name is not Stewart
	Hello my name is Stewart
";
            RunScenario(test);
        }

        [TestMethod]
        public void SingleLineRegexNonmatching()
        {
            string test = @"
Hello my name is Stewart
----------
{{Hello my name is not .*}}
----------
Line 1 did not match template line 1 (Value/Template)
	Hello my name is Stewart
	Hello my name is not .*
";
            RunScenario(test);
        }

        [TestMethod]
        public void SingleLineWithRegexCharactersMatching()
        {
            string test = @"
Hello my name is ""Stewart""[0-9]+.  What is your name?
----------
Hello my name is ""Stewart""[0-9]+.  What is your name?";
            RunScenario(test);
        }
        [TestMethod]
        public void SingleLineWithRegexCharactersNonmatching()
        {
            string test = @"
Hello my name is ""Stewart""[0-9]+.  What is your name?
----------
Hello my name is ""Stewart""[0-9]+[A-Z].  What is your name?
----------
Line 1 did not match template line 1 (Value/Template)
	Hello my name is ""Stewart""[0-9]+.  What is your name?
	Hello my name is ""Stewart""[0-9]+[A-Z].  What is your name?
";
            RunScenario(test);
        }

        [TestMethod]
        public void MultilineMatching()
        {
            string test = @"
Hello, 
my name is 
Stewart
----------
Hello, 
my name is 
Stewart";
            RunScenario(test);
        }
        [TestMethod]
        public void MultilineWithEmptyLineMatching()
        {
            string test = @"
Hello, 
my name is 

Stewart
----------
Hello, 
my name is 

Stewart";
            RunScenario(test);
        }
        [TestMethod]
        public void MultilineSingleLastLineNonmatching()
        {
            string test = @"
Hello, 
my name is 
not Stewart
----------
Hello, 
my name is 
Stewart
----------
Line 3 did not match template line 3 (Value/Template)
	not Stewart
	Stewart
";
            RunScenario(test);
        }

        [TestMethod]
        public void MultilineSingleFirstLineNonmatching()
        {
            string test = @"
Hello! 
my name is 
Stewart
----------
Hello, 
my name is 
Stewart
----------
Line 1 did not match template line 1 (Value/Template)
	Hello! 
	Hello, 
";
            RunScenario(test);
        }

        [TestMethod]
        public void MultilineSingleMiddleLineNonmatching()
        {
            string test = @"
Hello, 
my name is not 
Stewart
----------
Hello, 
my name is 
Stewart
----------
Line 2 did not match template line 2 (Value/Template)
	my name is not 
	my name is 
";
            RunScenario(test);
        }

        [TestMethod]
        public void MultilineMultipleLineNonmatching()
        {
            string test = @"
Hello! 
my name is not 
Stewart
----------
Hello, 
my name is 
Stewart
----------
Line 1 did not match template line 1 (Value/Template)
	Hello! 
	Hello, 
Line 2 did not match template line 2 (Value/Template)
	my name is not 
	my name is 
";
            RunScenario(test);
        }

        [TestMethod]
        public void MultilineSingleRegexLineMatching()
        {
            string test = @"
Hello, 
my name is not 
Stewart
----------
Hello, 
{{my name is .* }}
Stewart";
            RunScenario(test);
        }

        [TestMethod]
        public void MultilineSingleRegexLineNonmatching()
        {
            string test = @"
Hello, 
my name is not 
Stewart
----------
Hello, 
{{my name is [0-9]* }}
Stewart
----------
Line 2 did not match template line 2 (Value/Template)
	my name is not 
	my name is [0-9]* 
";
            RunScenario(test);
        }

        [TestMethod]
        public void MultilineSingleRegexRepeatingLineMatching()
        {
            string test = @"
Hello, 
my name 
is not 
Stewart
----------
Hello, 
{{.*}}/rl
Stewart";
            RunScenario(test);
        }

        [TestMethod]
        public void MultilineSingleLastRegexRepeatingLineMatching()
        {
            string test = @"
Hello, 
my name 
is not 
at all Stewart
----------
Hello, 
my name 
{{^[a-zA-Z0-9\s]+$}}/rl";
            RunScenario(test);
        }

        [TestMethod]
        public void MultilineSingleRegexRepeatingLineNonmatching()
        {
            string test = @"
Hello, 
my name 
is not 
Stewart
----------
Hello, 
{{^[0-9]+$}}/rl
Stewart
----------
Line 2 did not match template line 2 (Value/Template)
	my name 
	^[0-9]+$
Line 3 did not match template line 2 (Value/Template)
	is not 
	^[0-9]+$
";
            RunScenario(test);
        }
        [TestMethod]
        public void MultilineSingleRegexRepeatingLineRegularLastLineNonmatching()
        {
            string test = @"
Hello, 
my name 
is not 
at all Stewart
----------
Hello, 
{{^[a-zA-Z0-9\s]+$}}/rl
Stewart
----------
Extra template line at line number 3 (Template)
	Stewart
";
            RunScenario(test);
        }
        [TestMethod]
        public void MultilineSingleLastRegexRepeatingLineFirstRepeatingLineNonmatching()
        {
            string test = @"
Hello, 
my name ?
is not 
at all Stewart
----------
Hello, 
{{^[a-zA-Z0-9\s]+$}}/rl
----------
Line 2 did not match template line 2 (Value/Template)
	my name ?
	^[a-zA-Z0-9\s]+$
";
            RunScenario(test);
        }
        [TestMethod]
        public void MultilineWithRepeatingLineWithFirstLineEscaping()
        {
            string test = @"
Hello 
my 
name 
is 
Stewart
----------
{{^[a-zA-Z0-9\s]+$}}/rl
my 
name 
is 
Stewart";
            RunScenario(test);
        }

    }
}
