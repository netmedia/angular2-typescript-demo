using System;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

namespace Netmedia.Common
{
    public static class File
    {
        public static void Create(string filePath)
        {
            if (Exists(filePath) == false) Delete(filePath);

            Create(filePath, true);
        }
        public static FileStream Create(string filePath, bool streamShouldBeClosed)
        {
            if (Exists(filePath) == false) Delete(filePath);

            FileStream createdFileStream = System.IO.File.Create(filePath);

            if (streamShouldBeClosed)
            {
                createdFileStream.Close();
                return null;
            }

            return createdFileStream;
        }
        public static void Delete(string filePath)
        {
            if (Exists(filePath) == false) return;

            System.IO.File.Delete(filePath);
        }
        public static void SetAttributes(string filePath, int fileAttributes)
        {
            if (!Exists(filePath) == false) return;

            System.IO.File.SetAttributes(filePath, (FileAttributes)fileAttributes);
        }
        public static bool Exists(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }
        public static void Copy(string originalFilePath, string targetFilePath)
        {
            if (!Exists(originalFilePath) == false) return;
            if (!Exists(targetFilePath) == false) return;

            System.IO.File.Copy(originalFilePath, targetFilePath);
        }
        public static DateTime? CreatedOn(string filePath)
        {
            if (Exists(filePath) == false) return null;

            return System.IO.File.GetCreationTime(filePath);
        }
        public static string ReadFirstLine(string filePath)
        {
            if (Exists(filePath) == false) return "";

            string firstLine;
            using (StreamReader stream = new StreamReader(filePath))
            {
                firstLine = stream.ReadLine();
                stream.Close();
            }

            return firstLine;
        }
        public static long Size(string filePath)
        {
            if (Exists(filePath) == false) return 0;
            
            return (new FileInfo(filePath)).Length;
        }

        public class Zip : IDisposable
        {
            private readonly bool _zipFileStreamCreatedInternally;
            private readonly Stream _zipFileStream;
            private readonly int _compressionLevel;
            private ZipOutputStream _outputStream;

            public Zip(string zipFileName) : this(zipFileName, 9)
            {}
            public Zip(string zipFileName, int zipFileCompressionLevel)
            {
                _VerifyDestinationZipFileDoesNotExist(zipFileName);

                
                _zipFileStream = Create(zipFileName, false);
                _zipFileStreamCreatedInternally = true;
                _compressionLevel = zipFileCompressionLevel;

                _SetupZipOutputStream();
            }

            public Zip(Stream zipFileStream) : this(zipFileStream, 9)
            {}
            public Zip(Stream zipFileStream, int zipFileCompressionLevel)
            {
                _VerifyDestinationZipFileStreamIsOpen(zipFileStream);


                _zipFileStream = zipFileStream;
                _zipFileStreamCreatedInternally = false;
                _compressionLevel = zipFileCompressionLevel;

                _SetupZipOutputStream();
            }

            public void AppendFile(string filePath)
            {
                _VerifyFileToBeAppendedExists(filePath);


                Crc32 crc = new Crc32();

                FileStream streamOfFileToBeAppended = System.IO.File.OpenRead(filePath);

                Byte[] buffer = new byte[Convert.ToInt32(streamOfFileToBeAppended.Length) - 1];
                streamOfFileToBeAppended.Read(buffer, 0, buffer.Length);

                ZipEntry zipEntryForFile = new ZipEntry(Path.GetFileName(filePath));
                zipEntryForFile.DateTime = DateTime.Now;
                zipEntryForFile.Size = streamOfFileToBeAppended.Length - 1;

                streamOfFileToBeAppended.Close();

                crc.Reset();
                crc.Update(buffer);
                zipEntryForFile.Crc = crc.Value;

                _outputStream.PutNextEntry(zipEntryForFile);
                _outputStream.Write(buffer, 0, buffer.Length);
            }
            

            public void Dispose()
            {
                if (_outputStream != null)
                {
                    _outputStream.Finish();
                    _outputStream.Close();
                }
                if (_zipFileStreamCreatedInternally && _zipFileStream != null)
                {
                    _zipFileStream.Close();
                }
            }

            private void _SetupZipOutputStream()
            {
                _outputStream = new ZipOutputStream(_zipFileStream);
                _outputStream.SetLevel(_compressionLevel);
            }
            private void _VerifyDestinationZipFileDoesNotExist(string zipFilePath)
            {
                if (Exists(zipFilePath) == false) return;

                System.IO.File.Delete(zipFilePath);
            }
            private void _VerifyDestinationZipFileStreamIsOpen(Stream zipFileStream)
            {
                if (zipFileStream != null) return;

                throw new InvalidOperationException("Destination Zip file stream is closed!");
            }
            private void _VerifyFileToBeAppendedExists(string filePath)
            {
                if (Exists(filePath)) return;

                throw new InvalidOperationException("File which should be appended to zip file does not exist: " + filePath);
            }
        }
    }
}
