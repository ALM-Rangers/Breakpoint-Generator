// //———————————————————————
// // <copyright file="NativeMethods.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// // The common class which has declartions for Native COM32 methods. Necessary for showing icons in tree view.
// // </summary>
// //———————————————————————

using System;
using System.Runtime.InteropServices;

namespace Microsoft.ALMRangers.BreakpointGenerator.Common
{
    class NativeMethods
    {
        [DllImport("comctl32.dll", SetLastError = true)]
        public static extern IntPtr ImageList_GetIcon(IntPtr himl, Int32 i, Int32 flags);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        public static extern Int32 SetWindowTheme(IntPtr hWnd, String subAppName, String subIdList);
    }
}