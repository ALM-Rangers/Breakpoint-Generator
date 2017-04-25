// //———————————————————————
// // <copyright file="Tree.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// //  A model to hold the tree structure.
// // </summary>
// //———————————————————————

using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ALMRangers.BreakpointGenerator.Common
{
    public class Tree<T> : IEnumerable<Tree<T>>
    {
        public Tree(T text, ItemType itemType)
        {
            this.ItemType = itemType;
            this.Node = text;
            this.Children = new LinkedList<Tree<T>>();
        }

        public ItemType ItemType { get; set; }
        public T Node { get; set; }
        public Tree<T> Parent { get; set; }
        public ICollection<Tree<T>> Children { get; set; }
        public bool Checked { get; set; }

        public IEnumerator<Tree<T>> GetEnumerator()
        {
            yield return this;
            //foreach (var child in this.Children)
            //{
            //    yield return child;
            //    //if (!child.Children.Any())
            //    //    yield return child;
            //    //else
            //    //{
            //    //    yield return child;
            //    //    foreach (var kid in child.Children)
            //    //    {
            //    //        yield return kid;
            //    //    }
            //    //}
            //}
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Tree<T> AddChild(T child, ItemType itemType)
        {
            Tree<T> childNode = new Tree<T>(child, itemType) {Parent = this};
            this.Children.Add(childNode);
            return childNode;
        }

        public Tree<T> CreateNode(T child, ItemType itemType)
        {
            Tree<T> childNode = new Tree<T>(child, itemType) {Parent = this};
            return childNode;
        }

        public Tree<T> AppendChildNode(Tree<T> childNode)
        {
            this.Children.Add(childNode);
            return childNode;
        }
    }
}