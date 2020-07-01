using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;

namespace CloseAll
{
    class Filter
    {
        List<string> StartupApps;

        public bool WriteWhiteList { get; set; }
        public bool NoFocus { get; set; }
        public bool IgnoreStartup { get; set; }
        public bool UnderException { get; set; }
        public string WhiteListPath { get; set; } // by default => same dir as .exe
        public List<string> ExceptList { get; set; }
        public List<string> WhiteList { get; set; }

        public bool GetFilters(string[] args)
        {
            // Reads arguments from args and return true if everyting is ok.
            // It there are invalid arguments, it returns false.

            foreach (string arg in args)
            {
                switch (arg.ToLower())
                {
                    case "-help":
                        Console.WriteLine("\n------------------------ Commands ------------------------\n\n" +
                            "Command: closeall -except <app1> <app2> <app3> : Indicated processes won't get killed\n" +
                            "  ex: closeall -except opera    : All the processes will get killed, except Opera\n" +
                            "  ex: closeall -except discord devenv   : All the processes will get killed, except Discord and Visual Studio\n" +
                            "\nCommand: closeall -ignore-startup : Startup processes won't get killed\n" +
                            "\nCommand: closeall -nofocus : Focused window won't get killed\n" +
                            "\n\nAbbreviations:" +
                            "\n  -except: -e" +
                            "\n  -ignore-startup: -i-s" +
                            "\n  -nofocus: -nf" +
                            "\n----------------------------------------------------------");
                        return false;

                    case "-except":
                    case "-e":
                        UnderException = true;
                        WriteWhiteList = false;
                        break;

                    case "-whitelist":
                    case "-wl":
                        WriteWhiteList = true;
                        UnderException = false;
                        break;

                    case "-removefromwhitelist":
                    case "-rfwl":
                        //
                        // remove one or more items via arg params
                        //
                        break;

                    case "-dropwhitelist":
                    case "-dwl":
                        //
                        // remove all items from whitelist
                        //
                        break;

                    case "-checkwhitelist":
                    case "-cwl":
                        Console.WriteLine("Whitelisted processes:");
                        foreach(string item in WhiteList)
                        {
                            Console.WriteLine(item);
                        }
                        Console.WriteLine();
                        WriteWhiteList = false;
                        break;

                    case "-nofocus":
                    case "-nf":
                        NoFocus = true;
                        UnderException = false;
                        WriteWhiteList = false;
                        break;

                    case "-ignore-startup":
                    case "-i-s":
                        IgnoreStartup = true;

                        UnderException = false;
                        WriteWhiteList = false;
                        break;

                    default:
                        if (UnderException)
                        {
                            Except(arg.ToLower());
                        }
                        else if (WriteWhiteList)
                        {
                            if (WhiteList == null) WhiteList = new List<string>();

                            string wlProcessName = arg.ToLower();
                            if (WhitelistThis(wlProcessName))
                            {
                                Console.WriteLine(wlProcessName + " has been whitelisted");
                            }
                            else
                            {
                                Console.WriteLine(wlProcessName + " is already whitelisted");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nUnknown argument   ¯\\_('-')_/¯  ");
                            Console.WriteLine("   Use closeall -help to get the list of commands.");
                            return false;
                        }
                        break;
                }
            }

            return true;
        }

        public void Except(string processName)
        {
            ExceptList.Add(processName);
        }

        public bool IgnoreProcess(Process process)
        {
            // It runs the process through some filters.
            // Returns true if the process should be skipped
            // Returns false if the process should be killed

            // The process is included in whitelist
            if (WhiteList != null && WhiteList.Contains(process.ProcessName.ToLower()))
                return true;

            // The process is focused
            if (NoFocus)
            {
                if (process.MainWindowHandle == getFocusedProcess())
                    return true;
            }

            // The process is included in ExceptList
            foreach (string proc in ExceptList)
                if (proc == process.ProcessName.ToLower())
                    return true;

            // It's a Startup process
            if (IgnoreStartup)
            {
                // ISSUE: Find a way to get only startup applications that are currently enabled.
                // This way we get all startup apps, including disabled ones.

                if(StartupApps == null || StartupApps.Count == 0)
                {
                    // Get & store startup apps
                    ManagementClass mangnmt = new ManagementClass("Win32_StartupCommand");
                    ManagementObjectCollection startupApps = mangnmt.GetInstances();
                    StartupApps = new List<string>();

                    foreach (ManagementObject app in startupApps)
                    {
                        string processName = getProcessName(app["command"] as string);

                        if (processName != null)
                            StartupApps.Add(processName);
                    }
                }

                if (StartupApps.Contains(process.ProcessName.ToLower()))
                    return true;
            }

            // Process goes to GULAG /rip/
            return false;
        }

        public Filter()
        {
            ExceptList = new List<string>();
            WhiteListPath = Directory.GetCurrentDirectory() + "/closeall_whitelist.txt";

            if (File.Exists(WhiteListPath))
            {
                using (StreamReader sr = new StreamReader(WhiteListPath))
                {
                    WhiteList = sr.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }
            }
        }

        private string getProcessName(string command)
        {
            int startIndex = command.LastIndexOf('\\');

            if (startIndex <= 0)
                return null;

            string processName = command.Substring(++startIndex).ToLower();
            int endIndex = processName.IndexOf(".exe");

            if (endIndex <= 0)
                return null;

            return processName.Substring(0, endIndex);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]//RVA
        private static extern IntPtr GetForegroundWindow();

        public IntPtr getFocusedProcess()
        {
            return GetForegroundWindow();
        }

        public bool WhitelistThis(string processName)
        {
            // returns false if the process is already whitelisted
            if (WhiteList.Contains(processName))
                return false;

            WhiteList.Add(processName);

            SyncWhitelist();
            return true;
        }

        public void SyncWhitelist()
        {
            // Writes whitelist file
            using (StreamWriter sw = new StreamWriter(WhiteListPath))
            {
                foreach(string item in WhiteList)
                {
                    sw.WriteLine(item);
                }
            }
        }

    }
}
