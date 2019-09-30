using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace ReachConnectCE
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            // Get current path of "HookKeys.exe" and check if file "mac.txt" exists (contains bluetooth MAC adress of Reach RS+)
            string strCurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
        
            Process p = new Process();
            p.StartInfo.FileName = strCurrentDirectory + "\\HookKeys.exe";
            try
            {
                p.Start();
            }

            catch
            {
                MessageBox.Show("Die Datei \"HookKeys.exe\" wurde im Verzeichnis \\Program Files\\HookKeys\\ nicht gefunden. Die Home Taste am Gerät kann nicht zum Programmwechsel verwendet werden.", "Datei fehlt", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            
            Application.Run(new Form1());
        }
    }
}