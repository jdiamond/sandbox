@echo off

echo This needs to be run from the Visual Studio Command Prompt!

set installdir=%~dp0install
set tooldir=%~dp0bin\Debug
set tooldll=CustomTool.dll

rd /q /s %installdir%
md %installdir%

copy %tooldir%\%tooldll% %installdir%>nul

pushd %installdir%

tlbexp /silent %tooldll%
regasm /silent /codebase %tooldll%

popd

reg import install.reg>nul

echo Done!
