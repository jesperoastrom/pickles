using System.IO;
using System.IO.Abstractions;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.Extensions;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown
{
    public static class MarkdownTreeNodeExtensions
    {
        public static string GetMarkdownFilePath(this INode node, IFileSystem fileSystem, Configuration configuration)
        {
            var outPath = node.GetOutputPath(fileSystem, configuration);
            var markdownFilePath = outPath.Replace(fileSystem.Path.GetExtension(outPath), ".md");
            return markdownFilePath;
        }

        public static string GetOriginalNameAndExtension(this INode node)
        {
            string extension = node.NodeType == NodeType.Content
                ? ".md"
                : node.OriginalLocation.Extension;

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(node.OriginalLocation.Name);

            string nodeName = $"{fileNameWithoutExtension}{extension}";
            return nodeName;
        }
    }
}