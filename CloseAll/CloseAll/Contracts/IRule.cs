using System.Diagnostics;

namespace CloseAll.Contracts
{
    internal interface IRule
    {
        public bool IsEligible(Process process);
    }
}
