# UnityDarkSkin

## About

THIS PROGRAM IS NOT CURRENTLY WORKING

Use this command line tool to patch unity.exe for activating its dark theme.

Changes can be reverted by launching the program twice
The unity.exe file will be backed up into unity.exe.BAK on first launch.

** This is not a CRACK **
** All it does is change your theme for accessibility purposes. You do not gain any other premium features. **

Based on [UnityDarkSkin from Glushenko](https://github.com/Gluschenko/UnityDarkSkin)

Basically, the program just replaces some hex values in the unity.exe file.

- for the 2018.3 version and more on an x64 system:
  84C0750833C04883C4305BC38B034883C4305BC3 hex values are replaced by :
  84C0740833C04883C4305BC38B034883C4305BC3﻿

- for the 2018.3 version and more on an x86 system:
 **TODO**

- for the 2018.2 version and less on an x64 system:
 **TODO**

- for the 2018.2 version and less on an x86 system:
 **TODO**

## Usage

1. Compile. The executable is located into the bin\Debug (or bin\Release) sub-folder. Build has be tested using Jetbrain Rider IDE, but should work with visual studio.
2. (optional) copy UnityDarkSkin.exe to folder with Unity.exe
3. (optional) drag unity.exe file on the UnityDarkSkin.exe executable (in that case, the executable must have administrator rights)
4. OR (optional) run UnityDarkSkin.exe as administrator.
5. Answer to program's questions and (optional) Give the unity.exe file path when asked by the program. Current folder will be used by default if the path is not given.

## Notes
- Now compatible with unity 2018.3 and 2019 (and perhaps higher versions, but untested)
- tested on 2018.3 (x64), 2019.1 beta (x64)
- no tested on 5.3, 5.4, 5.6, 2017.2, 2017.4, 2018.2

![](Media/Preview.jpg)
![](Media/LightSkin.jpg)
![](Media/DarkSkin.jpg)
