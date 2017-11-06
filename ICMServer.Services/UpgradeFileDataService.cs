using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Services.Data
{
    public class UpgradeFileDataService : DataService<upgrade>
    {
        public override void Delete(upgrade obj)
        {
            if (obj == null)
                return;

            CreateDbTableRepository().Delete(obj).SaveChanges();

            // delete file and directory
            try
            {
                string directoryPath = System.IO.Path.GetDirectoryName(
                    Config.Instance.FTPServerRootDir + @"\" + obj.filepath);
                new System.IO.DirectoryInfo(directoryPath).Delete(true);
            }
            catch (Exception) { }
        }

        public override void Delete(Func<upgrade, bool> predicate)
        {
            var objs = this.Select(predicate);
            CreateDbTableRepository().Delete(predicate).SaveChanges();
            foreach (var obj in objs)
            {
                try
                {
                    new System.IO.DirectoryInfo(Config.Instance.FTPServerRootDir + @"\" + obj.filepath).Delete(true);
                }
                catch (Exception) { }
            }
        }

        public override void DeleteAll()
        {
            CreateDbTableRepository().DeleteAll().SaveChanges();
            new System.IO.DirectoryInfo(Path.GetUpgradeDataBaseFolderPath()).Empty();
        }
    }
}
