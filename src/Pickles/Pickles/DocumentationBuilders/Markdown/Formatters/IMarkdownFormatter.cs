using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.Formatters
{
    public interface IMarkdownFormatter
    {
        string FormatListItem(string text);
        string FormatLink(string text, string link);
        string FormatH1(string text);
        string FormatH3(string name);
        string FormatH4(string text);
        string FormatEndOfLine();
        string FormatItalic(string text);
        string FormatBold(string text);
        string FormatCode(string text);
        string FormatTableHeaderSeparator(int cellCount);
        string FormatTableRow(TableRow tableRow);
    }
}