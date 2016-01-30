using System.IO;
using PicklesDoc.Pickles.DirectoryCrawler;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.NodeWriters
{
    public class MarkdownFeatureNodeWriter
    {
        public void Write(FeatureNode featureNode, StreamWriter writer)
        {
            writer.WriteLine(featureNode.Feature.Name);
            writer.WriteLine("todo");
        }
    }
}
