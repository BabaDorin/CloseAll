using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Management.Instrumentation;

namespace CloseAll
{
    class Filter
    {
        ManagementObjectCollection StartupApps;

        public bool NoFocus { get; set; }
        public bool IgnoreStartup { get; set; }
        public bool UnderException { get; set; }
        public List<string> ExceptList { get; set; }

        public void GetFilters(string[] args)
        {
            foreach (string arg in args)
            {
                if (arg[0] == '-')
                {
                    UnderException = false;

                    switch (arg)
                    {
                        case "-except":
                            UnderException = true;
                            break;
                        case "-nofocus":
                            NoFocus = true;
                            break;
                        case "-ignore-startup":
                            IgnoreStartup = true;
                            break;
                        default:
                            Console.WriteLine("Unknown argument");
                            break;
                    }
                }
                else
                {
                    if (UnderException)
                    {
                        Except(arg);
                    }
                }
            }
        }

        public void Except(string processName)
        {
            ExceptList.Add(processName);
        }

        public bool IgnoreProcess(Process process)
        {
            // The process is included in ExceptList
            foreach (string proc in ExceptList)
                if (proc == process.ProcessName)
                    return true;

            // It's a Startup process
            if (IgnoreStartup)
            {
                //get startup processes and store them
                if(StartupApps == null || StartupApps.Count == 0)
                {
                    // Get & store startup apps
                    ManagementClass mangnmt = new ManagementClass("Win32_StartupCommand");
                    StartupApps = mangnmt.GetInstances();  
                }

                foreach(ManagementObject app in StartupApps)
                {
                    // Check if the process passed as argument is within StartupApps
                }

                Console.ReadKey();
            }

            // It is focused
            if (NoFocus)
            {
                //stuff
            }

            // Process goes to GULAG
            return false;
        }

        public Filter()
        {
            ExceptList = new List<string>();
        }
    }
}
