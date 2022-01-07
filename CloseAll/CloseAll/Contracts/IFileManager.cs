namespace CloseAll.Contracts
{
    internal interface IFileManager
    {
        public string ReadAll(string path);

        public void Write(string path, string content);

        public void Append(string path, string content);
    }
}
