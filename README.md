# ReachConnectCE
### Live transfer of coordinates from Reach RS+ GNSS receiver to Topcon FC-200 field computer via Bluetooth. Transformation from latitude/longitude to UTM. Further transfer of data to TopSURV 8 while performing resection of an optical total station.

![CF-200](https://user-images.githubusercontent.com/21182528/65868490-a1b3b300-e378-11e9-9165-5073fd31eb14.jpg)

This software runs on Windows CE based handheld devices and collects coordinates, which are transmitted via Bluetooth from Reach RS+ GNSS receivers. It also converts latitude/longitude to UTM north/east coordinates in real time and averages position and height values over any period of time. Averaged coordinates and height values can be copied to clipboard and pasted to any other software running on the same device. ReachConnectCE is optimized for Topcon FC-200 field computers and the TopSURV 8 surveying software. In particular it is suitable to perform the resection task with TopSURV 8 using an optical total station. At least two known coordinates are required to perform a resection of the total station on unknown points. Using ReachConnectCE such coordinates can conveniently be collected and transferred to TopSURV. After averaging coordinates und pressing the “New” button, a new point will automatically be generated in TopSURV and used for resection. Pressing the “Home” button on FC-200 toggles between TopSURV and ReachConnectCE window.

### System requirements
* ReachConnectCE.exe needs .NET Compact Framework 3.5 or newer to run. You need to install .NET Compact Framework 3.5 on FC-200 first. [Microsoft .NET Compact Framework 3.5](https://www.microsoft.com/en-us/download/details.aspx?id=65)
* Your device needs an active Bluetooth radio.

### Installing and Setting Up ReachConnectCE
* Copy all four files from folder “Program Files Windows CE” to any location on the field computer, for example “C:\Program Files\ReachConnectCE\”.
* Use ReachView app, which is provided by Emlid, to determine the MAC address of Reach RS+ Bluetooth radio. Go to the Bluetooth tab, set Bluetooth to ON and check “Always discoverable” ON. The device MAC is then shown in the format 12:34:56:78:90:AB. Copy the MAC address and paste it to the file “mac.txt”. It is used for pairing Reach RS+ and the field computer.
* Go to “Position output” tab in ReachView, check Output 1 ON via Bluetooth, output format: LLH.
* Run ReachConnectCE by clicking the file "ReachConnectCE.exe".

### Using ReachConnectCE

![HC_20190930_090152](https://user-images.githubusercontent.com/21182528/65902356-879ac480-e3ba-11e9-8b6a-19cd81ec447f.jpg)
* When starting ReachConnectCE, first another software will launch in background, it is called “HookKeys”. HookKeys enables you to conveniently toggle between ReachConnectCE and TopSURV by simply pressing the “Home” button on FC-200. It also shows/hides the software input panel (keyboard) by pressing the “Down” button.
* After ReachConnectCE is started, it will try to establish a Bluetooth connection to Reach RS+, which is defined by the MAC address in “mac.txt”.
* When Bluetooth connection is established, coordinates will be transformed to UTM, which is most common at least among Germany when performing surveying tasks. UTM coordinates and heights are shown in live view. UTM zone is also calculated and shown, as well as the solution status “Singe”, “Float” or “Fix”.

