using CloseAll.Contracts;

namespace CloseAll.Services
{
    internal class ProcessCleaner : IProcessCleaner
    {
        private readonly IFilter filter;
        private readonly IProcessManager processManager;

        public ProcessCleaner(IFilter filter, IProcessManager processManager)
        {
            this.filter = filter;
            this.processManager = processManager;
        }

        public void Start(bool simulate = false)
        {
            var processes = processManager.GetRunningProcesses()
                .ToList();

            processes.ForEach(proc =>
            {
                if (filter.IsEligibleForTermination(proc))
                {
                    Console.WriteLine($"Kill {proc.ProcessName}");
                    
                    if (!simulate)
                        processManager.KillProcess(proc);
                }
            });
        }
    }
}
