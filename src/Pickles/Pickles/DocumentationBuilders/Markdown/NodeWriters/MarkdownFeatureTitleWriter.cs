using System.IO;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.NodeWriters
{
    public class MarkdownFeatureTitleWriter
    {
        public void Write(StreamWriter writer, string featureName)
        {
            if (string.IsNullOrEmpty(featureName))
            {
                return;
            }

            writer.WriteLine($"# {featureName}");
            writer.WriteLine();
        }
    }
}
