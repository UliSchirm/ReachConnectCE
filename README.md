# ReachConnectCE
### Live transfer of coordinates from Reach RS+ GNSS receiver to Topcon FC-200 field computer via Bluetooth. Transformation from latitude/longitude to UTM. Further transfer of data to TopSURV 8 while performing resection of an optical total station.

![CF-200](https://user-images.githubusercontent.com/21182528/65868490-a1b3b300-e378-11e9-9165-5073fd31eb14.jpg)

This software runs on Windows CE based handheld devices and collects coordinates, which are transmitted via Bluetooth from Reach RS+ GNSS receivers. It also converts latitude/longitude to UTM north/east coordinates in real time and averages position and height values over any period of time. Averaged coordinates and height values can be copied to clipboard and pasted to any other software running on the same device.

<img align="right" src="https://user-images.githubusercontent.com/21182528/65906050-0a734d80-e3c2-11e9-8821-81d46e5e0385.jpeg">
ReachConnectCE is optimized for Topcon FC-200 field computers and the TopSURV 8 surveying software. In particular it is suitable to perform the resection task with TopSURV 8 using an optical total station. The coordinates of an unknown point at which the total station is set can be calculated by resection when at least two points of a known position are observed. Using a Reach RS+ GNSS receiver and ReachConnectCE such points can conveniently be collected and transferred to TopSURV. After averaging coordinates und pressing the “New” button, a new point will automatically be generated in TopSURV and used for resection. Pressing the “Home” button on FC-200 toggles between TopSURV and ReachConnectCE window.

### System requirements
* ReachConnectCE.exe needs .NET Compact Framework 3.5 or newer to run. You need to install .NET Compact Framework 3.5 on FC-200 first. [Microsoft .NET Compact Framework 3.5](https://www.microsoft.com/en-us/download/details.aspx?id=65)
* Your device needs an active Bluetooth radio.
* Emlid Reach RS+ GNSS receiver (Although not tested, a Reach RS2 receiver will most probably be just fine as well).

### Installing and Setting Up ReachConnectCE
* Copy all four files from folder “Program Files Windows CE” to any location on the field computer, for example “C:\Program Files\ReachConnectCE\”.
* Use ReachView app, which is provided by Emlid, to determine the MAC address of Reach RS+ Bluetooth radio. Go to the Bluetooth tab, set Bluetooth to ON and check “Always discoverable” ON. The device MAC is then shown in the format 12:34:56:78:90:AB. Copy the MAC address and paste it to the file “mac.txt”. It is used for pairing Reach RS+ and the field computer.
* Go to “Position output” tab in ReachView, check Output 1 ON via Bluetooth, output format: LLH.
* Run ReachConnectCE by clicking the file "ReachConnectCE.exe".

### Using ReachConnectCE

<img align="right" src="https://user-images.githubusercontent.com/21182528/65905332-aa2fdc00-e3c0-11e9-8dcd-bec897433348.jpg">

* When starting ReachConnectCE, first another software will launch in background, it is called “HookKeys”. HookKeys enables you to conveniently toggle between ReachConnectCE and TopSURV by simply pressing the “Home” button on FC-200. It also shows/hides the software input panel (keyboard) by pressing the “Down” button.
* After ReachConnectCE is started, it will try to establish a Bluetooth connection to Reach RS+, which is defined by the MAC address in “mac.txt”.
* When Bluetooth connection is established, UTM zone is shown, as well as the solution status “Singe”, “Float” or “Fix”. Coordinates will be transformed to UTM, which is most common at least among Germany when performing surveying tasks. UTM coordinates and heights are shown in live view.
* If you want to improve accuracy of position data, you can average coordinates and heights over any period of time. To do so, press the “Start” button. Now averaged position data is presented and a timer shows the averaging time in seconds. When the solution is “Fix”, no improvement will be achieved after a period of about 40 seconds, which you can tell from the third decimal place (mm) not changing anymore.

<img align="right" src="https://user-images.githubusercontent.com/21182528/65902356-879ac480-e3ba-11e9-8b6a-19cd81ec447f.jpg">

* If a “Fix” position is lost and solution downgrades to “Float” or “Singe”, averaging will stop and a warning is shown. This prevents collecting position data with a quality worse than “Fix”.
* After “Stop” has been pressed, averaging will freeze und you get the opportunity to copy the result for further usage. Pressing “Copy” next to the averaged value transfers it to the clipboard. You can then paste it to another application running on the same device. If you use TopSURV 8 and want to create a new point while performing a resection, copy and paste can be automated by pressing the “New” button, which is very convenient und saves some time. Just make sure, you are in the TopSURV resection dialog before starting the macro. By pressing the + or – button you can change the point number, which will be created in TopSURV.

## Information for developers
* If you want to customize this software for any device that runs Windows CE, you need an IDE which supports the Compact Framework 3.5. The latest IDE from Microsoft that supported Windows CE was VisualStudio 2008 Professional. Unfortunately, the download is no longer available at Microsoft.com. But you still can find a 90-day trial version of Visual Studio 2008 Pro around the Internet, you want to search a file named: “VS2008ProEdition90dayTrialENUX1435622.iso”.

<img align="right" src="https://user-images.githubusercontent.com/21182528/65940985-34605a80-e42a-11e9-9642-652b856e0aba.jpg">
                        
Many older books dealing with Visual C# 2008 contained a CD ROM with the 90-day trial version of Visual Studio 2008 Pro. A German version for example can be found on a CD ROM, which was sold with the Book “Visual C# 2008” (Addison-Wesley, ISBN-13: 978-3827326416).
* The toughest part in this software project was getting the Bluetooth stack work in Compact Framework 3.5. According my research there is basically just on option: The use of 32feet.NET.3.5.0 library. It is pretty well documented and can be downloaded from NuGet: [32feet.NET](https://www.nuget.org/packages/32feet.NET)
The package contains a file named “\32feet.net.3.5.0\lib\net-cf\InTheHand.Net.Personal.dll”. After installation of VisualStudion 2008 copy this file to: “C:\Programme\Microsoft.NET\SDK\CompactFramework\v3.5\WindowsCE\InTheHand.Net.Personal.dll”. Now you can reference InTheHand.Net.Personal.dll in your VisualStudio 2008 project.
An important reference for me to understanding how InTheHand.Net.Personal works was a German article which is still available on heise.de: [Blaues im Netz](https://www.heise.de/ix/artikel/Blaues-im-Netz-794702.html)
* Another resource that I used was the key hook class from hjgode.de which has been very helpful (Thank you J. for the great support!) [Hooking the keyboard message queue in compact framework code](http://www.hjgode.de/wp/2009/12/04/hooking-the-keyboard-message-queue-in-compact-framework-code). This class provides a key hook, which catches any hardware key press on Windows CE devices. I use the “Home” button to toggle between ReachConnectCE and TopSURV window and the “Down” key to show/hide the software input panel. Unfortunately, it turned out, that the key hook stops after some time when this feature is integrated in ReachConnectCE for some reasons, that I could not find out. I tried to run the key hook in a separate thread, which did not work at all. So, I put the key cook into another software called “KeyHooks.exe” which gets an own process when ReachConnectCE is started. This way key hooks are reliably recognized. 



