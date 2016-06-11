using System;

namespace Netmedia.Infrastructure.Services
{
    public interface IFileSystemService
    {
        bool FileExists(string filePath);
        void FileCopy(string originalFilePath, string targetFilePath);
        void FileCreate(string targetFilePath);
        DateTime? FileCreated(string filePath);
        string FileFirstLine(string filePath);
        long FileSize(string filePath);
    }
}