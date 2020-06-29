using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace CloseAll
{
    class Program
    {
        static void Main(string[] args)
        {
            Filter filter = new Filter();

            filter.GetFilters(args);

            filter.IgnoreStartup = true;
            Console.WriteLine("IgnoreStartup set to true");

            filter.IgnoreProcess(Process.GetCurrentProcess());

            //Process[] runningProcesses = Process.GetProcesses();
            //foreach (Process p in runningProcesses)
            //{
            //    if (!String.IsNullOrEmpty(p.MainWindowTitle)
            //    && p.MainWindowTitle != Process.GetCurrentProcess().MainWindowTitle
            //    || p.ProcessName == "explorer")
            //    {
            //        if (!filter.IgnoreProcess(p)) p.Kill();



            //        //bool allowed = true;
            //        //foreach (string proc in filter.ExceptList) if (proc == p.ProcessName) allowed = false;
            //        //if (allowed) p.Kill();

            //        //Console.WriteLine(p.ProcessName);
            //    }
                    
            //}
        }
    }
}
