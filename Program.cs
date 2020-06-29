using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CloseAll
{
    class Program
    {
        public string getActiveWindowName()
        {
            try //jic
            {
                var aHandle = GetForegroundWindow();
                Process[] processes = Process.GetProcesses();
                foreach (Process clsProcess in processes)
                {
                    if (aHandle == clsProcess.MainWindowHandle)
                    {
                        string processName = clsProcess.ProcessName;
                        return processName;
                    }
                }
            }
            catch { }
            return null;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();


        static void Main(string[] args)
        {

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
                            string focused = new Program().getActiveWindowName();
                            exceptList.Add(focused);
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
