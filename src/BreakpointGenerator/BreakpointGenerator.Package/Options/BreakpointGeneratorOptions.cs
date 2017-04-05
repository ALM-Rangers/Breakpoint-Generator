// //———————————————————————
// // <copyright file="BreakpointGeneratorOptions.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// // A dialog page class to hold the options.
// // </summary>
// //———————————————————————

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.ALMRangers.BreakpointGenerator.Options
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class BreakpointGeneratorOptions : DialogPage
    {
        public BreakpointGeneratorOptions()
        {
            ContinueExecution = true;
            LogMessaage = "$FUNCTION called by $CALLER";
        }


        [Category("General")]
        [DisplayName(@"Log Message")]
        [Description("This message will appear in IntelliTrace window. e.g. $Function: The value of x.y is {x.y}")]
        public string LogMessaage { get; set; }

        [Category("General")]
        [DisplayName(@"Continue Execution")]
        [Description("If true, do not break when breakpoint is hit")]
        public bool ContinueExecution { get; set; }

        protected override void OnApply(PageApplyEventArgs e)
        {
            base.OnApply(e);
            if (OptionsChanged != null)
            {
                var optionsEventArg = new OptionsChangedEventArgs
                {
                    BreakpointGeneratorOptions = this
                };
                OptionsChanged(this, optionsEventArg);
            }
        }

        public event EventHandler<OptionsChangedEventArgs> OptionsChanged;
    }
}