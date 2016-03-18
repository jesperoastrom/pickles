using System.Collections.Generic;
using System.IO;
using System.Linq;
using PicklesDoc.Pickles.DocumentationBuilders.Markdown.Formatters;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.ContentWriters
{
    public class MarkdownFeatureTagsWriter
    {
        private readonly IMarkdownFormatter formatter;

        public MarkdownFeatureTagsWriter(IMarkdownFormatter formatter)
        {
            this.formatter = formatter;
        }

        public void Write(StreamWriter writer, List<string> tags)
        {
            if (tags == null || tags.Count == 0)
            {
                return;
            }

            string[] orderedTags = tags
                .OrderBy(tag => tag)
                .ToArray();

            int lastTagIndex = orderedTags.Length - 1;

            for (int index = 0; index < orderedTags.Length; index++)
            {
                string tag = orderedTags[index];
                string italic = this.formatter.FormatItalic(tag);
                writer.Write(italic);

                bool isLast = index == lastTagIndex;
                if (!isLast)
                {
                    writer.Write(" ");
                }
            }

            writer.WriteLine(this.formatter.FormatEndOfLine());
        }
    }
}