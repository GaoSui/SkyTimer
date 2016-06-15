# SkyTimer
A cubing timer which supports StackMat and pure WCA scrambles.

##### MIT License

### Features
- Connect StackMat
- Use WCA official scramble
- Practice data plotting
- Unlimited data groups
- Real time statistics

### System Requirement
- Windows 7 SP1 or higher
- .NET Framwork 4.6.1 [Download Link](https://www.microsoft.com/net/download)
- Java runtime environment [Download Link](http://www.java.com)

### Setup Instruction
1. Make sure both .NET Framework 4.6.1 and Java are installed.
2. Download and unzip SkyTimer from the [release tab](https://github.com/GaoSui/SkyTimer/releases) or [度娘](http://pan.baidu.com/s/1dF7Faff).
3. Download TNoodle (WCA's scramble program).
[DownloadLink](https://www.worldcubeassociation.org/regulations/scrambles/tnoodle/TNoodle-WCA-0.11.1.jar)
4. Copy TNoodle-WCA-0.11.1.jar to SkyTimer's directory.
5. Double click SkyTimer.exe to run it. A TNoodle instance will also be launched automatically.

### Setup StackMat
1. Right click on the timer's digits to open system audio settings.
2. Set your StackMat as system default recording deivce.
3. Adjust recording volume to 50 or lower.
4. Turn off microphone boost.
5. Disable all sound effects.
6. Make sure the StackMat option is checked in SkyTimer.

I only tested this using a gen 4 mat.

### How to use
On the left is the list of groups. Add and remove a group using the plus and minus buttons below. Right click on a group to rename, clear or plot it. In the plot window you can click daily to show today's records or drag the sliders to show records in a certain amount of days or rounds while all data are displayed by default.

Select a record in your record list and DNF, +2 or delete it with the corresponding buttons.

Choose a scramble type in the drop down list. "fm" stands for fewest move. "ni" stands for no inspection.

Right click on the timer digits to choose wether to use StackMat or not. When StackMat is disabled, space bar will become enabled and vice versa. Check double precision to show only two digits, however triple precision is used internally.

Shortcuts:
- D: DNF last record
- 2: +2 last record
- x: Delete last record
- right arrow: Get next scramble

Your practice data are stored in SkyTimerData.bin.

##### If you have any problem using SkyTimer, please post it [here](https://github.com/GaoSui/SkyTimer/issues).
