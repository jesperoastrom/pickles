﻿//  --------------------------------------------------------------------------------------------------------------------
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