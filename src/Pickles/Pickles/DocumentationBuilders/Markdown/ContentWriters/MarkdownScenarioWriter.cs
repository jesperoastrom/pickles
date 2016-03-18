using System.IO;
using PicklesDoc.Pickles.DocumentationBuilders.Markdown.Formatters;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.ContentWriters
{
    public class MarkdownScenarioWriter
    {
        private readonly MarkdownFeatureTagsWriter tagsWriter;
        private readonly MarkdownDescriptionWriter descriptionWriter;
        private readonly MarkdownTestResultWriter testResultWriter;
        private readonly MarkdownStepWriter stepWriter;
        private readonly IMarkdownFormatter formatter;

        public MarkdownScenarioWriter(
            MarkdownFeatureTagsWriter tagsWriter,
            MarkdownDescriptionWriter descriptionWriter,
            MarkdownTestResultWriter testResultWriter,
            MarkdownStepWriter stepWriter,
            IMarkdownFormatter formatter)
        {
            this.tagsWriter = tagsWriter;
            this.descriptionWriter = descriptionWriter;
            this.testResultWriter = testResultWriter;
            this.stepWriter = stepWriter;
            this.formatter = formatter;
        }

        public void Write(StreamWriter writer, Scenario scenario)
        {
            string header = this.formatter.FormatH3(scenario.Name);
            writer.WriteLine($"{header}");
            this.tagsWriter.Write(writer, scenario.Tags);
            this.descriptionWriter.Write(writer, scenario.Description);
            this.testResultWriter.Write(writer, scenario);

            foreach (Step step in scenario.Steps)
            {
                this.stepWriter.Write(writer, step);
            }
        }
    }
}