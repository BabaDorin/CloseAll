using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CloseAll
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();

            Filter filter = new Filter();

            if (filter.GetFilters(args))
            {
                Console.WriteLine("TRUE");
                Process[] runningProcesses = Process.GetProcesses();
                foreach (Process p in runningProcesses)
                {
                    if (!String.IsNullOrEmpty(p.MainWindowTitle)
                    && p.MainWindowTitle != Process.GetCurrentProcess().MainWindowTitle
                    || p.ProcessName == "explorer")
                    {
                        if (!filter.IgnoreProcess(p))
                        {
                            //Console.WriteLine("Kill " + p.ProcessName);
                            Console.WriteLine("Kill " + p);
                            //p.Kill();
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("False");

                return;
            }
        }
    }
}
