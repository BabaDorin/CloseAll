using System.Diagnostics;

namespace CloseAll.Contracts
{
    internal interface IFilter
    {
        public bool IsEligibleForTermination(Process process);
    }
}
