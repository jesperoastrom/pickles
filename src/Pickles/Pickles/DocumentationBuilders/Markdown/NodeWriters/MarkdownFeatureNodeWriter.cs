//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WhenResolvingADocumentationBuilder.cs" company="PicklesDoc">
//  Copyright 2011 Jeffrey Cameron
//  Copyright 2012-present PicklesDoc team and community contributors
//
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.DocumentationBuilders.Markdown.ContentWriters;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.NodeWriters
{
    public class MarkdownFeatureNodeWriter
    {
        private readonly MarkdownFeatureTitleWriter titleWriter;
        private readonly MarkdownFeatureTagsWriter tagsWriter;
        private readonly MarkdownDescriptionWriter descriptionWriter;
        private readonly MarkdownFeatureScenarioWriter scenarioWriter;
        private readonly MarkdownFeatureScenarioOutlineWriter scenarioOutlineWriter;

        public MarkdownFeatureNodeWriter(
            MarkdownFeatureTitleWriter titleWriter,
            MarkdownFeatureTagsWriter tagsWriter,
            MarkdownDescriptionWriter descriptionWriter,
            MarkdownFeatureScenarioWriter scenarioWriter,
            MarkdownFeatureScenarioOutlineWriter scenarioOutlineWriter)
        {
            this.titleWriter = titleWriter;
            this.tagsWriter = tagsWriter;
            this.descriptionWriter = descriptionWriter;
            this.scenarioWriter = scenarioWriter;
            this.scenarioOutlineWriter = scenarioOutlineWriter;
        }

        public void Write(StreamWriter writer, FeatureNode featureNode)
        {
            if (featureNode == null)
            {
                throw new ArgumentNullException(nameof(featureNode));
            }

            Feature feature = featureNode.Feature;

            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }

            this.titleWriter.Write(writer, feature.Name);
            this.tagsWriter.Write(writer, feature.Tags);
            this.descriptionWriter.Write(writer, feature.Description);
            if (feature.Background != null)
            {
                writer.WriteLine("### Background");
                this.scenarioWriter.Write(writer, feature.Background);
            }

            foreach (var featureElement in feature.FeatureElements)
            {
                var scenario = featureElement as Scenario;
                if (scenario != null)
                {
                    this.scenarioWriter.Write(writer, scenario);
                    continue;
                }

                var scenarioOutline = featureElement as ScenarioOutline;
                if (scenarioOutline != null)
                {
                    this.scenarioOutlineWriter.Write(writer, scenarioOutline);
                }
            }
        }
    }
}
