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
            // TO BE REMOVED
            // |-----------------------
            var rule = rules.FirstOrDefault(r => r.IsEligible(process));
            
            if(rule != null)
                Console.WriteLine($"{process.ProcessName} was terminated due to {rule.GetType().Name} rule");
            // |-----------------------

            return rules.Any(r => r.IsEligible(process));
        }
    }
}
