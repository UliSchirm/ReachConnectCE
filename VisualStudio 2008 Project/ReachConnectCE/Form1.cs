using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Reflection;
using InTheHand.Net;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using System.Runtime.InteropServices;
using System.Diagnostics;

// Version V18
namespace ReachConnectCE
{
    public partial class Form1 : Form
    {
        #region Variables declaration

        enum TimerState { ResetToZero, Running, Stopped } // Timer states used to change functions of timer button
        TimerState timerState = TimerState.ResetToZero;
        byte bytQualityOld = 0; // Used to show message box if "Fix" is lost while averaging position 
        int intSecond = 0; // Used to count seconds while averaging position
        int intNewPoint = 101; // Numer use for new point in TopSURV
        int intMessageNumber = 1; // Counts amount of messages received from Reach RS+, used as divider for accumulated position and height values
        double dblAverageNorth, dblAverageEast, dblAverageHeight; // Accumulated position and height values

        #endregion

        #region P/Invoke coredll.dll

        static Dictionary<string, IntPtr> visibleWindows = new Dictionary<string, IntPtr>();
        private delegate int WNDENUMPROC(IntPtr hwnd, uint lParam);

        [DllImport("coredll.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("coredll.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("coredll.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("coredll.dll")]
        private static extern int EnumWindows(WNDENUMPROC lpEnumWindow, uint lParam);

        [DllImport("coredll.dll")]
        private static extern bool IsWindowVisible(IntPtr hwnd);

        [DllImport("coredll.dll")]
        private static extern IntPtr GetParent(IntPtr hwnd);

        [DllImport("coredll.dll")]
        private static extern bool GetWindowText(IntPtr hwnd, StringBuilder lpString, int nMaxCount);

        [DllImport("coredll.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("coredll.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint lpdwProcessId);

        [DllImport("coredll.dll")]
        private static extern int GetModuleFileName(UIntPtr hModule, StringBuilder lpFilename, int nSize);

        [DllImport("coredll.dll")]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("coredll.dll")]
        private static extern bool TerminateProcess(uint hProcess, uint ExitCode);

        private const int KEYEVENTF_KEYUP = 0x0002;
        private const int HWND_FOREGROUND = -1;
        private const uint KEY_STATE_DOWN = 0x0080;
        private const uint KEY_SHIFT_NO_CHARACTER = 0x00010000;
        private const uint KEY_SHIFT_ANY_CONTROL = 0x40000000;
        const int GWL_EXSTYLE = -20;
        const uint WS_EX_TOOLWINDOW = 0x0080;

        #endregion

        public Form1()
        {
            InitializeComponent();

            // Maximize window
            this.WindowState = FormWindowState.Maximized;

            // Create and start bluetooth thread
            Thread t = new Thread(new ThreadStart(BluetoothThread));
            t.Start();
            t.IsBackground = true;
        }

        private void BluetoothThread()
        {
            Bluetooth data = new Bluetooth(); // Instanciate object from class "Bluetooth"
            data.BluetoothStatus += ShowBluetoothStatus; // Subscribe to the event "ShowBluetoothStatus"
            data.NewDataAvailable += ShowData; // Subscribe to the event "ShowData"
            data.ReceiveData(); // Start event "ReceiveData" from object "data"
        }

        private void ShowBluetoothStatus(object sender, BluetoothEventArgs e)
        {
            if (e.bolBluetoothConnected)
            {
                lblBluetoothConnection.Invoke((Action)delegate { lblBluetoothConnection.Text = "Verbunden mit Reach RS+"; });
                picIconGrey.Invoke((Action)delegate { picIconGrey.Visible = false; });
                picIconBlue.Invoke((Action)delegate { picIconBlue.Visible = true; });
            }

            if (e.bolDataAvailable && bytQualityOld == 0)
            {
                lblRealtimeAverage.Invoke((Action)delegate { lblRealtimeAverage.Text = "UTM Koordinaten: Echtzeit"; });
                btnTimer.Invoke((Action)delegate { btnTimer.Enabled = true; });
            }
        }

        private void ShowData(object sender, DataEventArgs e)
        {
            lblZone.Invoke((Action)delegate { lblZone.Text = e.strZone + " " + e.strHemisphere; });

            // Show realtime position/height
            if (timerState == TimerState.ResetToZero)
            {
                lblEast.Invoke((Action)delegate { lblEast.Text = e.dblEast.ToString("0.000"); });
                lblNorth.Invoke((Action)delegate { lblNorth.Text = e.dblNorth.ToString("0.000"); });
                lblHeight.Invoke((Action)delegate { lblHeight.Text = e.dblHeight.ToString("0.000"); });
            }

            // Show averaged position/height
            if (timerState == TimerState.Running)
            {
                // Calculate new average once new data is received
                dblAverageNorth = dblAverageNorth + e.dblNorth;
                dblAverageEast = dblAverageEast + e.dblEast;
                dblAverageHeight = dblAverageHeight + e.dblHeight;

                lblEast.Invoke((Action)delegate { lblEast.Text = (dblAverageEast / intMessageNumber).ToString("0.000"); });
                lblNorth.Invoke((Action)delegate { lblNorth.Text = (dblAverageNorth / intMessageNumber).ToString("0.000"); });
                lblHeight.Invoke((Action)delegate { lblHeight.Text = (dblAverageHeight / intMessageNumber).ToString("0.000"); });

                intMessageNumber++;
            }

            // Show Solution status
            switch (e.bytQuality)
            {
                case 1:
                    lblQuality.Invoke((Action)delegate { lblQuality.Text = "Fix"; });
                    lblQuality.Invoke((Action)delegate { lblQuality.ForeColor = Color.Green; });
                    break;
                case 2:
                    lblQuality.Invoke((Action)delegate { lblQuality.Text = "Float"; });
                    lblQuality.Invoke((Action)delegate { lblQuality.ForeColor = Color.Orange; });
                    break;
                case 5:
                    lblQuality.Invoke((Action)delegate { lblQuality.Text = "Single"; });
                    lblQuality.Invoke((Action)delegate { lblQuality.ForeColor = Color.Red; });
                    break;
                default:
                    lblQuality.Invoke((Action)delegate { lblQuality.Text = ""; });
                    break;
            }

            // Show message box if "Fix" is lost while collecting position
            if (e.bytQuality != 1 && bytQualityOld == 1 && timer1.Enabled == true)
            {
                timer1.Enabled = false;
                timerState = TimerState.Stopped;
                btnTimer.Text = "Reset";
                btnCopyNorth.Enabled = true;
                btnCopyEast.Enabled = true;
                btnCopyHeight.Enabled = true;
                MessageBox.Show("Fix verloren! Timer wurde gestoppt.", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            bytQualityOld = e.bytQuality;
        }

        private void btnCopyEast_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(lblEast.Text);
        }

        private void btnCopyNorth_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(lblNorth.Text);
        }

        private void btnCopyHeight_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(lblHeight.Text);
        }

        private void btnTimer_Click(object sender, EventArgs e)
        {
            if (timerState == TimerState.ResetToZero)
            {
                timer1.Enabled = true;
                timerState = TimerState.Running;
                btnTimer.Text = "Stop";
                lblRealtimeAverage.Text = "UTM Koordinaten: Mitteln";
            }

            else if (timerState == TimerState.Running)
            {
                timer1.Enabled = false;
                timerState = TimerState.Stopped;
                btnTimer.Text = "Reset";
                btnCopyEast.Enabled = true;
                btnCopyNorth.Enabled = true;
                btnCopyHeight.Enabled = true;
                btnPlus.Enabled = true;
                btnMinus.Enabled = true;
                btnTopSURV.Enabled = true;
            }

            else if (timerState == TimerState.Stopped)
            {
                timerState = TimerState.ResetToZero;
                intSecond = 0;
                intMessageNumber = 1;
                dblAverageNorth = 0;
                dblAverageEast = 0;
                dblAverageHeight = 0;
                lblSecond.Text = "Sekunden: 0";
                btnTimer.Text = "Start";
                btnCopyEast.Enabled = false;
                btnCopyNorth.Enabled = false;
                btnCopyHeight.Enabled = false;
                btnPlus.Enabled = false;
                btnMinus.Enabled = false;
                btnTopSURV.Enabled = false;
                lblRealtimeAverage.Text = "UTM Koordinaten: Echtzeit";
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            intNewPoint++;
            lblPointNumber.Text = "Neuen Punkt Nr. " + intNewPoint.ToString();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            intNewPoint--;
            lblPointNumber.Text = "Neuen Punkt Nr. " + intNewPoint.ToString();
        }

        private void btnTopSURV_Click(object sender, EventArgs e)
        {
            ToggleWindow();
            Thread.Sleep(300);
            SendTap(14, 15); // Open menu top left corner
            Thread.Sleep(300);
            SendTap(14, 35); // Punkte bearbeiten
            Thread.Sleep(1000);
            SendTap(180, 275); // Neu
            Thread.Sleep(800);
            SendTap(218, 72); // Punkt
            Thread.Sleep(300);
            SendTap(100, 42); // Delete
            Thread.Sleep(300);
            inputPanel1.Enabled = true;
            Thread.Sleep(300);
            Clipboard.SetDataObject(intNewPoint.ToString());
            SendTap(11, 312); // Ctrl
            SendTap(94, 295); // V
            Thread.Sleep(100);
            SendTap(215, 20); // Enter
            Thread.Sleep(500);
            SendTap(210, 196); // Ost
            Thread.Sleep(300);
            SendTap(100, 42); // Delete
            inputPanel1.Enabled = true;
            Thread.Sleep(300);
            Clipboard.SetDataObject(lblEast.Text);
            SendTap(11, 312); // Ctrl
            SendTap(94, 295); // V
            Thread.Sleep(100);
            SendTap(215, 20); // Enter
            inputPanel1.Enabled = false;
            Thread.Sleep(300);
            SendTap(210, 220); // Nord
            Thread.Sleep(300);
            SendTap(100, 42); // Delete
            inputPanel1.Enabled = true;
            Thread.Sleep(300);
            Clipboard.SetDataObject(lblNorth.Text);
            SendTap(11, 312); // Ctrl
            SendTap(94, 295); // V
            Thread.Sleep(100);
            SendTap(215, 20); // Enter
            inputPanel1.Enabled = false;
            Thread.Sleep(300);
            SendTap(210, 245); // Höhe
            Thread.Sleep(300);
            SendTap(100, 42); // Delete
            inputPanel1.Enabled = true;
            Thread.Sleep(300);
            Clipboard.SetDataObject(lblHeight.Text);
            SendTap(11, 312); // Ctrl
            SendTap(94, 295); // V
            Thread.Sleep(300);
            SendTap(215, 20); // Enter
            inputPanel1.Enabled = false;
            Thread.Sleep(300);
            SendTap(180, 15); // OK
            Thread.Sleep(2500);
            SendTap(220, 15); // OK
            Thread.Sleep(1500);
            SendTap(215, 102); // Punkt
            Thread.Sleep(300);
            SendTap(100, 42); // Delete
            inputPanel1.Enabled = true;
            Thread.Sleep(300);
            Clipboard.SetDataObject(intNewPoint.ToString());
            SendTap(11, 312); // Ctrl
            SendTap(94, 295); // V
            Thread.Sleep(100);
            SendTap(215, 20); // Enter
            inputPanel1.Enabled = false;
            intNewPoint++;
            lblPointNumber.Text = "Neuen Punkt Nr. " + intNewPoint.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            intSecond++; // Timer ticks every 1000 ms
            lblSecond.Text = "Sekunden: " + intSecond.ToString();
        }

        private void Form1_Closing(object sender, CancelEventArgs e)
        {
            // Supress exeption message after closing
            e.Cancel = true;

            try
            {
                // Get handle of HookKeys window and terminate process
                IntPtr hwndHookKeys = FindWindow(null, "HookKeys");
                uint processId;
                GetWindowThreadProcessId(hwndHookKeys, out processId);
                TerminateProcess(processId, 0);

                Application.Exit();
            }
            catch { }

        }

        private void SendTap(int x, int y)
        {
            mouse_event(0x2 | 0x8000, (int)((65535 / 240) * x), (int)((65535 / 320) * y), 0, 0); // Left mouse button down at x, y
            mouse_event(0x4, 0, 0, 0, 0); // Left mouse button up
        }

        private void ToggleWindow()
        {
            // Go through all visible windows and write window titles in "lpString"
            EnumWindows(new WNDENUMPROC(Callback), 0);

            // Go through all found windows and find path of tpsMain.exe
            foreach (var key in visibleWindows.Keys)
            {
                uint processId;
                IntPtr hwnd = visibleWindows[key];
                GetWindowThreadProcessId(hwnd, out processId);

                StringBuilder exePath = new StringBuilder(1024);
                int exePathLen = GetModuleFileName((UIntPtr)processId, exePath, exePath.Capacity);
                string strPath = exePath.ToString();
                string strPathTopSurvExe = "\\Storage Card\\TopSURV\\tpsMain.exe";

                // If current path corresponds to "\Storage Card\TopSURV\tpsMain.exe" bring TopSURV to foreground
                if (string.Equals(strPath, strPathTopSurvExe))
                {
                    SetForegroundWindow(hwnd);
                }
            }
        }

        private int Callback(IntPtr hwnd, uint lParam)
        {
            // This Callback method writes the titels of all visible windows into lpString
            bool hasOwner = GetParent(hwnd) != IntPtr.Zero;
            bool visible = IsWindowVisible(hwnd);
            bool isToolWindow = (GetWindowLong(hwnd, GWL_EXSTYLE) & WS_EX_TOOLWINDOW) != 0;
            StringBuilder lpString = new StringBuilder(1024);
            GetWindowText(hwnd, lpString, 1024);

            string key = lpString.ToString();
            if (!hasOwner &&
                visible &&
                !isToolWindow &&
                !string.IsNullOrEmpty(key) &&
                !visibleWindows.ContainsKey(key))
            {
                visibleWindows.Add(key, hwnd);
            }

            return 1;
        }
    }

    public class Bluetooth
    {
        public event EventHandler<DataEventArgs> NewDataAvailable;
        public event EventHandler<BluetoothEventArgs> BluetoothStatus;

        public void ReceiveData()
        {
            // Get current path of "ReachConnectCE.exe" and check if file "mac.txt" exists (contains bluetooth MAC adress of Reach RS+)
            string strCurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            if (!File.Exists(strCurrentDirectory + "\\mac.txt"))
            {
                MessageBox.Show("Die Datei \"mac.txt\" im Verzeichnis " + strCurrentDirectory + " wurde nicht gefunden. " +
                    "Bitte Datei anlegen mit folgendem Inhalt: Bluetooth MAC-Adresse des Reach RS+ Empfängers.",
                    "Lesefehler", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                Application.Exit();
            }

            // Read bluetooth MAC adress from file "mac.txt" and store in strMAC
            FileStream fs = new FileStream(strCurrentDirectory + "\\mac.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string strMAC = sr.ReadLine();
            sr.Close();
            
            DataEventArgs args = new DataEventArgs();
            BluetoothEventArgs b = new BluetoothEventArgs();

            // Initialize bluetooth stack
            try
            {
                BluetoothRadio radio = BluetoothRadio.PrimaryRadio;
                radio.Mode = RadioMode.Discoverable;
            }
            // Catch exeption if bluetooth radio not found
            catch
            {
                MessageBox.Show("Der Bluetooth Empfänger wurde nicht gefunden. Bitte Bluetooth einschalten.", "Bluetooth Fehler", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                Application.Exit();
            }

            // Establish bluetooth connection
            BluetoothClient bcConn = new BluetoothClient();
            do
            {
                try
                {
                    bcConn.Connect(BluetoothAddress.Parse(strMAC), BluetoothService.SerialPort);
                }
                catch { }
            }
            while (!bcConn.Connected);

            // Update GUI once bluetooth connected
            b.bolBluetoothConnected = true;
            this.BluetoothStatus(this, b);

            // Open bluetooth socket
            NetworkStream stream = bcConn.GetStream();

            byte[] bytReadBuffer = new byte[142];
            double[] utmCoordinates = new double[4];
            string strMessage, strLatitude, strLongitude, strHeight, strQuality;
            double dblLatitude, dblLongitude;

            while (true)
            {
                try
                {
                    stream.Read(bytReadBuffer, 0, 142);

                    // Parse data from stream
                    strMessage = System.Text.Encoding.ASCII.GetString(bytReadBuffer, 0, 142);
                    strLatitude = strMessage.Substring(25, 13);
                    strLongitude = strMessage.Substring(40, 12);
                    strHeight = strMessage.Substring(55, 9);
                    strQuality = strMessage.Substring(67, 1);

                    if (strQuality == "1" || strQuality == "2" || strQuality == "5")
                    {
                        // Update GUI once data is received
                        b.bolDataAvailable = true;
                        this.BluetoothStatus(this, b);

                        dblLatitude = Convert.ToDouble(strLatitude);
                        dblLongitude = Convert.ToDouble(strLongitude);
                        args.dblHeight = Convert.ToDouble(strHeight);
                        args.bytQuality = Convert.ToByte(strQuality);

                        // Convert latitude/longitude to UTM by using UTMConverter class. UTMConverter is a static class
                        // which cannot be instanciated. latlontoUTM method expects two values (latitude and lontitude) and
                        // returns array with 4 double values: [0] = UTM east, [1] = UTM north, [2] = zone, [3] = hemisphere
                        utmCoordinates = UTMConverter.latlontoUTM(dblLatitude, dblLongitude);
                        args.dblEast = utmCoordinates[0];
                        args.dblNorth = utmCoordinates[1];
                        args.strZone = utmCoordinates[2].ToString();
                        if (utmCoordinates[3] == 0)
                            args.strHemisphere = "Süd";
                        else
                            args.strHemisphere = "Nord";

                        this.NewDataAvailable(this, args);
                    }
                }
                catch { }
            }
        }
    }

    public class DataEventArgs : EventArgs
    {
        public double dblEast { get; set; }
        public double dblNorth { get; set; }
        public double dblHeight { get; set; }
        public byte bytQuality { get; set; }
        public string strZone { get; set; }
        public string strHemisphere { get; set; }
    }

    public class BluetoothEventArgs : EventArgs
    {
        public bool bolBluetoothConnected { get; set; }
        public bool bolDataAvailable { get; set; }
    }
}