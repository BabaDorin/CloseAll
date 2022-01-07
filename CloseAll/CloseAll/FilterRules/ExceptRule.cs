using CloseAll.Contracts;
using System.Diagnostics;

namespace CloseAll.FilterRules
{
    /// <summary>
    /// The process won't be terminated if it's name is within the exceptions list.
    /// The exceptions are passed via cmd arguments
    /// </summary>
    internal class ExceptRule : IRule
    {
        private readonly IEnumerable<string> exceptions;

        public ExceptRule(IEnumerable<string> exceptions)
        {
            this.exceptions = exceptions ?? Enumerable.Empty<string>();
        }

        public bool IsEligible(Process process)
        {
            return !exceptions
                .Any(processName => processName.Equals(process.ProcessName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
