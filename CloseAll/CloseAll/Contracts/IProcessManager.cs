using System.Diagnostics;

namespace CloseAll.Contracts
{
    internal interface IProcessManager
    {
        IEnumerable<Process> GetRunningProcesses();

        IEnumerable<string> GetStartupProcessesNames();
        
        void KillProcess(Process proc);
    }
}