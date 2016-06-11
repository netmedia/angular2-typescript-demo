using System;
using Netmedia.Common;
using Netmedia.Infrastructure.Services;

namespace Netmedia.Web.Services
{
        public class FileSystemService : IFileSystemService
        {
            /// <summary>
            /// Checks if file exists.
            /// </summary>
            /// <param name="filePath"></param>
            /// <returns></returns>
            public bool FileExists(string filePath)
            {
                return File.Exists(filePath);
            }
            /// <summary>
            /// Copies file.
            /// </summary>
            /// <param name="originalFilePath"></param>
            /// <param name="targetFilePath"></param>
            public void FileCopy(string originalFilePath, string targetFilePath)
            {
                if (File.Exists(originalFilePath) == false) return;
                if (File.Exists(targetFilePath) == false) return;

                File.Copy(originalFilePath, targetFilePath);
            }

            /// <summary>
            /// Creates file.
            /// </summary>
            /// <param name="targetFilePath"></param>
            public void FileCreate(string targetFilePath)
            {
                if (File.Exists(targetFilePath)) return;

                File.Create(targetFilePath);
            }

            /// <summary>
            /// Gets file creation time.
            /// </summary>
            /// <param name="filePath"></param>
            /// <returns></returns>
            public DateTime? FileCreated(string filePath)
            {
                if (FileExists(filePath) == false) return null;

                return File.CreatedOn(filePath);
            }

            public string FileFirstLine(string filePath)
            {
                return File.ReadFirstLine(filePath);
            }

            public long FileSize(string filePath)
            {
                return File.Size(filePath);
            }
        }
}