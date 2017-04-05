// //———————————————————————
// // <copyright file="OpenSettingsCommand.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// //  The menu command for "Open Settings" action.
// // </summary>
// //———————————————————————

using System;
using System.ComponentModel.Design;
using Microsoft.ALMRangers.BreakpointGenerator.Commands.Base;
using Microsoft.ALMRangers.BreakpointGenerator.Options;

namespace Microsoft.ALMRangers.BreakpointGenerator.Commands
{
    public class OpenSettingsCommand : DynamicCommand
    {
        public OpenSettingsCommand(IServiceProvider provider)
            : base(provider, OnExecute, new CommandID(PackageGuids.guidToolbar, PackageIds.optionsCommand))
        {
        }

        private static void OnExecute(object sender, EventArgs e)
        {
            BreakpointGeneratorPackage.ShowOptionPage(typeof(BreakpointGeneratorOptions));
        }
    }
}