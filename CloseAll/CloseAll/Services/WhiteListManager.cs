using CloseAll.Contracts;

namespace CloseAll.Services
{
    internal class WhiteListManager : IWhiteListManager
    {
        // The file path will be hardcoded for the beginning
        public readonly string filePath;

        private readonly IFileManager fileManager;

        public WhiteListManager(IFileManager fileManager)
        {
            var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePath = Path.Combine(myDocs, "closeall_whishlist.txt");

            this.fileManager = fileManager;
        }

        public IEnumerable<string> GetWhitelistedProcesses()
        {
            return this.fileManager.ReadAll(filePath)
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
