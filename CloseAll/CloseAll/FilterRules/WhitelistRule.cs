using CloseAll.Contracts;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace CloseAll.FilterRules
{
    internal class WhitelistRule : IRule
    {
        private readonly IWhiteListManager whiteListManager;

        public WhitelistRule(IWhiteListManager whiteListManager)
        {
            this.whiteListManager = whiteListManager;
        }

        public bool IsEligible(Process process)
        {
            var whitelistedProcesses = whiteListManager
                .GetWhitelistedProcesses();

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
