using CloseAll.Contracts;
using System.Diagnostics;

namespace CloseAll.Services
{
    internal class Filter : IFilter
    {
        private readonly IEnumerable<IPassingRule> rules;

        public Filter(params IPassingRule[] rules)
        {
            this.rules = rules ?? Enumerable.Empty<IPassingRule>();
        }

        public bool IsEligibleForTermination(Process process)
        {
            // TO BE REMOVED
            // |-----------------------
            var rule = rules.FirstOrDefault(r => r.IsPrivileged(process));
            
            if(rule != null)
                Console.WriteLine($"{process.ProcessName} was privileged due to {rule.GetType().Name} rule");
            // |-----------------------

            return !rules.Any(r => r.IsPrivileged(process));
        }
    }
}
