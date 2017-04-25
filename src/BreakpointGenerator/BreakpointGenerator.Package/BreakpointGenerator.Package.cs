namespace Microsoft.ALMRangers.BreakpointGenerator
{
    using System;
    
    /// <summary>
    /// Helper class that exposes all GUIDs used across VS Package.
    /// </summary>
    internal sealed partial class PackageGuids
    {
        public const string guidBreakpointGenerator_PackagePkgString = "d26b7824-0b3f-4a14-aaa0-0ae9853d272c";
        public const string guidShowBreakpointGeneratorToolWindowCommandString = "39466096-2290-4156-99e9-adae31afa277";
        public const string guidDebugMenuString = "c9dd4a58-47fb-11d2-83e7-00c04f9902c1";
        public const string guidSolutionMenuString = "d309f791-903f-11d0-9efc-00a0c911004f";
        public const string guidProjectMenuString = "d309f791-903f-11d0-9efc-00a0c911004f";
        public const string guidFileMenuString = "d309f791-903f-11d0-9efc-00a0c911004f";
        public const string guidToolbarString = "f3daef55-3432-4bbf-9c35-7de495a80406";
        public const string guidImagesString = "ed2cc9fb-0f01-46d8-adc2-a64bd91293bc";
        public static Guid guidBreakpointGenerator_PackagePkg = new Guid(guidBreakpointGenerator_PackagePkgString);
        public static Guid guidShowBreakpointGeneratorToolWindowCommand = new Guid(guidShowBreakpointGeneratorToolWindowCommandString);
        public static Guid guidDebugMenu = new Guid(guidDebugMenuString);
        public static Guid guidSolutionMenu = new Guid(guidSolutionMenuString);
        public static Guid guidProjectMenu = new Guid(guidProjectMenuString);
        public static Guid guidFileMenu = new Guid(guidFileMenuString);
        public static Guid guidToolbar = new Guid(guidToolbarString);
        public static Guid guidImages = new Guid(guidImagesString);
    }
    /// <summary>
    /// Helper class that encapsulates all CommandIDs uses across VS Package.
    /// </summary>
    internal sealed partial class PackageIds
    {
        public const int grpidBreakpointGeneratorGroupDebugMenu = 0x1010;
        public const int grpidBreakpointGeneratorGroup = 0x1020;
        public const int cmdidBreakpointGenerator = 0x4121;
        public const int cmdidBreakpointGeneratorDebugMenu = 0x4122;
        public const int VSDebugMenu = 0x0401;
        public const int SolutionContextMenu = 0x0413;
        public const int ProjectContextMenu = 0x0402;
        public const int FileContextMenu = 0x0430;
        public const int toolbarGroup = 0x07D0;
        public const int toolbar = 0x07D1;
        public const int expandAllCommand = 0x07D2;
        public const int collapseAllCommand = 0x07D3;
        public const int optionsCommand = 0x07D4;
        public const int bmpBreakpoint = 0x0001;
        public const int bmpExpand = 0x0002;
        public const int bmpCollapse = 0x0003;
        public const int bmpOptions = 0x0004;
    }
}
