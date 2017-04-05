// //———————————————————————
// // <copyright file="IVsHierarchyExtensions.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// //  A IVSHierarchy extension methods.
// // </summary>
// //———————————————————————

using System;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.ALMRangers.BreakpointGenerator.Extensions
{
    public static class IVsHierarchyExtensions
    {
        /// <summary>
        /// Converts a IVsHierarchy to a Project.
        /// </summary>
        /// <param name="hierarchy">The hierarchy.</param>
        /// <returns></returns>
        public static Project ToProject(this IVsHierarchy hierarchy)
        {
            object project = null;

            if (hierarchy.GetProperty(VSConstants.VSITEMID_ROOT, (int) __VSHPROPID.VSHPROPID_ExtObject,
                    out project) < 0)
            {
                throw new ArgumentException("IVsHierarchy is not a project.");
            }

            return (Project) project;
        }
    }
}