// //———————————————————————
// // <copyright file="BreakpointGeneratorPackage.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// //  A package class necessary for the extension.
// // </summary>
// //———————————————————————

using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using Microsoft.ALMRangers.BreakpointGenerator.Options;
using Microsoft.ALMRangers.BreakpointGenerator.Services;
using Microsoft.ALMRangers.BreakpointGenerator.ToolWindows;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.ALMRangers.BreakpointGenerator
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad("{ADFC4E64-0397-11D1-9F4E-00A0C911004F}")]
    [ProvideService(typeof(SCommandManagerService), ServiceName = "CommandManagerService")]
    [ProvideToolWindow(typeof(BreakpointGeneratorToolWindow), MultiInstances = false, Transient = true)]
    [ProvideOptionPage(typeof(BreakpointGeneratorOptions), "Breakpoint Generator", "General", 0, 0, true)]
    [Guid("d26b7824-0b3f-4a14-aaa0-0ae9853d272c")]
    public sealed class BreakpointGeneratorPackage : Package
    {
        private BreakpointGeneratorToolWindow _breakpointGeneratorToolWindow;

        public BreakpointGeneratorToolWindow BreakpointGeneratorToolWindow
        {
            get
            {
                if (_breakpointGeneratorToolWindow == null)
                {
                    _breakpointGeneratorToolWindow =
                        this.FindToolWindow(typeof(BreakpointGeneratorToolWindow), 0,
                            true) as BreakpointGeneratorToolWindow;
                }

                return _breakpointGeneratorToolWindow;
            }
        }


        protected override void Initialize()
        {
            base.Initialize();

            (this as IServiceContainer).AddService(typeof(SCommandManagerService),
                new ServiceCreatorCallback(CreateCommandManagerService), true);

            var options = GetDialogPage(typeof(BreakpointGeneratorOptions)) as BreakpointGeneratorOptions;
            if (options != null)
            {
                UserSettings.Options = options;
            }

            CommandSet commandSet = new CommandSet(this);
            commandSet.Initialize();
        }

        private object CreateCommandManagerService(IServiceContainer container, Type serviceType)
        {
            if (container != this)
            {
                return null;
            }

            if (typeof(SCommandManagerService) == serviceType)
            {
                return new CommandManagerService();
            }

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}