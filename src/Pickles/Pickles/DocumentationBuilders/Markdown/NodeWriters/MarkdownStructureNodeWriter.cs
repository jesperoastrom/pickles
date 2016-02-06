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

using System.IO;
using System.IO.Abstractions;
using System.Linq;
using NGenerics.DataStructures.Trees;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.Extensions;
using PicklesDoc.Pickles.IO;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.NodeWriters
{
    public class MarkdownStructureNodeWriter : IMarkdownNodeWriter
    {
        private readonly Configuration configuration;
        private readonly IFileSystem fileSystem;
        private readonly IStreamWriterFactory streamWriterFactory;
        private readonly MarkdownBreadcrumsWriter breadcrumsWriter;

        public MarkdownStructureNodeWriter(
            Configuration configuration,
            IFileSystem fileSystem,
            IStreamWriterFactory streamWriterFactory,
            MarkdownBreadcrumsWriter breadcrumsWriter)
        {
            this.configuration = configuration;
            this.fileSystem = fileSystem;
            this.streamWriterFactory = streamWriterFactory;
            this.breadcrumsWriter = breadcrumsWriter;
        }

        public void WriteNode(GeneralTree<INode> tree)
        {
            string outputPath = tree.Data.GetOutputPath(this.fileSystem, this.configuration);
            string markdownFilePath = this.fileSystem.Path.Combine(outputPath, MarkdownFilenames.Index);

            this.fileSystem.Directory.CreateDirectory(outputPath);

            using (var writer = this.streamWriterFactory.Create(markdownFilePath))
            {
                this.breadcrumsWriter.Write(writer, tree);
                WriteChildLinks(writer, tree);
                WriteCustomIndexMarkdown(writer, tree);
                writer.Close();
            }
        }

        private static void WriteCustomIndexMarkdown(StreamWriter writer, GeneralTree<INode> tree)
        {
            var readMeNodes = tree
                .ChildNodes
                .Where(x => x.Data.IsIndexMarkDownNode())
                .Select(x => x.Data)
                .Cast<MarkdownNode>()
                .ToArray();

            foreach (MarkdownNode markdownNode in readMeNodes)
            {
                writer.Write(markdownNode.MarkdownOriginalContent);
            }
        }

        private static void WriteChildLinks(StreamWriter writer, GeneralTree<INode> tree)
        {
            foreach (GeneralTree<INode> childTree in tree.ChildNodes.OrderBy(x => x.Data.Name))
            {
                if (childTree.Data.IsIndexMarkDownNode() == false)
                {
                    WriteChildLink(writer, childTree);
                }
            }
        }

        private static void WriteChildLink(StreamWriter writer, GeneralTree<INode> childTree)
        {
            INode node = childTree.Data;
            if (node.NodeType == NodeType.Structure)
            {
                WriteStructureChildLink(writer, childTree);
            }
            else
            {
                WriteNonStructureChildLink(writer, childTree);
            }
        }

        private static void WriteNonStructureChildLink(StreamWriter writer, GeneralTree<INode> childTree)
        {
            INode node = childTree.Data;
            bool isImageNode = node is ImageNode;

            if (isImageNode)
            {
                return;
            }

            string nodeNameAndExtension = node.GetOriginalNameAndExtension();
            writer.WriteLine($"* [{node.Name}]({nodeNameAndExtension})");
        }

        private static void WriteStructureChildLink(StreamWriter writer, GeneralTree<INode> childTree)
        {
            INode node = childTree.Data;
            writer.WriteLine($"* [{node.Name}]({node.OriginalLocation.Name}\\{MarkdownFilenames.Index})");
        }
    }
}