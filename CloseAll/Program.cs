using System;
using System.Diagnostics;

namespace CloseAll
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Whitelist feature => user will have the possibility to whitelist some processes.
            // Save them in a file or smth
            // Those processes won't get killed so he can run closeall without indicating the same
            // apps in -except all the time.

            Filter filter = new Filter();

            if (filter.getFilters(args))
            {
                Console.WriteLine();

                Process[] runningProcesses = Process.GetProcesses();
                foreach (Process p in runningProcesses)
                {
                    if (!String.IsNullOrEmpty(p.MainWindowTitle)
                    && p.MainWindowTitle != Process.GetCurrentProcess().MainWindowTitle
                    || p.ProcessName == "explorer")
                    {
                        if (!filter.ignoreProcess(p))
                        {
                            Console.WriteLine("Kill " + p.ProcessName);
                            // p.Kill();
                        }
                    }
                }
            }
            else
            {
                // Invalid args
                return;
            }
        }
    }
}
