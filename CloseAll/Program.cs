using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace CloseAll
{
    class Program
    {
        static void Main(string[] args)
        {
            bool nofocus = false;
            bool ignoreStartup = false;
            bool underException = false;
            List<string> exceptList = new List<string>();
            

            foreach (string arg in args)
            {
                if (arg[0] == '-')
                {
                    underException = false;

                    switch (arg)
                    {
                        case "-except":
                            underException = true;
                            break;
                        case "-nofocus":
                            nofocus = true;
                            break;
                        case "-ignore-startup":
                            ignoreStartup = true;
                            break;
                        default:
                            Console.WriteLine("Unknown argument");
                            break;
                    }
                } else 
                {
                    if (underException) {
                        exceptList.Add(arg);
                    } 

                }
            }

            string[] except = exceptList.ToArray();

            Process[] runningProcesses = Process.GetProcesses();
            foreach (Process p in runningProcesses)
            {
                if (!String.IsNullOrEmpty(p.MainWindowTitle)
                && p.MainWindowTitle != Process.GetCurrentProcess().MainWindowTitle
                || p.ProcessName == "explorer")
                {
                    bool allowed = true;
                    foreach(string proc in except) if (proc == p.ProcessName) allowed = false;
                    if (allowed) p.Kill();
                }
                    
            }
        }
    }
}
