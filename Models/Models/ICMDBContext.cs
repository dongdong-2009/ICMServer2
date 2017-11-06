using ICMServer;
using ICMServer.Models.Mapping;
using ICMServer.Models.Migrations;
using MySql.Data.MySqlClient;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ICMServer.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public partial class ICMDBContext : DbContext
    {
        // TODO: 动態改變 database 连接設定
        private static string dbHost = "localhost";
        private static string dbName = "icmdb";
        private static string dbUser = "root";
        private static string dbPassword = "123456";

        static ICMDBContext()
        {
            //Database.SetInitializer<ICMDBContext>(null);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ICMDBContext, Configuration>());
            //Database.SetInitializer(new ValidateDatabase<ICMDBContext>());
        }

        private static MySqlConnectionStringBuilder ConnectionStringBuilder
        {

            get
            {
                MySqlConnectionStringBuilder connectString = new MySqlConnectionStringBuilder
                {
                    Server = dbHost,
                    UserID = dbUser,
                    Password = dbPassword,
                    PersistSecurityInfo = true,
                    AllowUserVariables = true
                };
                return connectString;
            }
        }

        private static string ConnectString
        {
            get 
            {
                MySqlConnectionStringBuilder connectString = ConnectionStringBuilder;
                connectString.Database = dbName;
                return connectString.ToString();
            }
        }

        private static string ConnectStringWithoutDBName
        {
            get
            {
                MySqlConnectionStringBuilder connectString = ConnectionStringBuilder;
                return connectString.ToString();
            }
        }

        public ICMDBContext()
            : base(ConnectString)
        {
        }

        public static bool DatabaseExists()
        {
            using (var db = new ICMDBContext())
            {
                return db.Database.Exists();
            }
        }

        public static bool CheckConnection(out string errorMsg)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectStringWithoutDBName))
                {
                    connection.Open();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                if (e.InnerException != null && e.InnerException.Message != null)
                    errorMsg = e.InnerException.Message;
                else
                    errorMsg = e.Message;
                return false;
            }
            //using (var db = new ICMDBContext())
            //{
            //    var entityConnection = db.Database.Connection.ConnectionString ;
            //    try
            //    {
            //        db.Database.Connection.Open();
            //        db.Database.Connection.Close();
            //    }
            //    catch (Exception e)
            //    {
            //        if (e.InnerException != null && e.InnerException.Message != null)
            //            errorMsg = e.InnerException.Message;
            //        else
            //            errorMsg = e.Message;
            //        return false;
            //    }
            //}
            errorMsg = "";
            return true;
        }

        public static user GetUserByName(string name)
        {
            using (var db = new ICMDBContext())
            {
                var result = (from user in db.Users
                              where user.C_username == name
                              select user).FirstOrDefault();
                return result;
            }
        }

        public static Device GetDeviceByAddress(string DeviceAddress)
        {
            using (var db = new ICMDBContext())
            {
                var result = (from Device in db.Devices
                              where Device.roomid == DeviceAddress
                              select Device).FirstOrDefault();
                return result;
            }
        }

        public static Task AddDevicesAsync(AddrList addrList, IProgress<int> progress = null)
        {
            return Task.Run(() => { AddDevices(addrList, progress); });
        }

        public static void AddDevices(AddrList addrList, IProgress<int> progress = null)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            using (var db = new ICMDBContext())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM Device");
                db.Database.ExecuteSqlCommand("ALTER TABLE Device AUTO_INCREMENT = 1");
                int processCount = 0;
                int reportedProgressPercentage = -1;
                foreach (var d in addrList.dev)
                {
                    Device dev = new Device
                    {
                        ip = d.ip,
                        roomid = d.ro,
                        Alias = (d.IsaliasNull()) ? null : d.alias,
                        group = (d.IsgroupNull()) ? null : d.group,
                        mac = (d.IsmcNull()) ? null : d.mc,
                        type = d.ty,
                        sm = d.sm,
                        gw = d.gw,
                        cameraid = (d.IsidNull()) ? null : d.id,
                        camerapw = (d.IspwNull()) ? null : d.pw
                    };
                    db.Devices.Add(dev);
                    processCount++;
                    if (progress != null)
                    {
                        int currentProgressPercentage = (processCount * 99 / addrList.dev.Count);
                        if (reportedProgressPercentage != currentProgressPercentage)
                        {
                            reportedProgressPercentage = currentProgressPercentage;
                            progress.Report(reportedProgressPercentage);
                        }
                    }
                }
                db.SaveChanges();
                if (progress != null)
                    progress.Report(100);
            }
            stopwatch.Stop();
            //DebugLog.TraceMessage(string.Format("done: {0}", stopwatch.Elapsed));
        }

        public DbSet<addrassociate> Addrassociates { get; set; }
        public DbSet<advertisement> Advertisements { get; set; }
        public DbSet<authority> Authorities { get; set; }
        public DbSet<buildproperty> Buildproperties { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<doorbellpassword> Doorbellpasswords { get; set; }
        public DbSet<eventcallout> Eventcallouts { get; set; }
        public DbSet<eventcommon> Eventcommons { get; set; }
        public DbSet<eventopendoor> Eventopendoors { get; set; }
        public DbSet<eventwarn> Eventwarns { get; set; }
        public DbSet<fs_city> Fs_city { get; set; }
        public DbSet<fs_district> Fs_district { get; set; }
        public DbSet<fs_province> Fs_province { get; set; }
        public DbSet<heartbeatlog> Heartbeatlogs { get; set; }
        public DbSet<holderinfo> Holderinfoes { get; set; }
        public DbSet<Iccard> Iccards { get; set; }
        public DbSet<icmap> Icmaps { get; set; }
        public DbSet<leaveword> Leavewords { get; set; }
        public DbSet<photograph> Photographs { get; set; }
        public DbSet<publishinfo> Publishinfoes { get; set; }
        public DbSet<sipaccount> Sipaccounts { get; set; }
        public DbSet<upgrade> Upgrades { get; set; }
        public DbSet<user> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new addrassociateMap());
            modelBuilder.Configurations.Add(new advertisementMap());
            modelBuilder.Configurations.Add(new authorityMap());
            modelBuilder.Configurations.Add(new buildpropertyMap());
            modelBuilder.Configurations.Add(new DeviceMap());
            modelBuilder.Configurations.Add(new doorbellpasswordMap());
            modelBuilder.Configurations.Add(new eventcalloutMap());
            modelBuilder.Configurations.Add(new eventcommonMap());
            modelBuilder.Configurations.Add(new eventopendoorMap());
            modelBuilder.Configurations.Add(new eventwarnMap());
            modelBuilder.Configurations.Add(new fs_cityMap());
            modelBuilder.Configurations.Add(new fs_districtMap());
            modelBuilder.Configurations.Add(new fs_provinceMap());
            modelBuilder.Configurations.Add(new heartbeatlogMap());
            modelBuilder.Configurations.Add(new holderinfoMap());
            modelBuilder.Configurations.Add(new iccardMap());
            modelBuilder.Configurations.Add(new icmapMap());
            modelBuilder.Configurations.Add(new leavewordMap());
            modelBuilder.Configurations.Add(new photographMap());
            modelBuilder.Configurations.Add(new publishinfoMap());
            modelBuilder.Configurations.Add(new sipaccountMap());
            modelBuilder.Configurations.Add(new upgradeMap());
            modelBuilder.Configurations.Add(new userMap());
        }
    }
}
