using CloseAll.Contracts;
using System.Diagnostics;

namespace CloseAll.FilterRules
{
    internal class IgnoreStartupRule : IRule
    {
        public bool IsEligible(Process process)
        {
            throw new NotImplementedException();
        }
    }
}
