using System;
using System.Diagnostics;

namespace CloseAll
{
    class Program
    {
        static void Main(string[] args)
        {
            Process[] runningProcesses = Process.GetProcesses();
            foreach (Process p in runningProcesses)
            {
                if (!String.IsNullOrEmpty(p.MainWindowTitle)
                && p.MainWindowTitle != Process.GetCurrentProcess().MainWindowTitle
                || p.ProcessName == "explorer")
                    p.Kill();
            }
        }
    }
}
