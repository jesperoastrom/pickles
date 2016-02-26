using System.IO;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.ContentWriters
{
    public class MarkdownStepWriter
    {
        private readonly MarkdownTableArgumentWriter tableArgumentWriter;

        public MarkdownStepWriter(MarkdownTableArgumentWriter tableArgumentWriter)
        {
            this.tableArgumentWriter = tableArgumentWriter;
        }

        public void Write(StreamWriter writer, Step step)
        {
            writer.Write($"__{step.NativeKeyword.Trim()}__ ");

            var name = MarkdownCleaner.CleanContent(step.Name);
            writer.WriteLine($" {name}");

            if (step.TableArgument != null)
            {
                this.tableArgumentWriter.Write(writer, step.TableArgument);
            }

            if (!string.IsNullOrEmpty(step.DocStringArgument))
            {
                writer.WriteLine("```");
                writer.WriteLine(step.DocStringArgument);
                writer.WriteLine("```");
            }
        }
    }
}