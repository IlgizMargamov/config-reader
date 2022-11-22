using System.Collections.Generic;

namespace FileReaderLibrary
{
    public interface IFileReader
    {
        string AcceptableFileFormat { get; }
        IEnumerable<IConfiguration> ReadFile(string path);
    }
}