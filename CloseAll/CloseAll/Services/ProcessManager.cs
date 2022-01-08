using CloseAll.Contracts;
using System.Diagnostics;

namespace CloseAll.Services
{
    internal class ProcessManager : IProcessManager
    {
        public IEnumerable<Process> GetRunningProcesses()
        {
            var processes = Process.GetProcesses();

            processes = processes.Where(proc => MightBeTerminated(proc)).ToArray();

            return processes;
        }

        // TODO: Enrich selection | Some processes that should be terminated are not selected
        private bool MightBeTerminated(Process proc)
        {
            return
                HasWindow(proc) || proc.ProcessName == "explorer";
        }

        private bool HasWindow(Process proc)
        {
            return !String.IsNullOrEmpty(proc.MainWindowTitle) && 
                proc.MainWindowTitle != Process.GetCurrentProcess().MainWindowTitle;
        }
        
        // TODO 
        public IEnumerable<string> GetStartupProcessesNames()
        {
            return Enumerable.Empty<string>();
        }

        public void KillProcess(Process proc)
        {
            proc.Kill();
        }
    }
}
