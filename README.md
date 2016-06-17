# SkyTimer
A cubing timer which supports StackMat and pure WCA scrambles.

##### MIT License

## Download Link 下载链接
- [Github] (https://github.com/GaoSui/SkyTimer/releases)
- [百度网盘](http://pan.baidu.com/s/1dF7Faff)

##### [中文使用说明](README.zh-CN.md)

### System Requirement
- Windows 10 is recommended
- .NET Framwork 4.6.1 [Download Link](https://www.microsoft.com/net/download)
- Java runtime environment [Download Link](http://www.java.com)

### Setup Instruction
1. Make sure both .NET Framework 4.6.1 and Java are installed.
2. Download TNoodle (WCA's scramble program) and copy it to SkyTimer's directory. [DownloadLink](https://www.worldcubeassociation.org/regulations/scrambles/tnoodle/TNoodle-WCA-0.11.1.jar) 
3. Double click SkyTimer.exe to run it. A TNoodle instance will also be launched.

### How to use
##### Timing
Right click on the timer digits to choose wether to use StackMat or not. When StackMat is disabled, space bar will become enabled and vice versa. Check double precision to show only two digits, however triple precision is used internally.
##### Penalty
Select a record in your record list and DNF, +2 or delete it with the corresponding buttons.
##### Scramble
Choose a scramble type in the drop down list. "fm" stands for fewest move. "ni" stands for no inspection.
##### Grouping
On the left is the list of groups. Add and remove a group using the plus and minus buttons below. Right click on a group to rename, clear or plot it.
##### Plotting
In the plot window you can click daily to show today's records or drag the sliders to show records in a certain amount of days or rounds while all data are displayed by default.
##### Statistic Output
Click on a particular statistic type to show a dedicated output window about your records. Choose whether to include scramble or not in the groups' context menu. The output is copied to your clip board automatically. 
##### Copy Scramble
Right click on a record to copy its corresponding scramble.
##### Shortcuts:
- D: DNF last record
- 2: +2 last record
- x: Delete last record
- right arrow: Get next scramble

Your practice data are stored in SkyTimerData.bin.

##### If you have any problem using SkyTimer, please post it [here](https://github.com/GaoSui/SkyTimer/issues).

### About StackMat Connection
##### Setup steps:
1. Right click on the timer's digits to open system audio settings.
2. Set your StackMat as system default recording deivce.
3. Adjust recording volume to 50 or lower.
4. Turn off microphone boost.
5. Disable all sound effects.
6. Make sure the StackMat option is checked in SkyTimer.

I only tested this using a gen 4 mat. 

If SkyTimer does not respond to your StackMat at all, there must be something wrong in you system audio setting or you have active sound effects. If your sound card has filters which are impossible to be disabled, StackMat connection will not work properly. Using a cheap usb sound card could be a work around.

If SkyTimer is responding incorrectly, chances are your StackMat is using an unsupported signal format. Contact me if you are willing to help improve this software.

##### Simple diagnostic
Right click on the timer and check "Diagnostic", SkyTimer will write the next 10 seconds' signal into a file called diagnostic.wav. Open this file with any audio editor, for example, Audacity, and you can observe the waveform. It should be something like this:
