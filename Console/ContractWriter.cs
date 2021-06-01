namespace HumbleVerifierConsole
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using HumbleVerifierLibrary;
    using Newtonsoft.Json.Linq;

    public class ContractWriter
    {
        private readonly IFileSystem fileSystem;

        public ContractWriter(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        /// <summary>
        /// Write the set of contracts to disk
        /// </summary>
        /// <param name="contracts"></param>
        /// <param name="contractType"></param>
        /// <param name="name"></param>
        /// <param name="rootDir"></param>
        public void WriteToDisk(JToken contracts, ContractType contractType, string name, string rootDir)
        {
            if (contracts == null) return;

            switch (contractType)
            {
                case ContractType.Multi:
                    {
                        IEnumerable<JProperty> children = contracts.Children<JProperty>();
                        foreach (JProperty child in children)
                        {
                            FileInfo fullPath = this.MakeUniquePath(Path.Combine(rootDir, Path.GetFileName(child.Name)));
                            WriteHelper(fullPath, child.Value["content"].ToString());
                        }

                        break;
                    }
                case ContractType.Single:
                    {
                        FileInfo fullPath = this.MakeUniquePath(Path.Combine(rootDir, name + ".sol"));
                        WriteHelper(fullPath, contracts.ToString());
                        break;
                    }
            }

            void WriteHelper(FileInfo fullPath, string content)
            {
                Console.WriteLine("Writing " + fullPath.FullName);
                this.fileSystem.File.WriteAllText(fullPath.FullName, content);
            }
        }

        public FileInfo MakeUniquePath(string path)
        {
            string dir = Path.GetDirectoryName(path);
            string fileName = Path.GetFileNameWithoutExtension(path);
            string fileExt = Path.GetExtension(path);

            for (int i = 1; ; ++i)
            {
                if (!this.fileSystem.File.Exists(path))
                    return new FileInfo(path);

                path = Path.Combine(dir, fileName + i + fileExt);
            }
        }
    }
}