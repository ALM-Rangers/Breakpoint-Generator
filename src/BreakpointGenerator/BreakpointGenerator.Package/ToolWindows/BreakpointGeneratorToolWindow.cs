// //———————————————————————
// // <copyright file="BreakpointGeneratorToolWindow.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// // This class implements the tool window exposed by this package and hosts a user control.
// //
// // In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane, 
// // usually implemented by the package implementer.
// //
// // This class derives from the ToolWindowPane class provided from the MPF in order to use its 
// // implementation of the IVsUIElementPane interface.
// // </summary>
// //———————————————————————

using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.ALMRangers.BreakpointGenerator.ToolWindows
{
    [Guid("4db55759-ee05-48c6-9ac8-087971420a46")]
    public class BreakpointGeneratorToolWindow : ToolWindowPane //, IVsWindowFrameNotify3
    {
        private BreakPointGeneratorWindow _window;

        /// <summary>
        /// Standard constructor for the tool window.
        /// </summary>
        public BreakpointGeneratorToolWindow() : base(null)
        {
            this.Caption = Resources.ToolWindowTitle;
            this.BitmapResourceID = 301;
            this.BitmapIndex = 1;
            this.ToolBar = new CommandID(PackageGuids.guidToolbar, PackageIds.toolbar);
            _window = new BreakPointGeneratorWindow(this);
            base.Content = _window;
        }


        public override IWin32Window Window
        {
            get { return (IWin32Window) _window; }
        }
    }
}