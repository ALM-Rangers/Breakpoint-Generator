// //———————————————————————
// // <copyright file="ShowBreakpointGeneratorToolWindowCommand.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// //  The menu command for "Show Breakpoint Generator Toolwindow" action.
// // </summary>
// //———————————————————————

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
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
    [Guid("39466096-2290-4156-99E9-ADAE31AFA277")]
    [DisplayName("Breakpoint Generator")]
    public class ShowBreakpointGeneratorToolWindowCommand : DynamicCommand
    {
        public const uint cmdidBreakpointGenerator = 0x4121;

        public ShowBreakpointGeneratorToolWindowCommand(IServiceProvider serviceProvider) :
            base(serviceProvider, OnExecute,
                new CommandID(typeof(ShowBreakpointGeneratorToolWindowCommand).GUID, (int) cmdidBreakpointGenerator))
        {
        }

        protected override bool CanExecute(OleMenuCommand command)
        {
            try
            {
                if (base.CanExecute(command))
                {
                    var project = VsShellHelper.Dte.SelectedItems.Item(1).Project;

                    if (project != null && project.Kind == PrjKind.prjKindCSharpProject)
                    {
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
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return false;
        }

        private static void OnExecute(object sender, EventArgs e)
        {
            try
            {
                ToolWindowPane window = BreakpointGeneratorPackage.BreakpointGeneratorToolWindow;
                if ((window == null) || (window.Frame == null))
                {
                    throw new COMException("Cannot create toolwindow");
                }

                IVsWindowFrame windowFrame = (IVsWindowFrame) window.Frame;
                ErrorHandler.ThrowOnFailure(windowFrame.Show());

                var viewModel = BreakpointGeneratorToolWindowViewModel.Instance;
                var project = VsShellHelper.Dte.SelectedItems.Item(1).Project;
                string path;

                if (project != null && project.Kind == PrjKind.prjKindCSharpProject)
                {
                    //Executed at the project level
                    path = project.FullName;
                    viewModel.AnalyzeProject(VsShellHelper.Dte, path);
                    return;
                }
                var projectItem = VsShellHelper.Dte.SelectedItems.Item(1).ProjectItem;
                if (projectItem != null)
                {
                    var containingProject = projectItem.ContainingProject;

                    //Executed at the folder level / item level
                    if (projectItem.FileCodeModel != null &&
                        projectItem.FileCodeModel.Language == CodeModelLanguageConstants.vsCMLanguageCSharp)
                    {
                        path = projectItem.FileNames[0];

                        viewModel.AnalyzeFile(VsShellHelper.Dte, containingProject.FullName, path);
                        return;
                    }

                    //probably a folder inside the solution

                    viewModel.AnalyzeProject(VsShellHelper.Dte, containingProject.FullName);
                    return;
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
                            path = vsProject.Project.FullName;
                            viewModel.AnalyzeProject(VsShellHelper.Dte, path);
                        }
                    }
                }
                else if (VsShellHelper.Dte.Solution.IsOpen)
                {
                    path = VsShellHelper.Dte.Solution.FullName;
                    viewModel.AnalyzeSolution(VsShellHelper.Dte, path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}