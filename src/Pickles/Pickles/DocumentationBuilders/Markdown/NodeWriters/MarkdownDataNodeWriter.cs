//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MarkdownDataNodeWriter.cs" company="PicklesDoc">
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

using System.IO.Abstractions;
using NGenerics.DataStructures.Trees;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.Extensions;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown.NodeWriters
{
    public class MarkdownDataNodeWriter : IMarkdownNodeWriter
    {
        private readonly Configuration configuration;
        private readonly IFileSystem fileSystem;

        public MarkdownDataNodeWriter(Configuration configuration, IFileSystem fileSystem)
        {
            this.configuration = configuration;
            this.fileSystem = fileSystem;
        }

        public void WriteNode(GeneralTree<INode> tree)
        {
            this.CopySourceFileToRelativeOutFolder(tree.Data);
        }

        private void CopySourceFileToRelativeOutFolder(INode node)
        {
            string outputPath = node.GetOutputPath(this.fileSystem, this.configuration);
            this.fileSystem.File.Copy(node.OriginalLocation.FullName, outputPath, overwrite: true);
        }
    }
}