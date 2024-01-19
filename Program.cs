using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Overbrugging
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (Priorprocess() != null)
            {
                _ = MessageBox.Show("Programma draait al op deze PC\nKan er nog niet 1 starten op zelfde PC");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public static Process Priorprocess()
        {
            Process curr = Process.GetCurrentProcess();
            Process[] procs = Process.GetProcessesByName(curr.ProcessName);
            foreach (Process p in procs)
            {
                if ((p.Id != curr.Id) && (p.MainModule.FileName == curr.MainModule.FileName))
                {
                    return p;
                }
            }
            return null;
        }
    }
}