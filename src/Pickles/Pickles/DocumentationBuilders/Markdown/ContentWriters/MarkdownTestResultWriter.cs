using System.IO;
using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.TestFrameworks;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.ContentWriters
{
    public class MarkdownTestResultWriter
    {
        private readonly Configuration configuration;
        private readonly ITestResults results;

        public MarkdownTestResultWriter(Configuration configuration, ITestResults results)
        {
            this.configuration = configuration;
            this.results = results;
        }

        public void Write(StreamWriter writer, Scenario scenario)
        {
            if (this.configuration.HasTestResults)
            {
                TestResult scenarioResult = this.results.GetScenarioResult(scenario);
                if (scenarioResult.WasExecuted == false)
                {
                    writer.WriteLine("Test: __not run__");
                }
                if (scenarioResult.WasSuccessful)
                {
                    writer.WriteLine("Test: ##ok##");
                }
            }
        }
    }
}