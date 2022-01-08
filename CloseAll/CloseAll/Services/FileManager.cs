using CloseAll.Contracts;

namespace CloseAll.Services
{
    internal class FileManager : IFileManager
    {
        public string ReadAll(string path)
        {
            if (!File.Exists(path))
                File.Create(path);

            using var sr = new StreamReader(path);

            return sr.ReadToEnd().Replace(@"\r", "");
        }

        public void Append(string path, string content)
        {
            if (!File.Exists(path))
                File.Create(path);

            using var sw = new StreamWriter(path, true);
            sw.WriteLine(content);
        }

        public void Write(string path, string content)
        {
            using var sw = new StreamWriter(path, false);
            sw.WriteLine(content);
        }
    }
}
