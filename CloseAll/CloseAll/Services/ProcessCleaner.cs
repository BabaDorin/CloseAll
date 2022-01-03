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

        public void Start()
        {
            var processes = processManager.GetRunningProcesses();

            processes.ForEach(proc =>
            {
                if (filter.IsEligibleForTermination(proc))
                {
                    processManager.KillProcess(proc);
                }
            });
        }
    }
}
