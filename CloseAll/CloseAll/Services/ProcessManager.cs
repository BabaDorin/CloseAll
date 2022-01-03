using CloseAll.Contracts;
using System.Diagnostics;

namespace CloseAll.Services
{
    internal class ProcessManager : IProcessManager
    {
        public IEnumerable<Process> GetRunningProcesses()
        {
            throw new Exception();
        }
        
        public IEnumerable<string> GetStartupProcessesNames()
        {
            throw new NotImplementedException();
        }

        public void KillProcess(Process proc)
        {
            Console.WriteLine(proc.ProcessName);
            // proc.Kill();
        }
    }
}
