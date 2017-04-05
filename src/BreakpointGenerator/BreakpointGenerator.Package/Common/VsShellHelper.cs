// //———————————————————————
// // <copyright file="VsShellHelper.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// // A helper class to iterate with Hierarchy items.
// // </summary>
// //———————————————————————

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using VSLangProj;

namespace Microsoft.ALMRangers.BreakpointGenerator.Common
{
    public static class VsShellHelper
    {
        private static DTE _dte;

        private static UIHierarchy solutionExplorer;

        public static DTE Dte
        {
            get
            {
                if (_dte == null)
                {
                    _dte = Package.GetGlobalService(typeof(DTE)) as DTE;
                }

                return _dte;
            }
        }

        public static UIHierarchy SolutionExplorer
        {
            get
            {
                if (solutionExplorer == null)
                {
                    solutionExplorer = ((DTE2) Dte).ToolWindows.SolutionExplorer;
                }

                return solutionExplorer;
            }
        }

        public static BitmapSource GetIcon(ItemType itemType)
        {
            var imageService = (IVsImageService2) Package.GetGlobalService(typeof(SVsImageService));

            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.Flags = (uint) _ImageAttributesFlags.IAF_RequiredFlags;
            imageAttributes.ImageType = (uint) _UIImageType.IT_Bitmap;
            imageAttributes.Format = (uint) _UIDataFormat.DF_WPF;
            imageAttributes.LogicalHeight = 16;
            imageAttributes.LogicalWidth = 16;
            imageAttributes.StructSize = Marshal.SizeOf(typeof(ImageAttributes));

            // Replace this KnownMoniker with your desired ImageMoniker
            IVsUIObject imageObject = imageService.GetImage(KnownMonikers.Blank, imageAttributes);

            switch (itemType)
            {
                case ItemType.Solution:
                {
                    imageObject = imageService.GetImage(KnownMonikers.Solution, imageAttributes);
                    break;
                }
                case ItemType.Project:
                {
                    imageObject = imageService.GetImage(KnownMonikers.CSProjectNode, imageAttributes);
                    break;
                }
                case ItemType.File:
                {
                    imageObject = imageService.GetImage(KnownMonikers.CSFileNode, imageAttributes);
                    break;
                }
                case ItemType.Method:
                {
                    imageObject = imageService.GetImage(KnownMonikers.MethodPublic, imageAttributes);
                    break;
                }
            }
            Object data;
            imageObject.get_Data(out data);
            return data as BitmapSource;
        }

        public static IVsHierarchy GetSelectedHierarchy()
        {
            IntPtr ptr;
            IntPtr ptr2;
            uint num;
            IVsMultiItemSelect select;
            IVsHierarchy hierarchy = null;

            IVsMonitorSelection selection =
                Package.GetGlobalService(typeof(IVsMonitorSelection)) as IVsMonitorSelection;

            if (selection.GetCurrentSelection(out ptr, out num, out select, out ptr2) == 0)
            {
                if (ptr != IntPtr.Zero)
                {
                    hierarchy = Marshal.GetObjectForIUnknown(ptr) as IVsHierarchy;

                    Marshal.Release(ptr);
                }
            }

            return hierarchy;
        }

        public static UIHierarchyItem GetSelectedUIHierarchy()
        {
            object[] selection = SolutionExplorer.SelectedItems as object[];

            if (selection != null && selection.Length == 1)
            {
                return selection[0] as UIHierarchyItem;
            }

            return null;
        }

        /// <summary>
        /// Determines whether [is UI solution node] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// 	<c>true</c> if [is UI solution node] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUISolutionNode(UIHierarchyItem item)
        {
            var project = item.Object as Project;
            var projectItem = item.Object as ProjectItem;

            return ((project != null && project.Kind == ProjectKinds.vsProjectKindSolutionFolder) ||
                    (projectItem != null && projectItem.Object is Project &&
                     ((Project) projectItem.Object).Kind == ProjectKinds.vsProjectKindSolutionFolder));
        }

        /// <summary>
        /// Determines whether [is project node] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        /// 	<c>true</c> if [is project node] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsProjectNode(UIHierarchyItem item)
        {
            var project = item.Object as Project;
            var projectItem = item.Object as ProjectItem;

            return ((project != null && project.Kind != ProjectKinds.vsProjectKindSolutionFolder) ||
                    (projectItem != null && projectItem.Object is Project &&
                     ((Project) projectItem.Object).Kind != ProjectKinds.vsProjectKindSolutionFolder));
        }

        public static bool IsCSharpFile(UIHierarchyItem item)
        {
            var projectItem = item.Object as ProjectItem;
            if (projectItem != null)
            {
                return (projectItem.FileCodeModel != null &&
                        projectItem.FileCodeModel.Language == CodeModelLanguageConstants.vsCMLanguageCSharp);
            }
            return false;
        }

        /// <summary>
        /// Gets the UI project nodes.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <returns></returns>
        public static IEnumerable<UIHierarchyItem> GetUIProjectNodes(UIHierarchyItems root)
        {
            var projects =
                new UIHierarchyItemIterator(root)
                    .Where(item =>
                        (item.Object is Project && ((Project) item.Object).Kind !=
                         ProjectKinds.vsProjectKindSolutionFolder) ||
                        (item.Object is ProjectItem && ((ProjectItem) item.Object).Object is Project &&
                         ((Project) ((ProjectItem) item.Object).Object).Kind !=
                         ProjectKinds.vsProjectKindSolutionFolder))
                    .Select(item => (UIHierarchyItem) item);

            return projects;
        }

        public static bool IsValidItemSelected()
        {
            ProjectItem item = Dte.SelectedItems.Item(1).ProjectItem;
            if (item != null)
            {
                if (item.ContainingProject.Kind == PrjKind.prjKindCSharpProject)
                {
                    //Available only for C# projects and C# files
                    if (item.FileCodeModel != null && item.FileCodeModel.Language ==
                        EnvDTE.CodeModelLanguageConstants.vsCMLanguageCSharp)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}