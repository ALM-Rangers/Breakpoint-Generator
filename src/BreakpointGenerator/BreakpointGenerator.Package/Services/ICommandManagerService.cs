// //———————————————————————
// // <copyright file="ICommandManagerService.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// //  An interface required to host the service.
// // </summary>
// //———————————————————————

using System.Runtime.InteropServices;
using Microsoft.ALMRangers.BreakpointGenerator.Options;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.ALMRangers.BreakpointGenerator.Services
{
    [Guid("A12C2602-DF73-44AA-B8E8-B262810DB729")]
    [ComVisible(true)]
    internal interface ICommandManagerService
    {
        BreakpointGeneratorOptions BreakpointGeneratorOptions { get; set; }

        /// <summary>
        /// Registers the command.
        /// </summary>
        /// <param name="command">The command.</param>
        void RegisterCommand(OleMenuCommand command);

        /// <summary>
        /// Uns the register command.
        /// </summary>
        /// <param name="command">The command.</param>
        void UnRegisterCommand(OleMenuCommand command);
    }
}