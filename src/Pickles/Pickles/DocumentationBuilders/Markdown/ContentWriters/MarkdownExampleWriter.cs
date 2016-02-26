using System.IO;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.ContentWriters
{
    public class MarkdownExampleWriter
    {
        private readonly MarkdownTableWriter tableWriter;

        public MarkdownExampleWriter(MarkdownTableWriter tableWriter)
        {
            this.tableWriter = tableWriter;
        }

        public void Write(StreamWriter writer, Example example)
        {
            writer.WriteLine($"#### Example: {example.Name}");
            this.tableWriter.Write(writer, example.TableArgument);
        }
    }
}