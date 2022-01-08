using CloseAll.Presentation;
using CloseAll.Services;

class Program
{
    static void Main(string[] args)
    {
        var filterBuilder = new FilterBuilder();

        var runCleaner = true;
        var currentOperation = ArgsOperation.None;

        foreach(var arg in args)
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
                    runCleaner = false;
                    break;

                case "-except":
                case "-e":
                    currentOperation = ArgsOperation.Except;
                    break;
                    
                // Not Supported yet
                case "-whitelist":
                case "-wl":
                    currentOperation = ArgsOperation.WhitelistAppend;
                    break;

                // Not Supported yet
                case "-removefromwhitelist":
                case "-rfwl":
                    currentOperation = ArgsOperation.WhitelistRemove;
                    break;

                default:
                    if (currentOperation == ArgsOperation.None)
                    {
                        Console.WriteLine($"Invalid argument: {arg}");
                        runCleaner = false;
                    }

                    if (currentOperation == ArgsOperation.Except)
                        filterBuilder.Except(arg);
                    break;
            }
        }

        if (!runCleaner)
            return;

        var processManager = new ProcessManager();

        var filter = filterBuilder.IgnoreStartup(processManager)
            .EnableWhiteList(new WhiteListManager(new FileManager()))
            .Build();

        new ProcessCleaner(filter, processManager)
            .Start();

        Console.ReadKey();
    }
}