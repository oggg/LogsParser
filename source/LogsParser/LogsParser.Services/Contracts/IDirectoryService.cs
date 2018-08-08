using System.Collections.Generic;

namespace LogsParser.Services.Contracts
{
    public interface IDirectoryService
    {
        IEnumerable<string> GetLogDirectories();

        IEnumerable<string> GetLogFilesPerDirectory(string path);
    }
}
