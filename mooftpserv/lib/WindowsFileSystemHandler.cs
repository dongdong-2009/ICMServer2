using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using ICMServer;

namespace mooftpserv
{
    /// <summary>
    /// Default file system handler. Allows access to the whole file system. Supports drives on Windows.
    /// </summary>
    public class WindowsFileSystemHandler : IFileSystemHandler
    {
        // the path of root directory, for example: "c:\"
        protected string rootPath;
        // current path as TVFS or unix-like
        private string currentPath;

        public WindowsFileSystemHandler(string rootPath)
        {
            this.rootPath = rootPath;
            this.currentPath = "/";
        }

        public WindowsFileSystemHandler()
            : this(null)
        {
        }

        public virtual IFileSystemHandler Clone(IPEndPoint peer)
        {
            return new WindowsFileSystemHandler(rootPath);
        }

        public ResultOrError<string> GetCurrentDirectory()
        {
            //DebugLog.TraceMessage("currentPath: " + currentPath);
            return MakeResult<string>(currentPath);
        }

        public ResultOrError<string> ChangeDirectory(string path)
        {
            //DebugLog.TraceMessage("path: " + path);
            string newPath = ResolvePath(path);

            // special fake root for WinNT drives
            if (newPath == "/")
            {
                currentPath = newPath;
                return MakeResult<string>(newPath);
            }

            string realPath = DecodePath(newPath);
            if (!Directory.Exists(realPath))
                return MakeError<string>("Path does not exist.");

            currentPath = newPath;
            return MakeResult<string>(newPath);
        }

        public ResultOrError<string> ChangeToParentDirectory()
        {
            return ChangeDirectory("..");
        }

        public virtual ResultOrError<string> CreateDirectory(string path)
        {
            string newPath = ResolvePath(path);

            try
            {
                DirectoryInfo newDir = new DirectoryInfo(DecodePath(newPath));
                if (newDir.Exists)
                    return MakeError<string>("Directory already exists.");

                newDir.Create();
            }
            catch (Exception ex)
            {
                return MakeError<string>(ex.Message);
            }

            return MakeResult<string>(newPath);
        }

        public virtual ResultOrError<bool> RemoveDirectory(string path)
        {
            string newPath = ResolvePath(path);

            try
            {
                DirectoryInfo newDir = new DirectoryInfo(DecodePath(newPath));
                if (!newDir.Exists)
                    return MakeError<bool>("Directory does not exist.");

                if (newDir.GetFileSystemInfos().Length > 0)
                    return MakeError<bool>("Directory is not empty.");

                newDir.Delete();
            }
            catch (Exception ex)
            {
                return MakeError<bool>(ex.Message);
            }

            return MakeResult<bool>(true);
        }

        public ResultOrError<Stream> ReadFile(string path)
        {
            string newPath = ResolvePath(path);
            string realPath = DecodePath(newPath);

            if (!File.Exists(realPath))
                return MakeError<Stream>("File does not exist.");

            try
            {
                return MakeResult<Stream>(File.OpenRead(realPath));
            }
            catch (Exception ex)
            {
                return MakeError<Stream>(ex.Message);
            }
        }

        public virtual ResultOrError<Stream> WriteFile(string path)
        {
            string newPath = ResolvePath(path);
            string realPath = DecodePath(newPath);

            try
            {
                return MakeResult<Stream>(File.Open(realPath, FileMode.OpenOrCreate));
            }
            catch (Exception ex)
            {
                return MakeError<Stream>(ex.Message);
            }
        }

        public virtual ResultOrError<bool> RemoveFile(string path)
        {
            string newPath = ResolvePath(path);
            string realPath = DecodePath(newPath);

            if (!File.Exists(realPath))
                return MakeError<bool>("File does not exist.");

            try
            {
                File.Delete(realPath);
            }
            catch (Exception ex)
            {
                return MakeError<bool>(ex.Message);
            }

            return MakeResult<bool>(true);
        }

        public virtual ResultOrError<bool> RenameFile(string fromPath, string toPath)
        {
            string realFromPath = DecodePath(ResolvePath(fromPath));
            string realToPath = DecodePath(ResolvePath(toPath));

            if (!File.Exists(realFromPath) && !Directory.Exists(realFromPath))
                return MakeError<bool>("Source path does not exist.");

            if (File.Exists(realToPath) || Directory.Exists(realToPath))
                return MakeError<bool>("Target path already exists.");

            try
            {
                File.Move(realFromPath, realToPath);
            }
            catch (Exception ex)
            {
                return MakeError<bool>(ex.Message);
            }

            return MakeResult<bool>(true);
        }

