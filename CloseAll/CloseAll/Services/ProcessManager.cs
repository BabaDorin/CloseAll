using CloseAll.Contracts;
using System.Diagnostics;

namespace CloseAll.Services
{
    internal class ProcessManager : IProcessManager
    {
        public List<Process> GetRunningProcesses()
        {
            throw new Exception();
        }

        public void KillProcess(Process proc)
        {
            Console.WriteLine(proc.ProcessName);
            // proc.Kill();
        }
    }
}
