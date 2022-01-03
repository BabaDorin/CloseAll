using CloseAll.Contracts;
using System.Diagnostics;

namespace CloseAll.Services
{
    internal class ProcessCleaner : IProcessCleaner
    {
        private readonly IFilter filter;

        public ProcessCleaner(IFilter filter)
        {
            this.filter = filter;
        }

        public void Start()
        {
            GetRunningProcesses().ForEach(proc =>
            {
                if (filter.IsEligibleForTermination(proc))
                {
                    KillProcess(proc);
                }
            });
        }

        private List<Process> GetRunningProcesses()
        {
            throw new Exception();
        }

        private void KillProcess(Process proc)
        {
            Console.WriteLine(proc.ProcessName);
            // proc.Kill();
        }
    }
}
