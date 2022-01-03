using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;

namespace CloseAll.Models
{
    class Filter
    {
        List<string> StartupApps;

        public bool WriteWhiteList { get; set; }
        public bool RemoveFromWhiteList { get; set; }
        public bool DropWhiteList { get; set; }
        public bool NoFocus { get; set; }
        public bool IgnoreStartup { get; set; }
        public bool UnderException { get; set; }
        public string WhiteListPath { get; set; } // by default => same dir as .exe
        public List<string> ExceptList { get; set; }
        public List<string> WhiteList { get; set; }

        public bool getFilters(string[] args)
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
                        RemoveFromWhiteList = false;
                        break;

                    case "-whitelist":
                    case "-wl":
                        WriteWhiteList = true;
                        UnderException = false;
                        RemoveFromWhiteList = false;
                        break;

                    case "-removefromwhitelist":
                    case "-rfwl":
                        // removes one or more items via arg params
                        RemoveFromWhiteList = true;
                        UnderException = false;
                        WriteWhiteList = false;
                        break;

                    case "-dropwhitelist":
                    case "-dwl":
                        // removes all items from whitelist
                        dropWhiteList();
                        RemoveFromWhiteList = false;
                        UnderException = false;
                        WriteWhiteList = false;
                        break;

                    case "-checkwhitelist":
                    case "-cwl":
                        // displays whitelisted processes
                        Console.WriteLine("\nWhitelisted processes:");
                        foreach (string item in WhiteList)
                        {
                            Console.WriteLine("  " + item);
                        }
                        Console.WriteLine();
                        RemoveFromWhiteList = false;
                        WriteWhiteList = false;
                        RemoveFromWhiteList = false;
                        break;

                    case "-nofocus":
                    case "-nf":
                        NoFocus = true;
                        UnderException = false;
                        WriteWhiteList = false;
                        RemoveFromWhiteList = false;
                        break;

                    case "-ignore-startup":
                    case "-i-s":
                        IgnoreStartup = true;
                        UnderException = false;
                        WriteWhiteList = false;
                        RemoveFromWhiteList = false;
                        break;

                    default:
                        if (UnderException)
                        {
                            except(arg.ToLower());
                        }

                        else if (WriteWhiteList)
                        {
                            string wlProcessName = arg.ToLower();
                            if (whitelistThis(wlProcessName))
                                Console.WriteLine(wlProcessName + " has been whitelisted");
                            else
                                Console.WriteLine(wlProcessName + " is already whitelisted");
                        }

                        else if (RemoveFromWhiteList)
                        {
                            if (!File.Exists(WhiteListPath))
                                Console.WriteLine("There is no whitelist.");
                            else
                            {
                                string wlProcessName = arg.ToLower();
                                if (!removeFromWhiteList(wlProcessName))
                                    Console.WriteLine(wlProcessName + " is not whitelisted.");
                                else
                                    Console.WriteLine(wlProcessName + " has been removed from your whitelist");
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

        public void except(string processName)
        {
            ExceptList.Add(processName);
        }

        public bool ignoreProcess(Process process)
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

                if (StartupApps == null || StartupApps.Count == 0)
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

            // read whitelist from file
            if (File.Exists(WhiteListPath))
                using (StreamReader sr = new StreamReader(WhiteListPath))
                    WhiteList = sr.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            else
                WhiteList = new List<string>();
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

        public bool whitelistThis(string processName)
        {
            // returns false if the process is already whitelisted
            if (WhiteList.Contains(processName))
                return false;

            WhiteList.Add(processName);

            syncWhitelist();
            return true;
        }

        public bool removeFromWhiteList(string processName)
        {
            // false => item is not whitelisted
            // true => item has been un-whitelisted

            if (!WhiteList.Contains(processName))
                return false;

            WhiteList.RemoveAll(p => p == processName);

            syncWhitelist();
            return true;
        }

        public void dropWhiteList()
        {
            try
            {
                WhiteList.Clear();

                if (File.Exists(WhiteListPath))
                    File.Delete(WhiteListPath);

                Console.WriteLine("your whitelist has been dropped.");
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Something unexpected happened. The whitelist has not been dropped.");
            }
        }

        public void syncWhitelist()
        {
            // Writes whitelist file
            using (StreamWriter sw = new StreamWriter(WhiteListPath))
            {
                foreach (string item in WhiteList)
                {
                    sw.WriteLine(item);
                }
            }
        }

    }
}
