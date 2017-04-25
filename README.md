# Breakpoint Generator

![Build status](https://almrangers.visualstudio.com/_apis/public/build/definitions/7f3cfb9a-d1cb-4e66-9d36-1af87b906fe9/80/badge)

<!-- Update the VS Gallery link after you upload the VSIX-->
Download this extension from the [VS Marketplace](https://marketplace.visualstudio.com/items?itemName=AndrewBHall-MSFT.BreakpointGenerator).

---------------------------------------

This extension enables you to generate breakpoints (and TracePoints) for any public method in your application.  This quickly allows you to learn the execution flow of new code bases and add debug time logging to your applications without the need to modify the source code.

> The extension only supports C# projects.

See the [change log](.github/CHANGELOG.md) for changes and road map.

## Using the extension

Once you download and install the tool, a new menu item "Generate Breakpoints" will appear under the Debug menu

![Menu](src/BreakpointGenerator/Screenshots/menu.png)

You can then choose which projects, files, and methods to create breakpoints for

![Toolwindow](src/BreakpointGenerator/Screenshots/toolwindow.png)

The tool will by default generate TracePoints but can be configured to use a different default message or create breakpoints instead. 

![Breakpoint Config](src/BreakpointGenerator/Screenshots/breakpoint-config.png)

More information can be found in the [blog post announcing the tool](http://blogs.msdn.com/b/visualstudioalm/archive/2015/11/19/breakpoint-generator-extension.aspx).

#Contributors

We thank the following contributors for this extension: [Jakob Ehn ](https://blogs.msdn.microsoft.com/willy-peter_schaub/2011/11/10/introducing-the-visual-studio-alm-rangers-jakob-ehn/) and [Utkarsh Shigihalli](https://blogs.msdn.microsoft.com/willy-peter_schaub/2013/07/05/introducing-the-visual-studio-alm-rangers-utkarsh-shigihalli/).

#Contribute
Contributions to this project are welcome. Here is how you can contribute:  

- Submit bugs and help us verify fixes  
- Submit pull requests for bug fixes and features and discuss existing proposals   

Please refer to [Contribution guidelines](.github/CONTRIBUTING.md) and the [Code of Conduct](.github/COC.md) for more details.