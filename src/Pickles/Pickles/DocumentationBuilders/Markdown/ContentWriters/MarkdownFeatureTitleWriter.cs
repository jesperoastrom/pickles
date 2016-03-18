using System.IO;
using PicklesDoc.Pickles.DocumentationBuilders.Markdown.Formatters;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.ContentWriters
{
    public class MarkdownFeatureTitleWriter
    {
        private readonly IMarkdownFormatter formatter;

        public MarkdownFeatureTitleWriter(IMarkdownFormatter formatter)
        {
            this.formatter = formatter;
        }

        public void Write(StreamWriter writer, string featureName)
        {
            if (string.IsNullOrEmpty(featureName))
            {
                return;
            }

            string header = this.formatter.FormatH1(featureName);
            writer.WriteLine(header);
            //writer.WriteLine();
        }
    }
}
