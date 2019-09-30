# ReachConnectCE
### Live transfer of coordinates from Reach RS+ to Topcon FC-200 field computer via Bluetooth. Transformation from Latitude/Longitude to UTM. Further transfer of data to TopSURV 8 while performing resection of an optical total station.

This software runs on Windows CE based handheld devices and collects coordinates, which are transmitted via Bluetooth from Reach RS+ GNSS receivers. It also converts latitude/longitude to UTM north/east coordinates in real time and averages position and height values over any period of time. Averaged coordinates and height values can be copied to clipboard and transferred to any other software running on the same device. ReachConnectCE is optimized for Topcon FC-200 field computers and the TopSURV 8 surveying software. In particular it is suitable to perform the resection task with TopSURV 8 using an optical total station. At least two known coordinates are required to perform a resection of the total station on unknown points. Using ReachConnectCE such coordinates can conveniently be collected and transferred to TopSURV. After averaging coordinates und pressing the “Copy” button, values are stored to the clipboard. Pressing the “Home” button on FC-200 brings TopSURV window to front. Using the software input panel and ctrl+v command pastes the value from clipboard to any selected input field in topSURV.
