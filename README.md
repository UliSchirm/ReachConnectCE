# ReachConnectCE
### Live transfer of coordinates from Reach RS+ GNSS receiver to Topcon FC-200 field computer via Bluetooth. Transformation from latitude/longitude to UTM. Further transfer of data to TopSURV 8 while performing resection of an optical total station.

![CF-200](https://user-images.githubusercontent.com/21182528/65868490-a1b3b300-e378-11e9-9165-5073fd31eb14.jpg)

This software runs on Windows CE based handheld devices and collects coordinates, which are transmitted via Bluetooth from Reach RS+ GNSS receivers. It also converts latitude/longitude to UTM north/east coordinates in real time and averages position and height values over any period of time. Averaged coordinates and height values can be copied to clipboard and pasted to any other software running on the same device.

<img align="right" src="https://user-images.githubusercontent.com/21182528/65906050-0a734d80-e3c2-11e9-8821-81d46e5e0385.jpeg">
ReachConnectCE is optimized for Topcon FC-200 field computers and the TopSURV 8 surveying software. In particular it is suitable to perform the resection task with TopSURV 8 using an optical total station. The coordinates of an unknown point at which the total station is set can be calculated by resection when at least two points of a known position are observed. Using a Reach RS+ GNSS receiver and ReachConnectCE such points can conveniently be collected and transferred to TopSURV. After averaging coordinates und pressing the “New” button, a new point will automatically be generated in TopSURV and used for resection. Pressing the “Home” button on FC-200 toggles between TopSURV and ReachConnectCE window.

### System requirements
* ReachConnectCE.exe needs .NET Compact Framework 3.5 or newer to run. You need to install .NET Compact Framework 3.5 on FC-200 first. [Microsoft .NET Compact Framework 3.5](https://www.microsoft.com/en-us/download/details.aspx?id=65)
* Your device needs an active Bluetooth radio.

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
* After “Stop” has been pressed, averaging will freeze und you get the opportunity to copy the result for further usage. Pressing “Copy” next to the averaged value transfers it to the clipboard. You can then paste it to another application running on the same device. If you use TopSURV 8 and you want to create a new point while performing a resection, copy and paste can be automated by pressing the “New” button, which is very convenient und saves some time. Just make sure, you are in the TopSURV resection dialog before starting the macro. By pressing the + or – button you can change the point number, which will be created in TopSURV.


