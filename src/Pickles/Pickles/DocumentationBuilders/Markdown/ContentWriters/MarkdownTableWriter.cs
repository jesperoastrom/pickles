using System.IO;
using PicklesDoc.Pickles.DocumentationBuilders.Markdown.Formatters;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.ContentWriters
{
    public class MarkdownTableWriter
    {
        private readonly IMarkdownFormatter formatter;

        public MarkdownTableWriter(IMarkdownFormatter formatter)
        {
            this.formatter = formatter;
        }

        public void Write(StreamWriter writer, Table tableArgument)
        {
            writer.WriteLine();

            this.WriteTableRow(writer, tableArgument.HeaderRow);

            this.WriteHeaderSeparator(writer, tableArgument.HeaderRow);

            foreach (TableRow tableRow in tableArgument.DataRows)
            {
                this.WriteTableRow(writer, tableRow);
            }

            writer.WriteLine();
        }

        private void WriteHeaderSeparator(StreamWriter writer, TableRow headerRow)
        {
            string separator = this.formatter.FormatTableHeaderSeparator(headerRow.Cells.Count);
            writer.Write(separator);
        }

        private void WriteTableRow(StreamWriter writer, TableRow tableRow)
        {
            string row = this.formatter.FormatTableRow(tableRow);
            writer.Write(row);
        }
    }
}