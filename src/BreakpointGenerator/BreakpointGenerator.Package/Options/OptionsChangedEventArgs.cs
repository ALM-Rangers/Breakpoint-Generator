// //———————————————————————
// // <copyright file="OptionsChangedEventArgs.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// // EventArgs class to pass the options on change of the options.
// // </summary>
// //———————————————————————

namespace Microsoft.ALMRangers.BreakpointGenerator.Options
{
    public class OptionsChangedEventArgs
    {
        public BreakpointGeneratorOptions BreakpointGeneratorOptions { get; set; }
    }
}