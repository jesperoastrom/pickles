using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.NodeWriters
{
    public class MarkdownDescriptionWriter
    {
        private static readonly Regex MultipleNewLinesRegEx = new Regex(@"[\r\n]+", RegexOptions.Compiled | RegexOptions.Multiline);

        public void Write(StreamWriter writer, string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                return;
            }

            string formattedDescription = Format(description);

            writer.WriteLine(formattedDescription);
        }

        private static string Format(string description)
        {
            string singleNewLineDescription = MultipleNewLinesRegEx.Replace(description, "\n");
            return string.Join("\n\n", singleNewLineDescription.Split('\n').Select(s => s.Trim()));
        }
    }
}