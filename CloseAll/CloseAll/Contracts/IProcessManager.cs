using System.Diagnostics;

namespace CloseAll.Contracts
{
    internal interface IProcessManager
    {
        List<Process> GetRunningProcesses();
        void KillProcess(Process proc);
    }
}