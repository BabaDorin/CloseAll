using CloseAll.Contracts;
using System.Diagnostics;

namespace CloseAll.Services
{
    internal class Filter : IFilter
    {
        private readonly IEnumerable<IRule> rules;

        public Filter(params IRule[] rules)
        {
            this.rules = rules ?? Enumerable.Empty<IRule>();
        }

        public bool IsEligibleForTermination(Process process)
        {
            return rules.Any(r => r.IsEligible(process));
        }
    }
}
