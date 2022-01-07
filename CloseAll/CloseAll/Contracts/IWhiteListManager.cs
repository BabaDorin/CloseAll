namespace CloseAll.Contracts
{
    public interface IWhiteListManager
    {
        public IEnumerable<string> GetWhitelistedProcesses();
    }
}
