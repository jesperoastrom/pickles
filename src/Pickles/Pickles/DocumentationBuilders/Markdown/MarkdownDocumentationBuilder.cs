//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MarkdownDocumentationBuilder.cs" company="PicklesDoc">
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

using System.Linq;
using System.Reflection;
using Autofac.Features.Indexed;
using NGenerics.DataStructures.Trees;
using NLog;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.Extensions;

namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown
{
    public class MarkdownDocumentationBuilder : IDocumentationBuilder
    {
        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        private readonly Configuration configuration;
        private readonly IIndex<NodeType, IMarkdownNodeWriter> nodeWriters;

        public MarkdownDocumentationBuilder(
            Configuration configuration,
            IIndex<NodeType, IMarkdownNodeWriter> nodeWriters)
        {
            this.configuration = configuration;
            this.nodeWriters = nodeWriters;
        }

        #region IDocumentationBuilder Members

        public void Build(GeneralTree<INode> features)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Info("Writing Markdown to {0}", this.configuration.OutputFolder.FullName);
            }

            this.VisitNode(features);
        }

        private void VisitNode(GeneralTree<INode> tree)
        {
            if (tree == null)
            {
                return;
            }

            this.CreateDocumentationForCurrentNode(tree);

            this.VisitChildNodes(tree);
        }

        private void CreateDocumentationForCurrentNode(GeneralTree<INode> tree)
        {
            INode currentNode = tree.Data;
            IMarkdownNodeWriter nodeWriter;

            if (this.nodeWriters.TryGetValue(currentNode.NodeType,  out nodeWriter))
            {
                nodeWriter.WriteNode(tree);
            }
        }

        private void VisitChildNodes(GeneralTree<INode> tree)
        {
            GeneralTree<INode>[] childNodesExceptReadme = tree
                .ChildNodes
                .Where(x => !x.Data.IsIndexMarkDownNode())
                .ToArray();

            foreach (GeneralTree<INode> subtree in childNodesExceptReadme)
            {
                this.VisitNode(subtree);
            }
        }

        #endregion
    }
}