        public ResultOrError<FileSystemEntry[]> ListEntries(string path)
        {
            //DebugLog.TraceMessage("path: " + path);
            string newPath = ResolvePath(path);
            if (newPath == null)
                newPath = currentPath;

            List<FileSystemEntry> result = new List<FileSystemEntry>();

            // special fake root for WinNT drives
            if (newPath == "/" && rootPath == null)
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    if (!drive.IsReady)
                        continue;

                    FileSystemEntry entry = new FileSystemEntry();
                    entry.Name = drive.Name[0].ToString();
                    entry.IsDirectory = true;
                    entry.Size = drive.TotalSize;
                    entry.LastModifiedTimeUtc = DateTime.MinValue;
                    result.Add(entry);
                }

                return MakeResult<FileSystemEntry[]>(result.ToArray());
            }

            string realPath = DecodePath(newPath);
            FileSystemInfo[] files;

            if (File.Exists(realPath))
                files = new FileSystemInfo[] { new FileInfo(realPath) };
            else if (Directory.Exists(realPath))
                files = new DirectoryInfo(realPath).GetFileSystemInfos();
            else
                return MakeError<FileSystemEntry[]>("Path does not exist.");

            foreach (FileSystemInfo file in files)
            {
                FileSystemEntry entry = new FileSystemEntry();
                entry.Name = file.Name;
                // CF is missing FlagsAttribute.HasFlag
                entry.IsDirectory = ((file.Attributes & FileAttributes.Directory) == FileAttributes.Directory);
                entry.Size = (entry.IsDirectory ? 0 : ((FileInfo)file).Length);
                entry.LastModifiedTimeUtc = file.LastWriteTime.ToUniversalTime();
                result.Add(entry);
            }

            return MakeResult<FileSystemEntry[]>(result.ToArray());
        }

        public ResultOrError<long> GetFileSize(string path)
        {
            string realPath = DecodePath(ResolvePath(path));
            if (Directory.Exists(realPath))
                return MakeError<long>("Cannot get size of directory.");
            else if (!File.Exists(realPath))
                return MakeError<long>("File does not exist.");

            long size = new FileInfo(realPath).Length;
            return MakeResult<long>(size);
        }

        public ResultOrError<DateTime> GetLastModifiedTimeUtc(string path)
        {
            string realPath = DecodePath(ResolvePath(path));
            if (!File.Exists(realPath))
                return MakeError<DateTime>("File does not exist.");

            // CF is missing FileInfo.LastWriteTimeUtc
            DateTime time = new FileInfo(realPath).LastWriteTime.ToUniversalTime();
            return MakeResult<DateTime>(time);
        }

        private string ResolvePath(string path)
        {
            return FileSystemHelper.ResolvePath(currentPath, path);
        }

        private string EncodePath(string path)
        {
            return "/" + path[0] + (path.Length > 2 ? path.Substring(2).Replace(@"\", "/") : "");
        }

        // decode vfs path (for example: "/") to Windows path (for example: "c:\")
        private string DecodePath(string path)
        {
            if (path == null || path == "" || path[0] != '/')
                return rootPath;

            // some error checking for the drive layer
            if (path == "/")
                return rootPath;    // should have been caught elsewhere

            if (path.Length > 1 && path[0] == '/')
                return System.IO.Path.Combine(rootPath, path.Substring(1));

            if (path.Length > 2 && path[2] != '/')
                return System.IO.Path.Combine(rootPath, path);

            if (path.Length < 4) // e.g. "/C/"
                return path[1] + @":\";
            else
                return path[1] + @":\" + path.Substring(3).Replace("/", @"\");
        }

        /// <summary>
        /// Shortcut for ResultOrError<T>.MakeResult()
        /// </summary>
        private ResultOrError<T> MakeResult<T>(T result)
        {
            return ResultOrError<T>.MakeResult(result);
        }

        /// <summary>
        /// Shortcut for ResultOrError<T>.MakeError()
        /// </summary>
        protected ResultOrError<T> MakeError<T>(string error)
        {
            return ResultOrError<T>.MakeError(error);
        }
    }
}
