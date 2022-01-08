using System.Diagnostics;

namespace CloseAll.Contracts
{
    internal interface IPassingRule
    {
        /// <summary>
        /// Finds out whether the process can overpass termination based on some rules
        /// </summary>
        /// <param name="process">The process to be checked against</param>
        /// <returns>True if the process is privileged and should not be killed.</returns>
        public bool IsPrivileged(Process process);
    }
}
