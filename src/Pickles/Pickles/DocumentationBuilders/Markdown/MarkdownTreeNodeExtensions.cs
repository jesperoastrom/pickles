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
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.Extensions;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown
{
    public static class MarkdownTreeNodeExtensions
    {
        public static string GetMarkdownFilePath(this INode node, IFileSystem fileSystem, Configuration configuration)
        {
            var outPath = node.GetOutputPath(fileSystem, configuration);
            var markdownFilePath = outPath.Replace(fileSystem.Path.GetExtension(outPath), ".md");
            return markdownFilePath;
        }

        public static string GetOriginalNameAndExtension(this INode node)
        {
            string extension = node.NodeType == NodeType.Content
                ? ".md"
                : node.OriginalLocation.Extension;

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(node.OriginalLocation.Name);

            string nodeName = $"{fileNameWithoutExtension}{extension}";
            return nodeName;
        }
    }
}