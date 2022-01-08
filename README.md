# CloseAll
CloseAll is a small command-line utility that allows you to close all Windows applications.

**To use it from cmd, add the utility to PATH.**

> This utility can work on all versions of Windows operating system: Windows 9x/ME, Windows NT, Windows 2000, Windows XP, Windows Server 2003, Windows Vista, Windows Server 2008, Windows 7, Windows 8, and Windows 10.

## Platforms
* Windows

## Features
* Window closing
* Exception list
* Whitelist applications in a .txt file 
  * Path: `Documents/closeall_whitelist.txt. If the file isn't there, feel free to create it manually and make sure to not misspell the name. Each whitelisted process name has to be written from new line, and it is case insensitive)
* [TODO] Ignore startup applications

## Arguments
* **-help**

List availalbe commands and their usage, along with examples.

**Usage** _closeall -help_

‎

* **-list** _(**-ls**)_

List all processes to be killed before applying any filters.

**Usage** _closeall -ls

‎

* **-simulate** _(**-sim**)_

Simulate killing processes. It helps to visualize if the filter works as expected. 

**Usage** _closeall -sim

‎

* **-except** _(**-e**)_

Will not close specific windows / processes.

**Input** Accepts a list of parameters representing process names.

**Usage** _closeall -except discord devenv   : All the processes will get killed, except Discord and Visual Studio. NOTE: devend is Visual Studio's process name_


## Important

This utility is still in development, and on execution it will force-kill every open window, **without any saving or confirmation prompt**.
