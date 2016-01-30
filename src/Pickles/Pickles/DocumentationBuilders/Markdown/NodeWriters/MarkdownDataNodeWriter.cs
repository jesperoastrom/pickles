using System.IO.Abstractions;
using NGenerics.DataStructures.Trees;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.Extensions;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.NodeWriters
{
    public class MarkdownDataNodeWriter : IMarkdownNodeWriter
    {
        private readonly Configuration configuration;
        private readonly IFileSystem fileSystem;

        public MarkdownDataNodeWriter(Configuration configuration, IFileSystem fileSystem)
        {
            this.configuration = configuration;
            this.fileSystem = fileSystem;
        }

        public void WriteNode(GeneralTree<INode> tree)
        {
            this.CopySourceFileToRelativeOutFolder(tree.Data);
        }

        private void CopySourceFileToRelativeOutFolder(INode node)
        {
            string outputPath = node.GetOutputPath(this.fileSystem, this.configuration);
            this.fileSystem.File.Copy(node.OriginalLocation.FullName, outputPath, overwrite: true);
        }
    }
}