// //———————————————————————
// // <copyright file="PackageContext.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// //  A singleton class to hold the package context so that it can be accessed in other classes.
// // </summary>
// //———————————————————————

using System;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.ALMRangers.BreakpointGenerator.Common
{
    public class PackageContext
    {
        private static readonly PackageContext _instance = new PackageContext();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static PackageContext()
        {
        }

        private PackageContext()
        {
        }

        public static PackageContext Instance
        {
            get { return _instance; }
        }

        public IServiceProvider ServiceProvider { get; set; }
        public BreakpointGeneratorPackage Package { get; set; }
        public IVsSolution Solution { get; set; }
        public uint SolutionCookie { get; set; }
        public IVsHierarchy Hierarchy { get; set; }
    }
}