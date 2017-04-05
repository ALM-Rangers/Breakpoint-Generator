// //———————————————————————
// // <copyright file="IntegrationTests.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// //  Some basic unit tests
// // </summary>
// //———————————————————————

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ALMRangers.BreakpointGenerator.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.ALMRangers.BreakpointGenerator.Analyzer.Test
{
    [TestClass]
    public class IntegrationTests
    {
        private TreeViewModel _treeviewModel;

        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public async Task ParseSolution()
        {
            var tree =
                await CodeParser.GetPublicMethodsFromSolution(
                    @"C:\Users\jakobe\Source\Repos\NewRepo4\WebApplication1\WebApplication1.sln");
            Console.WriteLine("****************************Traversing tree*********************************");
            _treeviewModel = new TreeViewModel(tree.Node);
            BuildTree(tree, _treeviewModel);
        }

        [TestMethod]
        public async Task ParseFile()
        {
            var projectPath =
                @"C:\Users\jakobe\Source\Repos\NewRepo4\WebApplication1\ClassLibrary1\ClassLibrary1.csproj";
            var filePath = @"C:\Users\jakobe\Source\Repos\NewRepo4\WebApplication1\ClassLibrary1\Class2.cs";
            Tree<TreeNode> tree = await CodeParser.GetPublicMethodsFromFile(projectPath, filePath);
            Console.WriteLine("****************************Traversing tree*********************************");
            _treeviewModel = new TreeViewModel(tree.Node);
            BuildTree(tree, _treeviewModel);
        }

        private void BuildTree(Tree<TreeNode> tree, TreeViewModel root)
        {
            foreach (var treeItem in tree)
            {
                foreach (var child in treeItem.Children)
                {
                    var childNode = root.AddChild(child.Node);
                    Console.WriteLine(childNode.Text.DisplayName);
                    BuildTree(child, childNode);
                }
            }
        }
    }

    public class TreeViewModel
    {
        bool? _isChecked = false;

        public TreeViewModel(TreeNode text)
        {
            Text = text;
            this.Children = new List<TreeViewModel>();
        }

        public TreeViewModel Parent { get; set; }
        public List<TreeViewModel> Children { get; set; }

        public bool IsInitiallySelected { get; private set; }

        public TreeNode Text { get; set; }

        //public void Initialize()
        //{
        //    foreach (TreeViewModel child in Children)
        //    {
        //        //child._parent = this;
        //        child.Initialize();
        //    }
        //}

        /// <summary>
        /// Gets/sets the state of the associated UI toggle (ex. CheckBox).
        /// The return value is calculated based on the check state of all
        /// child FooViewModels.  Setting this property to true or false
        /// will set all children to the same check state, and setting it 
        /// to any value will cause the parent to verify its check state.
        /// </summary>
        public bool? IsChecked
        {
            get { return _isChecked; }
            set { SetIsChecked(value, true, true); }
        }


        public TreeViewModel AddChild(TreeNode text)
        {
            TreeViewModel childNode = new TreeViewModel(text) {Parent = this};
            this.Children.Add(childNode);
            return childNode;
        }

        void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == _isChecked)
                return;

            _isChecked = value;

            if (updateChildren && _isChecked.HasValue)
                Children.ForEach(c => c.SetIsChecked(_isChecked, true, false));

            if (updateParent && Parent != null)
                Parent.VerifyCheckState();
        }

        void VerifyCheckState()
        {
            bool? state = null;
            for (int i = 0; i < Children.Count; ++i)
            {
                bool? current = Children[i].IsChecked;
                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }
            SetIsChecked(state, false, true);
        }
    }
}