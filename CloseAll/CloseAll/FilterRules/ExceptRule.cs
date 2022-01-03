using CloseAll.Contracts;
using System.Diagnostics;

namespace CloseAll.FilterRules
{
    internal class ExceptRule : IRule
    {
        public bool IsEligible(Process process)
        {
            throw new NotImplementedException();
        }
    }
}
