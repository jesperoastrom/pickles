using System.IO;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.NodeWriters
{
    public class MarkdownFeatureScenarioOutlineWriter
    {
        public void Write(StreamWriter writer, ScenarioOutline scenarioOutline)
        {
            if (scenarioOutline == null)
            {
                return;
            }

            //todo
        }
    }
}