using System;
using System.IO.Abstractions;
using NGenerics.DataStructures.Trees;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.IO;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.NodeWriters
{
    public class MarkdownContentNodeWriter : IMarkdownNodeWriter
    {
        private readonly Configuration configuration;
        private readonly IFileSystem fileSystem;
        private readonly IStreamWriterFactory streamWriterFactory;

        public MarkdownContentNodeWriter(
            Configuration configuration,
            IFileSystem fileSystem,
            IStreamWriterFactory streamWriterFactory)
        {
            this.configuration = configuration;
            this.fileSystem = fileSystem;
            this.streamWriterFactory = streamWriterFactory;
        }

        public void WriteNode(GeneralTree<INode> tree)
        {
            string outputPath = tree.Data.GetMarkdownFilePath(this.fileSystem, this.configuration);
            using (var writer = this.streamWriterFactory.Create(outputPath))
            {
                tree.WriteBreadcrums(writer);

                var markdownNode = tree.Data as MarkdownNode;
                if (markdownNode != null)
                {
                    new MarkdownMarkdownNodeWriter().Write(markdownNode, writer);
                    return;
                }

                var featureNode = tree.Data as FeatureNode;
                if (featureNode != null)
                {
                    new MarkdownFeatureNodeWriter().Write(featureNode, writer);
                    return;
                }

                throw new InvalidOperationException($"Cannot format a node with a Type of {tree.Data.GetType()} as content");
            }
        }
    }
}