using mooftpserv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Net
{
    public class FileSystemHandler : WindowsFileSystemHandler
    {
        private const string PERMISSION_DENIED = "Permission denied.";

        public FileSystemHandler(string rootPath) : base(rootPath) { }

        public override ResultOrError<string> CreateDirectory(string path)
        {
            return MakeError<string>(PERMISSION_DENIED);
        }
        public override ResultOrError<bool> RemoveDirectory(string path)
        {
            return MakeError<bool>(PERMISSION_DENIED);
        }

        public override ResultOrError<Stream> WriteFile(string path)
        {
            return MakeError<Stream>(PERMISSION_DENIED);
        }

        public override ResultOrError<bool> RemoveFile(string path)
        {
            return MakeError<bool>(PERMISSION_DENIED);
        }

        public override ResultOrError<bool> RenameFile(string fromPath, string toPath)
        {
            return MakeError<bool>(PERMISSION_DENIED);
        }

        public override IFileSystemHandler Clone(IPEndPoint peer)
        {
            return new FileSystemHandler(rootPath);
        }
    }
}
