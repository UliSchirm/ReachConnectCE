# ReachConnectCE
### Live transfer of coordinates from Reach RS+ GNSS receiver to Topcon FC-200 field computer via Bluetooth. Transformation from latitude/longitude to UTM. Further transfer of data to TopSURV 8 while performing resection of an optical total station.

![CF-200](https://user-images.githubusercontent.com/21182528/65868490-a1b3b300-e378-11e9-9165-5073fd31eb14.jpg)

This software runs on Windows CE based handheld devices and collects coordinates, which are transmitted via Bluetooth from Reach RS+ GNSS receivers. It also converts latitude/longitude to UTM north/east coordinates in real time and averages position and height values over any period of time. Averaged coordinates and height values can be copied to clipboard and pasted to any other software running on the same device. ReachConnectCE is optimized for Topcon FC-200 field computers and the TopSURV 8 surveying software. In particular it is suitable to perform the resection task with TopSURV 8 using an optical total station. At least two known coordinates are required to perform a resection of the total station on unknown points. Using ReachConnectCE such coordinates can conveniently be collected and transferred to TopSURV. After averaging coordinates und pressing the “New” button, a new point will automatically be generated in TopSURV and used for resection. Pressing the “Home” button on FC-200 toggles between TopSURV and ReachConnectCE window.

### System requirements
* ReachConnectCE.exe needs .NET Compact Framework 3.5 or newer to run. You need to install .NET Compact Framework 3.5 on FC-200 first. [Microsoft .NET Compact Framework](https://www.microsoft.com/en-us/download/details.aspx?id=65)

