// //———————————————————————
// // <copyright file="ShowBreakpointGeneratorViaDebugCommand.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// //  The menu command for "Show Breakpoint Generator" in Debug menu.
// // </summary>
// //———————————————————————

using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using EnvDTE;
using Microsoft.ALMRangers.BreakpointGenerator.Commands.Base;
using Microsoft.ALMRangers.BreakpointGenerator.Common;
using Microsoft.ALMRangers.BreakpointGenerator.Extensions;
using Microsoft.ALMRangers.BreakpointGenerator.ViewModels;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using VSLangProj;

namespace Microsoft.ALMRangers.BreakpointGenerator.Commands
{
    public class ShowBreakpointGeneratorViaDebugCommand : DynamicCommand
    {
        public ShowBreakpointGeneratorViaDebugCommand(IServiceProvider serviceProvider) :
            base(serviceProvider, OnExecute,
                new CommandID(PackageGuids.guidShowBreakpointGeneratorToolWindowCommand,
                    (int) PackageIds.cmdidBreakpointGeneratorDebugMenu))
        {
        }

        protected override bool CanExecute(OleMenuCommand command)
        {
            if (base.CanExecute(command))
            {
                var project = VsShellHelper.Dte.SelectedItems.Item(1).Project;

                if (project != null && project.Kind == PrjKind.prjKindCSharpProject)
                {
                    //Executed at the project level
                    return true;
                }
                var projectItem = VsShellHelper.Dte.SelectedItems.Item(1).ProjectItem;
                if (projectItem != null)
                {
                    //Executed at the folder level / item level
                    if (projectItem.FileCodeModel != null &&
                        projectItem.FileCodeModel.Language == CodeModelLanguageConstants.vsCMLanguageCSharp)
                    {
                        return true;
                    }

                    //TODO:probably a folder inside the solution
                    return true;
                }
                //Executed at the solution level
                var selectedHierarchy = VsShellHelper.GetSelectedHierarchy();
                if (selectedHierarchy != null)
                {
                    Project selectedProject = selectedHierarchy.ToProject();
                    if (selectedProject != null)
                    {
                        VSProject vsProject = selectedProject.Object as VSProject;
                        if (vsProject != null)
                        {
                            return true;
                        }
                        return false;
                    }
                }
                else if (VsShellHelper.Dte.Solution.IsOpen)
                {
                    return true;
                }
                return false;
            }

            return false;
        }

        private static void OnExecute(object sender, EventArgs e)
        {
            ToolWindowPane window = BreakpointGeneratorPackage.BreakpointGeneratorToolWindow;
            if ((window == null) || (window.Frame == null))
            {
                throw new COMException("Cannot create toolwindow");
            }

            IVsWindowFrame windowFrame = (IVsWindowFrame) window.Frame;
            ErrorHandler.ThrowOnFailure(windowFrame.Show());
            var viewModel = BreakpointGeneratorToolWindowViewModel.Instance;

            //Executed at the solution level
            if (VsShellHelper.Dte.Solution.IsOpen)
            {
                var path = VsShellHelper.Dte.Solution.FullName;
                viewModel.AnalyzeSolution(VsShellHelper.Dte, path);
            }
        }
    }
}