using NGenerics.DataStructures.Trees;
using PicklesDoc.Pickles.DirectoryCrawler;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown
{
    public interface IMarkdownNodeWriter
    {
        void WriteNode(GeneralTree<INode> tree);
    }
}