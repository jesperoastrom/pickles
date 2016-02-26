using System.IO;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.NodeWriters
{
    public class MarkdownFeatureScenarioOutlineWriter
    {
        private readonly MarkdownFeatureTagsWriter tagsWriter;
        private readonly MarkdownDescriptionWriter descriptionWriter;
        private readonly MarkdownTestResultWriter testResultWriter;
        private readonly MarkdownStepWriter stepWriter;

        public MarkdownFeatureScenarioOutlineWriter(
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

        public void Write(StreamWriter writer, ScenarioOutline scenarioOutline)
        {
            writer.WriteLine($"### {scenarioOutline.Name}");
            this.tagsWriter.Write(writer, scenarioOutline.Tags);
            this.descriptionWriter.Write(writer, scenarioOutline.Description);

            foreach (Step step in scenarioOutline.Steps)
            {
                this.stepWriter.Write(writer, step);
            }
        }
    }
}