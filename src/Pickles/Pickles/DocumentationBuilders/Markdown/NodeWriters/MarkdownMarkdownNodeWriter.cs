using System.IO;
using PicklesDoc.Pickles.DirectoryCrawler;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.NodeWriters
{
    public class MarkdownMarkdownNodeWriter
    {
        public void Write(MarkdownNode markdownNode, StreamWriter writer)
        {
            writer.Write(markdownNode.MarkdownOriginalContent);
        }
    }
}