using System.IO;
using PicklesDoc.Pickles.DocumentationBuilders.Markdown.Formatters;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.ContentWriters
{
    public class MarkdownExampleWriter
    {
        private readonly MarkdownTableWriter tableWriter;
        private readonly IMarkdownFormatter formatter;

        public MarkdownExampleWriter(MarkdownTableWriter tableWriter, IMarkdownFormatter formatter)
        {
            this.tableWriter = tableWriter;
            this.formatter = formatter;
        }

        public void Write(StreamWriter writer, Example example)
        {
            string header = this.formatter.FormatH4($"Example: {example.Name}");
            writer.WriteLine(header);
            this.tableWriter.Write(writer, example.TableArgument);
        }
    }
}