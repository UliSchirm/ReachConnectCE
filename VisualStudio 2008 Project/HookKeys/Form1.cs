using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

// Version V2
namespace HookKeys
{
    public partial class Form1 : Form
    {
        #region P/Invoke coredll.dll, used for key hooks

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
            timer1.Enabled = true;
        }

        static int Callback(IntPtr hwnd, uint lParam)
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

        private void HookEvent(HookEventArgs e, KeyBoardInfo keyBoardInfo)
        {
            /*
            This method is called, when any hardware button on FC-200 is pressed.
            When the home button is pressed, foreground windows toggles between
            ReachConnectCE and TopSURV.
            
            FC-200 HARDWARE KEYS
                                        wParam      wParam
                            vkCode:     key down:   key up:
             
            ENT button      13          256         257
            Alt button:     18          260         261
            ESC button:     27          256         257
            Home button:    36          256         257
            Left button:    37          256         257
            Up button       38          256         257
            Right button    39          256         257
            Down button     40          256         257
            Windows button: 92          256         257
            */

            if (keyBoardInfo.vkCode == 36 && e.wParam.ToInt32() == 256) // Home button pressed
            {
                ToggleWindow();
            }

            if (keyBoardInfo.vkCode == 40 && e.wParam.ToInt32() == 256) // Down button pressed
            {
                // Show/hide software input panel (SIP)
                if (!inputPanel1.Enabled)
                {
                    inputPanel1.Enabled = true;
                }
                else
                    inputPanel1.Enabled = false;

            }
        }

        private void ToggleWindow()
        {
            // Get handle of ReachConnectCE window
            IntPtr hwndReachConnectCE = FindWindow(null, "ReachConnectCE");

            if (hwndReachConnectCE != GetForegroundWindow()) // Check if ReachConnectCE is in foreground
            {
                SetForegroundWindow(hwndReachConnectCE); // Bring ReachConnectCE to foreground
                inputPanel1.Enabled = false;
            }
            else
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
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Inizialize keyboard hook event handler. Calls "HookEvent" method,
            // when any hardware button is pressed on the field computer.
            HookKeys hook = new HookKeys();
            hook.HookEvent += new HookKeys.HookEventHandler(HookEvent);
            hook.Start();

            timer1.Enabled = false;
        }
    }
}