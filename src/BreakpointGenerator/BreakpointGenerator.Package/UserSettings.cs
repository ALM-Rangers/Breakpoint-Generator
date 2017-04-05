// //———————————————————————
// // <copyright file="UserSettings.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// //  Utility class to hold the changes in options.
// // </summary>
// //———————————————————————

using System;
using Microsoft.ALMRangers.BreakpointGenerator.Options;

namespace Microsoft.ALMRangers.BreakpointGenerator
{
    public static class UserSettings
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static string TracepointExpression
        {
            get { return Options.LogMessaage; }
            set { Options.LogMessaage = value; }
        }

        public static bool ContinueExecution
        {
            get { return Options.ContinueExecution; }
            set { Options.ContinueExecution = value; }
        }

        public static BreakpointGeneratorOptions Options { get; set; }

        private static void OnOptionsChanged(object sender, OptionsChangedEventArgs e)
        {
        }
    }
}