using CloseAll.Contracts;
using System.Diagnostics;

namespace CloseAll.FilterRules
{
    internal class IgnoreStartupRule : IPassingRule
    {
        private readonly IEnumerable<string> startupProcesses;

        public IgnoreStartupRule(IProcessManager processManager)
        {
            startupProcesses = processManager.GetStartupProcessesNames() ?? Enumerable.Empty<string>();
        }

        public bool IsPrivileged(Process process)
        {
            return startupProcesses
                .Any(procName => procName.Equals(process.ProcessName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
