using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.NodeWriters
{
    public class MarkdownFeatureTagsWriter
    {
        public void Write(StreamWriter writer, List<string> tags)
        {
            if (tags == null || tags.Count == 0)
            {
                return;
            }

            string[] orderedTags = tags
                .OrderBy(tag => tag)
                .ToArray();

            writer.Write("**_");

            foreach (string tag in orderedTags)
            {
                writer.Write($"{tag} ");
            }

            writer.WriteLine("_**");
            writer.WriteLine();
        }
    }
}