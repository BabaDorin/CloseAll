using CloseAll.Contracts;
using System.Diagnostics;

namespace CloseAll.FilterRules
{
    internal class IgnoreStartupRule : IRule
    {
        private readonly IEnumerable<string> startupProcesses;

        public IgnoreStartupRule(IProcessManager processManager)
        {
            startupProcesses = processManager.GetStartupProcessesNames() ?? Enumerable.Empty<string>();
        }

        public bool IsEligible(Process process)
        {
            return !startupProcesses
                .Any(procName => procName.Equals(process.ProcessName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
