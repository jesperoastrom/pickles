using System.Collections.Generic;
using System.IO;
using NGenerics.DataStructures.Trees;
using PicklesDoc.Pickles.DirectoryCrawler;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.NodeWriters
{
    public static class MarkdownNodeExtensions
    {
        public static void WriteBreadcrums(this GeneralTree<INode> tree, StreamWriter writer)
        {
            IList<GeneralTree<INode>> path = tree.GetPath();
            for (int index = 0; index < path.Count; index++)
            {
                GeneralTree<INode> parentTree = path[index];
                INode node = parentTree.Data;
                var numberOfBackSlashes = tree.Data.NodeType == NodeType.Structure
                    ? path.Count - index
                    : path.Count - index - 1;

                writer.Write($"/ [{node.Name}](");
                WriteBackSlashes(numberOfBackSlashes, writer);
                writer.Write($"{MarkdownFilenames.Index}) ");

                bool isLastNode = index == path.Count - 1;
                if (isLastNode)
                {
                    writer.WriteLine();
                    writer.WriteLine();
                }
            }
        }

        private static void WriteBackSlashes(int n, StreamWriter writer)
        {
            for (int i = 0; i < n; i++)
            {
                writer.Write("..\\");
            }
        }
    }
}