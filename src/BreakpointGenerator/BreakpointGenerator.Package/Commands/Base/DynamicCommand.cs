// //———————————————————————
// // <copyright file="DynamicCommand.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// // THe base class for any menu Command we are going to use in this extension. Provides basic infrastructure for menu items.
// // </summary>
// //———————————————————————

using System;
using System.ComponentModel.Design;
using Microsoft.ALMRangers.BreakpointGenerator.Extensions;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.ALMRangers.BreakpointGenerator.Commands.Base
{
    public abstract class DynamicCommand : OleMenuCommand
    {
        private static IServiceProvider _serviceProvider;
        private static BreakpointGeneratorPackage _beakpointGeneratorPackage;

        /// <summary>
        /// The ServiceProvider
        /// </summary>
        protected static IServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicCommand"/> class.
        /// </summary>
        /// <param name="provider">The service provider.</param>
        /// <param name="onExecute">The on execute delegate.</param>
        /// <param name="id">The command id.</param>
        public DynamicCommand(IServiceProvider provider, EventHandler onExecute, CommandID id)
            : base(onExecute, id)
        {
            this.BeforeQueryStatus += new EventHandler(OnBeforeQueryStatus);
            _serviceProvider = provider;
        }

        /// <summary>
        /// Called when [before query status].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void OnBeforeQueryStatus(object sender, EventArgs e)
        {
            OleMenuCommand command = sender as OleMenuCommand;

            command.Enabled = CanExecute(command);
            //command.Visible = CanExecute(command);
            //command.Supported = CanExecute(command);
        }

        /// <summary>
        /// Determines whether this instance can execute the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can execute the specified command; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool CanExecute(OleMenuCommand command)
        {
            return true;
            //return BreakpointGeneratorPackage.CommandsPage.DisabledCommands.SingleOrDefault(
            //    cmd => cmd.Guid.Equals(command.CommandID.Guid) &&
            //        cmd.ID.Equals(command.CommandID.ID)) == null;
        }

        /// <summary>
        /// CommandManagerService
        /// </summary>
        protected static BreakpointGeneratorPackage BreakpointGeneratorPackage
        {
            get
            {
                if (_beakpointGeneratorPackage == null)
                {
                    _beakpointGeneratorPackage = ServiceProvider.GetService<BreakpointGeneratorPackage>();
                }

                return _beakpointGeneratorPackage;
            }
        }
    }
}