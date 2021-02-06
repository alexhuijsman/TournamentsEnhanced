# TournamentsEnhanced
Tournaments Enhanced mod for Mount &amp; Blade II: Bannerlord

https://www.nexusmods.com/mountandblade2bannerlord/mods/1449

Developed using VScode and Ubuntu on WSL for Windows 10, and Visual Studio 2019 (for attaching debugger to Bannerlord).

Make sure PATH_TO_BANNERLORD environment variable is pointing to your local Bannerlord install dir. This is needed to build against the Bannerlord DLLs and to package releases.
Example .bashrc entry:
export PATH_TO_BANNERLORD="/mnt/c/Program Files (x86)/Steam/steamapps/common/Mount & Blade II Bannerlord/"

Install the workspace-recommended vscode extensions, then click Watch in vscode status bar to view unit test code coverage in source files.

Run Cache Bannerlord DLLs to copy the current version of bannerlord DLLs to a local folder.
To set the target version to build against, run Switch to Cached Bannerlord DLLs.