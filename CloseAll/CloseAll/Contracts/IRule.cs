using System.Diagnostics;

namespace CloseAll.Contracts
{
    internal interface IRule
    {
        /// <summary>
        /// Finds out whether the process can be terminated based on a specific rule
        /// </summary>
        /// <param name="process">The process to be checked against</param>
        /// <returns>True if the process can be terminated.</returns>
        public bool IsEligible(Process process);
    }
}
