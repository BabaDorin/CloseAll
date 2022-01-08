using CloseAll.Contracts;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace CloseAll.FilterRules
{
    internal class WhitelistRule : IPassingRule
    {
        private readonly List<string> whitelistedProcesses;

        public WhitelistRule(IWhiteListManager whiteListManager)
        {
            this.whitelistedProcesses = whiteListManager.GetWhitelistedProcesses()
                .ToList();
        }

        public bool IsPrivileged(Process process)
        {
            if (whitelistedProcesses is null)
                return false;

            return whitelistedProcesses.Contains(
                process.ProcessName,
                new WhitelistComparer());
        }

        private class WhitelistComparer : IEqualityComparer<string>
        {
            public bool Equals(string? x, string? y)
            {
                if (x is null || y is null)
                    return false;

                return x.Equals(y, StringComparison.InvariantCultureIgnoreCase);
            }

            public int GetHashCode([DisallowNull] string obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}
