// //———————————————————————
// // <copyright file="BreakPointGeneratorWindow.xaml.cs">
// // This code is licensed under the MIT License.
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF 
// // ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// // TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
// // PARTICULAR PURPOSE AND NONINFRINGEMENT.
// // </copyright>
// // <summary>
// //  A user control class to host it in the toolwindow.
// // </summary>
// //———————————————————————

using System;
using System.Windows.Controls;
using Microsoft.ALMRangers.BreakpointGenerator.ViewModels;

namespace Microsoft.ALMRangers.BreakpointGenerator.ToolWindows
{
    /// <summary>
    /// Interaction logic for MyControl.xaml
    /// </summary>
    public partial class BreakPointGeneratorWindow : UserControl
    {
        private IServiceProvider _serviceProvider;


        public BreakPointGeneratorWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            DataContext = BreakpointGeneratorToolWindowViewModel.Instance;
        }
    }
}