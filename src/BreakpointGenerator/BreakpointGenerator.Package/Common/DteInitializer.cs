// //———————————————————————
// // <copyright file="DteInitializer.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// //  A utility class to initialize Dte.
// // </summary>
// //———————————————————————

using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.ALMRangers.BreakpointGenerator.Common
{
    internal class DteInitializer : IVsShellPropertyEvents
    {
        private IVsShell shellService;
        private uint cookie;
        private Action callback;

        internal DteInitializer(IVsShell shellService, Action callback)
        {
            int hr;

            this.shellService = shellService;
            this.callback = callback;

            // Set an event handler to detect when the IDE is fully initialized
            hr = this.shellService.AdviseShellPropertyChanges(this, out this.cookie);

            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);
        }

        int IVsShellPropertyEvents.OnShellPropertyChange(int propid, object var)
        {
            int hr;
            bool isZombie;

            if (propid == (int) __VSSPROPID.VSSPROPID_Zombie)
            {
                isZombie = (bool) var;

                if (!isZombie)
                {
                    // Release the event handler to detect when the IDE is fully initialized
                    hr = this.shellService.UnadviseShellPropertyChanges(this.cookie);

                    Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(hr);

                    this.cookie = 0;

                    this.callback();
                }
            }
            return VSConstants.S_OK;
        }
    }
}