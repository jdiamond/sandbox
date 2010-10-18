This is a pretty good page that describes creating custom tools:

http://www.drewnoakes.com/snippets/WritingACustomCodeGeneratorToolForVisualStudio/

After building, run install.cmd. This copies the DLL to another folder and
registers it from there. If you register it from bin\Debug, you won't be able
to build when Visual Studio loads it into memory!

Once Visual Studio is loading the assembly, you'll need to close it to
re-run install.cmd. You don't really need to do everything in install.cmd,
you just need to copy the DLL to the correct directory.

Once the tool is installed, you can a new instance of Visual Studio and
set a file's CustomTool to MyCustomTool.

When you save the file, it will generate a .cs file.
