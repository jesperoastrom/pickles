using System.IO;
using PicklesDoc.Pickles.DocumentationBuilders.Markdown.Formatters;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.ContentWriters
{
    public class MarkdownScenarioOutlineWriter
    {
        private readonly MarkdownFeatureTagsWriter tagsWriter;
        private readonly MarkdownDescriptionWriter descriptionWriter;
        private readonly MarkdownTestResultWriter testResultWriter;
        private readonly MarkdownStepWriter stepWriter;
        private readonly MarkdownExampleWriter exampleWriter;
        private readonly IMarkdownFormatter formatter;

        public MarkdownScenarioOutlineWriter(
            MarkdownFeatureTagsWriter tagsWriter,
            MarkdownDescriptionWriter descriptionWriter,
            MarkdownTestResultWriter testResultWriter,
            MarkdownStepWriter stepWriter, 
            MarkdownExampleWriter exampleWriter,
            IMarkdownFormatter formatter)
        {
            this.tagsWriter = tagsWriter;
            this.descriptionWriter = descriptionWriter;
            this.testResultWriter = testResultWriter;
            this.stepWriter = stepWriter;
            this.exampleWriter = exampleWriter;
            this.formatter = formatter;
        }

        public void Write(StreamWriter writer, ScenarioOutline scenarioOutline)
        {
            string header = this.formatter.FormatH3(scenarioOutline.Name);
            writer.WriteLine(header);

            this.tagsWriter.Write(writer, scenarioOutline.Tags);
            this.descriptionWriter.Write(writer, scenarioOutline.Description);
            this.testResultWriter.Write(writer, scenarioOutline);

            foreach (Step step in scenarioOutline.Steps)
            {
                this.stepWriter.Write(writer, step);
            }

            foreach (var example in scenarioOutline.Examples)
            {
                this.exampleWriter.Write(writer, example);
            }
        }
    }
}