using System.IO;
using PicklesDoc.Pickles.DocumentationBuilders.Markdown.Formatters;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.ContentWriters
{
    public class MarkdownStepWriter
    {
        private readonly MarkdownTableWriter tableWriter;
        private readonly IMarkdownFormatter formatter;

        public MarkdownStepWriter(MarkdownTableWriter tableWriter, IMarkdownFormatter formatter)
        {
            this.tableWriter = tableWriter;
            this.formatter = formatter;
        }

        public void Write(StreamWriter writer, Step step)
        {
            string nativeKeyword = this.formatter.FormatBold(step.NativeKeyword.Trim());
            writer.Write(nativeKeyword);

            var name = MarkdownCleaner.CleanContent(step.Name);
            writer.WriteLine($" {name}  ");

            if (step.TableArgument != null)
            {
                this.tableWriter.Write(writer, step.TableArgument);
            }

            if (!string.IsNullOrEmpty(step.DocStringArgument))
            {
                string docStringArgument = this.formatter.FormatCode(step.DocStringArgument);
                writer.WriteLine(docStringArgument);
            }
        }
    }
}