﻿using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.ContentWriters
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

            string cleanedDescription = MarkdownCleaner.CleanContent(description);
            string formattedDescription = Format(cleanedDescription);

            writer.WriteLine(formattedDescription);
        }

        private static string Format(string description)
        {
            string singleNewLineDescription = MultipleNewLinesRegEx.Replace(description, "\n");
            return string.Join("  \n", singleNewLineDescription.Split('\n').Select(s => s.Trim()));
        }
    }
}