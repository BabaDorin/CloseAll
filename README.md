# CloseAll
CloseAll is a small command-line utility that allows you to close all Windows applications.
> This utility can work in all versions of Windows operating system: Windows 9x/ME, Windows NT, Windows 2000, Windows XP, Windows Server 2003, Windows Vista, Windows Server 2008, Windows 7, Windows 8, and Windows 10.

## Platforms
* Windows

## Features
* Window closing
* Exception list
* Prevent focused window from closing.
* Ignore startup applications
* [TODO] Whitelist applications in a .txt file.

## Arguments
* **-except** _(**-e**)_

Will not close specific windows.

**Input** Accepts a list of parameters representing window titles.

**Usage** _closeall -except discord devenv   : All the processes will get killed, except Discord and Visual Studio_

‎

* **-igonre-startup** _(**-i-s**)_

Startup processes won't get killed.

**Usage** _closeall -ignore-startup_

‎

* **-help**

List available arguments and usage.

**Usage** _closeall -help_

‎

* **-nofocus** _(**-nf**)_

Focused window won't get killed

**Usage** _closeall -nofocus_

‎

[TODO] **-warn** _(**-w**)_

Windows that issue a warning before closure, will not be force-killed until confirmation.

**Usage** _closeall -warn_



### Important

This utility is still in development, and on execution it will force-kill every open window, **without any saving or confirmation prompt**, to prevent that , include **-w**.

On compilation , the utility will be placed in **%dir&/Bin/Debug** or **%dir&/Bin/Release**, there is also a Release version.

To use it from cmd, move add the utility to PATH.
