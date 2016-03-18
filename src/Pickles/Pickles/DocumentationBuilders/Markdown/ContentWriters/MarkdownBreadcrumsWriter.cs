using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NGenerics.DataStructures.Trees;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.DocumentationBuilders.Markdown.Formatters;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.ContentWriters
{
    public class MarkdownBreadcrumsWriter
    {
        private readonly IMarkdownFormatter formatter;

        public MarkdownBreadcrumsWriter(IMarkdownFormatter formatter)
        {
            this.formatter = formatter;
        }

        public void Write(StreamWriter writer, GeneralTree<INode> tree)
        {
            IList<GeneralTree<INode>> path = tree.GetPath();
            for (int index = 0; index < path.Count; index++)
            {
                GeneralTree<INode> parentTree = path[index];
                INode node = parentTree.Data;
                var numberOfBackslashes = tree.Data.NodeType == NodeType.Structure
                    ? path.Count - index
                    : path.Count - index - 1;

                string backslashes = string.Concat(Enumerable.Repeat("..\\", numberOfBackslashes));
                string linkPath = $"{backslashes}{MarkdownFilenames.Index}";
                string link = this.formatter.FormatLink(node.Name, linkPath);

                writer.Write($"/ ");
                writer.Write(link);

                bool isLastNode = index == path.Count - 1;
                if (isLastNode)
                {
                    writer.WriteLine(this.formatter.FormatEndOfLine());
                }
            }
        }
    }
}