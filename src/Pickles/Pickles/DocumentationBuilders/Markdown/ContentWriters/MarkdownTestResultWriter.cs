using System.IO;
using PicklesDoc.Pickles.DocumentationBuilders.Markdown.Formatters;
using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.TestFrameworks;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.ContentWriters
{
    public class MarkdownTestResultWriter
    {
        private readonly Configuration configuration;
        private readonly ITestResults results;
        private readonly IMarkdownFormatter formatter;

        public MarkdownTestResultWriter(Configuration configuration, ITestResults results, IMarkdownFormatter formatter)
        {
            this.configuration = configuration;
            this.results = results;
            this.formatter = formatter;
        }

        public void Write(StreamWriter writer, Scenario scenario)
        {
            if (!this.configuration.HasTestResults)
            {
                return;
            }

            TestResult scenarioResult = this.results.GetScenarioResult(scenario);
            this.WriteTestResult(writer, scenarioResult);
        }

        public void Write(StreamWriter writer, ScenarioOutline scenarioOutline)
        {
            if (!this.configuration.HasTestResults)
            {
                return;
            }

            TestResult scenarioResult = this.results.GetScenarioOutlineResult(scenarioOutline);
            this.WriteTestResult(writer, scenarioResult);
        }

        private void WriteTestResult(StreamWriter writer, TestResult scenarioResult)
        {
            if (!scenarioResult.WasExecuted)
            {
                string notRun = this.formatter.FormatBold("not run");
                writer.WriteLine($"Test: {notRun}");
            }

            if (scenarioResult.WasSuccessful)
            {
                string ok = this.formatter.FormatBold("ok");
                writer.WriteLine($"Test: {ok}");
            }
        }
    }
}