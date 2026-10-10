using System;
using System.Collections;
using System.IO;

namespace _Game.Develop.Utils.DataManagement.DataRepositories
{
    public class LocalFileDataRepository : IDataRepository
    {
        private readonly string folderPath;
        private readonly string fileExtension;

        public LocalFileDataRepository(string folderPath, string fileExtension)
        {
            this.folderPath = folderPath;
            this.fileExtension = fileExtension;
        }

        public IEnumerator Read(string key, Action<string> onRead)
        {
            string data = File.ReadAllText(GetFullPathFor(key));
            onRead?.Invoke(data);

            yield break;
        }

        public IEnumerator Write(string key, string serializedData)
        {
            File.WriteAllText(GetFullPathFor(key), serializedData);

            yield break;
        }

        public IEnumerator Remove(string key)
        {
            File.Delete(GetFullPathFor(key));

            yield break;
        }

        public IEnumerator Exists(string key, Action<bool> onExistsResult)
        {
            bool isFileExists = File.Exists(GetFullPathFor(key));
            onExistsResult?.Invoke(isFileExists);

            yield break;
        }

        private string GetFullPathFor(string key)
        {
            return Path.Combine(folderPath, key) + "." + fileExtension;
        }
    }
}