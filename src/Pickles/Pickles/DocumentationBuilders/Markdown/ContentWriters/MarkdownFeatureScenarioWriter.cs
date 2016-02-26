using System.IO;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.ContentWriters
{
    public class MarkdownFeatureScenarioWriter
    {
        private readonly MarkdownFeatureTagsWriter tagsWriter;
        private readonly MarkdownDescriptionWriter descriptionWriter;
        private readonly MarkdownTestResultWriter testResultWriter;
        private readonly MarkdownStepWriter stepWriter;

        public MarkdownFeatureScenarioWriter(
            MarkdownFeatureTagsWriter tagsWriter,
            MarkdownDescriptionWriter descriptionWriter,
            MarkdownTestResultWriter testResultWriter,
            MarkdownStepWriter stepWriter)
        {
            this.tagsWriter = tagsWriter;
            this.descriptionWriter = descriptionWriter;
            this.testResultWriter = testResultWriter;
            this.stepWriter = stepWriter;
        }

        public void Write(StreamWriter writer, Scenario scenario)
        {
            writer.WriteLine($"### {scenario.Name}");
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