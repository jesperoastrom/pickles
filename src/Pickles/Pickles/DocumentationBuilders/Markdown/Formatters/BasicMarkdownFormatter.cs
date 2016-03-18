using System;
using System.Text;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.Formatters
{
    public class BasicMarkdownFormatter : IMarkdownFormatter
    {
        private const char TableSeparator = '|';
        private const string TableHeaderSeparatorCellContent = " ---: ";

        public string FormatListItem(string text)
        {
            return $"* {text}";
        }

        public string FormatLink(string text, string link)
        {
            return $"[{text}]({link})";
        }

        public string FormatH1(string text)
        {
            return $"# {text}";
        }

        public string FormatH3(string text)
        {
            return $"### {text}";
        }

        public string FormatH4(string text)
        {
            return $"#### {text}";
        }

        public string FormatEndOfLine()
        {
            return "  ";
        }

        public string FormatItalic(string text)
        {
            return $"_{text}_";
        }

        public string FormatBold(string text)
        {
            return $"**{text}**";
        }

        public string FormatCode(string text)
        {
            return $"```{Environment.NewLine}{text}{Environment.NewLine}```";
        }

        public string FormatTableHeaderSeparator(int cellCount)
        {
            StringBuilder sb = new StringBuilder(TableHeaderSeparatorCellContent.Length * cellCount);

            for (int index = 0; index < cellCount; index++)
            {
                AppendTableCellContent(sb, TableHeaderSeparatorCellContent);
            }

            if (cellCount > 0)
            {
                sb.AppendLine($" {TableSeparator}  ");
            }

            return sb.ToString();
        }

        public string FormatTableRow(TableRow tableRow)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var cell in tableRow.Cells)
            {
                AppendTableCellContent(sb, cell);
            }

            if (tableRow.Cells.Count > 0)
            {
                sb.AppendLine($" {TableSeparator}  ");
            }

            return sb.ToString();
        }

        private static void AppendTableCellContent(StringBuilder sb, string cell)
        {
            sb.Append($"{TableSeparator} ");
            sb.Append(cell);
        }
    }
}
