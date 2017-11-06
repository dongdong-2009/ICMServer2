using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class VideoMessageDataService : DataService<leaveword>
    {
        public static void DeleteVideoMessageFiles(string videoMsgFilename)
        {
            // delete files
            System.IO.FileInfo fiSdp = new System.IO.FileInfo(videoMsgFilename + ".sdp");
            System.IO.FileInfo fiRtpaD = new System.IO.FileInfo(videoMsgFilename + ".rtpa.data");
            System.IO.FileInfo fiRtpaI = new System.IO.FileInfo(videoMsgFilename + ".rtpa.index");
            System.IO.FileInfo fiRtpvD = new System.IO.FileInfo(videoMsgFilename + ".rtpv.data");
            System.IO.FileInfo fiRtpvI = new System.IO.FileInfo(videoMsgFilename + ".rtpv.index");
            try
            {
                fiSdp.Delete();
                fiRtpaD.Delete();
                fiRtpaI.Delete();
                fiRtpvD.Delete();
                fiRtpvI.Delete();
            }
            catch { }
        }

        public override void Delete(leaveword obj)
        {
            if (obj == null)
                return;

            CreateDbTableRepository().Delete(obj).SaveChanges();
            DeleteVideoMessageFiles(obj.filenames);
        }

        public override void Delete(Func<leaveword, bool> predicate)
        {
            var objs = this.Select(predicate);
            CreateDbTableRepository().Delete(predicate).SaveChanges();
            foreach (var obj in objs)
            {
                DeleteVideoMessageFiles(obj.filenames);
            }
        }

        public override void DeleteAll()
        {
            CreateDbTableRepository().DeleteAll().SaveChanges();
            new System.IO.DirectoryInfo(Path.GetVideoMessageBaseFolderPath()).Empty();
        }
    }
}
