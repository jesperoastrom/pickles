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
        private readonly MarkdownBreadcrumsWriter breadcrumsWriter;
        private readonly MarkdownMarkdownNodeWriter markdownNodeWriter;
        private readonly MarkdownFeatureNodeWriter featureNodeWriter;

        public MarkdownContentNodeWriter(
            Configuration configuration,
            IFileSystem fileSystem,
            IStreamWriterFactory streamWriterFactory,
            MarkdownBreadcrumsWriter breadcrumsWriter,
            MarkdownMarkdownNodeWriter markdownNodeWriter,
            MarkdownFeatureNodeWriter featureNodeWriter)
        {
            this.configuration = configuration;
            this.fileSystem = fileSystem;
            this.streamWriterFactory = streamWriterFactory;
            this.breadcrumsWriter = breadcrumsWriter;
            this.markdownNodeWriter = markdownNodeWriter;
            this.featureNodeWriter = featureNodeWriter;
        }

        public void WriteNode(GeneralTree<INode> tree)
        {
            string outputPath = tree.Data.GetMarkdownFilePath(this.fileSystem, this.configuration);
            using (var writer = this.streamWriterFactory.Create(outputPath))
            {
                this.breadcrumsWriter.Write(writer, tree);

                var markdownNode = tree.Data as MarkdownNode;
                if (markdownNode != null)
                {
                    this.markdownNodeWriter.Write(writer, markdownNode);
                    return;
                }

                var featureNode = tree.Data as FeatureNode;
                if (featureNode != null)
                {
                    this.featureNodeWriter.Write(writer, featureNode);
                    return;
                }

                throw new InvalidOperationException($"Cannot format a node with a Type of {tree.Data.GetType()} as content");
            }
        }
    }
}