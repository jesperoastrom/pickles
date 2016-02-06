using System.IO;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.NodeWriters
{
    public class MarkdownTableArgumentWriter
    {
        public void Write(StreamWriter writer, Table tableArgument)
        {
            foreach (TableRow tableRow in tableArgument.DataRows)
            {
                Write(writer, tableRow);
            }
        }

        private static void Write(StreamWriter writer, TableRow tableRow)
        {
            foreach (var cell in tableRow.Cells)
            {
                Write(writer, cell);
            }
        }

        private static void Write(StreamWriter writer, string cell)
        {
            writer.WriteLine(cell);
        }
    }
}