using System.IO;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.ContentWriters
{
    public class MarkdownTableWriter
    {
        private const char TableSeparator = '|';
        private const string TableHeaderSeparatorCellContent = " ---: ";

        public void Write(StreamWriter writer, Table tableArgument)
        {
            writer.WriteLine();

            Write(writer, tableArgument.HeaderRow);

            WriteHeaderSeparator(writer, tableArgument.HeaderRow);

            foreach (TableRow tableRow in tableArgument.DataRows)
            {
                Write(writer, tableRow);
            }

            writer.WriteLine();
        }

        private static void WriteHeaderSeparator(StreamWriter writer, TableRow headerRow)
        {
            for (int index = 0; index < headerRow.Cells.Count; index++)
            {
                Write(writer, TableHeaderSeparatorCellContent);
            }

            if (headerRow.Cells.Count > 0)
            {
                writer.WriteLine($" {TableSeparator}  ");
            }
        }

        private static void Write(StreamWriter writer, TableRow tableRow)
        {
            foreach (var cell in tableRow.Cells)
            {
                Write(writer, cell);
            }

            if (tableRow.Cells.Count > 0)
            {
                writer.WriteLine($" {TableSeparator}  ");
            }
        }

        private static void Write(StreamWriter writer, string cell)
        {
            writer.Write($"{TableSeparator} ");
            writer.Write(cell);
        }
    }
}