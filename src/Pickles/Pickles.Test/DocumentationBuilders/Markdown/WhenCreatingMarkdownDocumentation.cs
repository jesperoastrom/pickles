using Autofac;
using Autofac.Features.Indexed;
using NFluent;
using NUnit.Framework;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.DocumentationBuilders.Markdown;

namespace PicklesDoc.Pickles.Test.DocumentationBuilders.Markdown
{
    [TestFixture]
    public class WhenCreatingMarkdownDocumentation : BaseFixture
    {
        [Test]
        public void ThenNodeWritersCanBeResolved()
        {
            var nodeWriters = Container.Resolve<IIndex<NodeType, IMarkdownNodeWriter>>();
            Check.That(nodeWriters).IsNotNull();
            Check.That(ThereIsAMarkdownNodeWriterForNodeType(nodeWriters, NodeType.Data));
            Check.That(ThereIsAMarkdownNodeWriterForNodeType(nodeWriters, NodeType.Content));
            Check.That(ThereIsAMarkdownNodeWriterForNodeType(nodeWriters, NodeType.Structure));
        }

        [Test]
        public void ThenDataNodesCanBeWritten()
        {
            var nodeWriters = Container.Resolve<IIndex<NodeType, IMarkdownNodeWriter>>();
            Check.That(ThereIsAMarkdownNodeWriterForNodeType(nodeWriters, NodeType.Data));
        }

        [Test]
        public void ThenContentNodesCanBeWritten()
        {
            var nodeWriters = Container.Resolve<IIndex<NodeType, IMarkdownNodeWriter>>();
            Check.That(ThereIsAMarkdownNodeWriterForNodeType(nodeWriters, NodeType.Content));
        }

        [Test]
        public void ThenStructureNodesCanBeWritten()
        {
            var nodeWriters = Container.Resolve<IIndex<NodeType, IMarkdownNodeWriter>>();
            Check.That(ThereIsAMarkdownNodeWriterForNodeType(nodeWriters, NodeType.Structure));
        }

        private static bool ThereIsAMarkdownNodeWriterForNodeType(IIndex<NodeType, IMarkdownNodeWriter> nodeWriters, NodeType nodeType)
        {
            IMarkdownNodeWriter writer;
            return nodeWriters.TryGetValue(nodeType, out writer);
        }
    }
}
