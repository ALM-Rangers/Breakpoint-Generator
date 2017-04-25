// //———————————————————————
// // <copyright file="CommandManagerService.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// // A class containing implementation ICommandManager service, required for hosting the service.
// // </summary>
// //———————————————————————

using System.Collections.Generic;
using System.Linq;
using Microsoft.ALMRangers.BreakpointGenerator.Options;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.ALMRangers.BreakpointGenerator.Services
{
    internal class CommandManagerService : ICommandManagerService, SCommandManagerService
    {
        private IList<OleMenuCommand> _registeredCommands;

        public CommandManagerService()
        {
            _registeredCommands = new List<OleMenuCommand>();
        }

        public void RegisterCommand(OleMenuCommand command)
        {
            if (_registeredCommands.SingleOrDefault(
                    cmd => cmd.CommandID.Guid.Equals(command.CommandID.Guid) &&
                           cmd.CommandID.ID.Equals(command.CommandID.ID)) == null)
            {
                _registeredCommands.Add(command);
            }
        }

        public void UnRegisterCommand(OleMenuCommand command)
        {
            if (_registeredCommands.SingleOrDefault(
                    cmd => cmd.CommandID.Guid.Equals(command.CommandID.Guid) &&
                           cmd.CommandID.ID.Equals(command.CommandID.ID)) != null)
            {
                _registeredCommands.Remove(command);
            }
        }

        public BreakpointGeneratorOptions BreakpointGeneratorOptions { get; set; }
    }
}