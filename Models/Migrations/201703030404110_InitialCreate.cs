namespace ICMServer.Models.Migrations
{
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        private bool AnyTableExists()
        {
            using (var db = new ICMDBContext())
            {
                try
                {
                    db.Users.Load();
                    return true;
                }
                catch (Exception) { }
            }
            return false;
        }

        public override void Up()
        {
            if (AnyTableExists())
            {
                DropTable("fs_city");
                DropTable("fs_district");
                DropTable("fs_province");

                CreateTable(
                    "fs_city",
                    c => new
                    {
                        CityID = c.Long(nullable: false),
                        Country = c.String(maxLength: 10, storeType: "nvarchar"),
                        CityName = c.String(maxLength: 50, storeType: "nvarchar"),
                        ZipCode = c.String(maxLength: 50, storeType: "nvarchar"),
                        ProvinceID = c.Long(),
                        DateCreated = c.DateTime(precision: 0),
                        DateUpdated = c.DateTime(precision: 0),
                    })
                    .PrimaryKey(t => t.CityID);

                CreateTable(
                    "fs_district",
                    c => new
                    {
                        DistrictID = c.Long(nullable: false),
                        DistrictName = c.String(maxLength: 50, storeType: "nvarchar"),
                        CityID = c.Long(),
                        DateCreated = c.DateTime(precision: 0),
                        DateUpdated = c.DateTime(precision: 0),
                    })
                    .PrimaryKey(t => t.DistrictID);

                CreateTable(
                    "fs_province",
                    c => new
                    {
                        ProvinceID = c.Long(nullable: false),
                        ProvinceName = c.String(maxLength: 50, storeType: "nvarchar"),
                        DateCreated = c.DateTime(precision: 0),
                        DateUpdated = c.DateTime(precision: 0),
                    })
                    .PrimaryKey(t => t.ProvinceID);
            }
            else
            {
                CreateTable(
                    "addrassociate",
                    c => new
                        {
                            _id = c.Int(nullable: false, identity: true),
                            _addrA = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                            _addrB = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                            _typeA = c.Int(),
                            _typeB = c.Int(),
                            _des = c.String(maxLength: 255, storeType: "nvarchar"),
                        })
                    .PrimaryKey(t => t._id);

                CreateTable(
                    "advertisement",
                    c => new
                        {
                            _id = c.Int(nullable: false, identity: true),
                            _no = c.Int(),
                            _title = c.String(maxLength: 255, storeType: "nvarchar"),
                            _time = c.DateTime(precision: 0),
                            _path = c.String(maxLength: 255, storeType: "nvarchar"),
                            _checksum = c.String(maxLength: 32, storeType: "nvarchar"),
                        })
                    .PrimaryKey(t => t._id);

                CreateTable(
                    "authority",
                    c => new
                        {
                            _id = c.Int(nullable: false, identity: true),
                            _name = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                            _authority = c.Int(),
                        })
                    .PrimaryKey(t => t._id);

                CreateTable(
                    "buildproperty",
                    c => new
                        {
                            _qu = c.String(nullable: false, maxLength: 5, storeType: "nvarchar"),
                            _dong = c.String(nullable: false, maxLength: 5, storeType: "nvarchar"),
                            _type = c.Int(),
                        })
                    .PrimaryKey(t => new { t._qu, t._dong });

                CreateTable(
                    "Device",
                    c => new
                        {
                            _id = c.Int(nullable: false, identity: true),
                            _ip = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                            _roomid = c.String(maxLength: 50, storeType: "nvarchar"),
                            _alias = c.String(maxLength: 50, storeType: "nvarchar"),
                            _group = c.String(maxLength: 20, storeType: "nvarchar"),
                            _mac = c.String(maxLength: 20, storeType: "nvarchar"),
                            _status = c.Int(nullable: false),
                            _type = c.Int(),
                            _sm = c.String(maxLength: 20, storeType: "nvarchar"),
                            _gw = c.String(maxLength: 20, storeType: "nvarchar"),
                            _cameraid = c.String(maxLength: 20, storeType: "nvarchar"),
                            _camerapw = c.String(maxLength: 20, storeType: "nvarchar"),
                            _sd = c.Int(),
                            _aVer = c.String(maxLength: 20, storeType: "nvarchar"),
                            _cVer = c.String(maxLength: 20, storeType: "nvarchar"),
                            _fVer = c.String(maxLength: 20, storeType: "nvarchar"),
                            _laVer = c.String(maxLength: 20, storeType: "nvarchar"),
                            _lcVer = c.String(maxLength: 20, storeType: "nvarchar"),
                            _lfVer = c.String(maxLength: 20, storeType: "nvarchar"),
                        })
                    .PrimaryKey(t => t._id);

                CreateTable(
                    "doorbellpassword",
                    c => new
                        {
                            _id = c.Int(nullable: false, identity: true),
                            _roomid = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                            _password = c.String(maxLength: 20, storeType: "nvarchar"),
                            _time = c.DateTime(precision: 0),
                        })
                    .PrimaryKey(t => t._id);

                CreateTable(
                    "eventcallout",
                    c => new
                        {
                            _from = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                            _time = c.DateTime(nullable: false, precision: 0),
                            _to = c.String(maxLength: 20, storeType: "nvarchar"),
                            _ower = c.String(maxLength: 50, storeType: "nvarchar"),
                            _type = c.Int(),
                            _action = c.String(maxLength: 250, storeType: "nvarchar"),
                            _img = c.String(maxLength: 250, storeType: "nvarchar"),
                        })
                    .PrimaryKey(t => new { t._from, t._time });

                CreateTable(
                    "eventcommon",
                    c => new
                        {
                            _srcaddr = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                            _time = c.DateTime(nullable: false, precision: 0),
                            _handlestatus = c.Int(),
                            _handletime = c.DateTime(precision: 0),
                            _type = c.Int(),
                            _content = c.String(maxLength: 250, storeType: "nvarchar"),
                            _action = c.String(maxLength: 250, storeType: "nvarchar"),
                            _handler = c.String(maxLength: 250, storeType: "nvarchar"),
                        })
                    .PrimaryKey(t => new { t._srcaddr, t._time });

                CreateTable(
                    "eventopendoor",
                    c => new
                        {
                            _from = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                            _time = c.DateTime(nullable: false, precision: 0),
                            _mode = c.String(maxLength: 20, storeType: "nvarchar"),
                            _open_object = c.String(maxLength: 50, storeType: "nvarchar"),
                            _verified = c.Int(),
                            _img = c.String(maxLength: 250, storeType: "nvarchar"),
                        })
                    .PrimaryKey(t => new { t._from, t._time });

                CreateTable(
                    "eventwarn",
                    c => new
                        {
                            _srcaddr = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                            _time = c.DateTime(nullable: false, precision: 0),
                            _channel = c.Int(nullable: false),
                            _action = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                            _handlestatus = c.Int(),
                            _handletime = c.DateTime(precision: 0),
                            _type = c.String(maxLength: 50, storeType: "nvarchar"),
                            _handler = c.String(maxLength: 250, storeType: "nvarchar"),
                        })
                    .PrimaryKey(t => new { t._srcaddr, t._time, t._channel, t._action });

                CreateTable(
                    "fs_city",
                    c => new
                        {
                            CityID = c.Long(nullable: false),
                            Country = c.String(maxLength: 10, storeType: "nvarchar"),
                            CityName = c.String(maxLength: 50, storeType: "nvarchar"),
                            ZipCode = c.String(maxLength: 50, storeType: "nvarchar"),
                            ProvinceID = c.Long(),
                            DateCreated = c.DateTime(precision: 0),
                            DateUpdated = c.DateTime(precision: 0),
                        })
                    .PrimaryKey(t => t.CityID);

                CreateTable(
                    "fs_district",
                    c => new
                        {
                            DistrictID = c.Long(nullable: false),
                            DistrictName = c.String(maxLength: 50, storeType: "nvarchar"),
                            CityID = c.Long(),
                            DateCreated = c.DateTime(precision: 0),
                            DateUpdated = c.DateTime(precision: 0),
                        })
                    .PrimaryKey(t => t.DistrictID);

                CreateTable(
                    "fs_province",
                    c => new
                        {
                            ProvinceID = c.Long(nullable: false),
                            ProvinceName = c.String(maxLength: 50, storeType: "nvarchar"),
                            DateCreated = c.DateTime(precision: 0),
                            DateUpdated = c.DateTime(precision: 0),
                        })
                    .PrimaryKey(t => t.ProvinceID);

                CreateTable(
                    "heartbeatlog",
                    c => new
                        {
                            _id = c.Int(nullable: false, identity: true),
                            _log = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                            _logtime = c.DateTime(precision: 0),
                        })
                    .PrimaryKey(t => t._id);

                CreateTable(
                    "holderinfo",
                    c => new
                        {
                            _id = c.Int(nullable: false, identity: true),
                            _name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                            _sex = c.Int(),
                            _phoneno = c.String(maxLength: 50, storeType: "nvarchar"),
                            _roomid = c.String(maxLength: 50, storeType: "nvarchar"),
                            _isholder = c.Int(),
                        })
                    .PrimaryKey(t => t._id);

                CreateTable(
                    "iccard",
                    c => new
                        {
                            _icid = c.Int(nullable: false, identity: true),
                            _roomid = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                            _icno = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                            _username = c.String(maxLength: 50, storeType: "nvarchar"),
                            _ictype = c.Int(),
                            _icpassword = c.String(maxLength: 50, storeType: "nvarchar"),
                            _available = c.Int(),
                            _time = c.DateTime(precision: 0),
                            _uptime = c.DateTime(precision: 0),
                            _downtime = c.DateTime(precision: 0),
                        })
                    .PrimaryKey(t => new { t._icid, t._roomid });

                CreateTable(
                    "icmap",
                    c => new
                        {
                            _id = c.Int(nullable: false, identity: true),
                            _icno = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                            _entrancedoor = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        })
                    .PrimaryKey(t => t._id);

                CreateTable(
                    "leaveword",
                    c => new
                        {
                            _id = c.Int(nullable: false, identity: true),
                            _filenames = c.String(maxLength: 250, storeType: "nvarchar"),
                            _src_addr = c.String(maxLength: 20, storeType: "nvarchar"),
                            _dst_addr = c.String(maxLength: 20, storeType: "nvarchar"),
                            _ttime = c.String(maxLength: 20, storeType: "nvarchar"),
                            _readflag = c.Int(),
                        })
                    .PrimaryKey(t => t._id);

                CreateTable(
                    "photograph",
                    c => new
                        {
                            _id = c.Int(nullable: false, identity: true),
                            _srcaddr = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                            _time = c.DateTime(precision: 0),
                            _img = c.Binary(),
                        })
                    .PrimaryKey(t => t._id);

                CreateTable(
                    "publishinfo",
                    c => new
                        {
                            _id = c.Int(nullable: false, identity: true),
                            _title = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                            _dstaddr = c.String(maxLength: 50, storeType: "nvarchar"),
                            _time = c.DateTime(precision: 0),
                            _filepath = c.String(maxLength: 250, storeType: "nvarchar"),
                            _type = c.Int(),
                            _fmt = c.Int(),
                            _isread = c.Int(),
                        })
                    .PrimaryKey(t => t._id);

                CreateTable(
                    "sipaccount",
                    c => new
                        {
                            _user = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                            _password = c.String(maxLength: 255, storeType: "nvarchar"),
                            _room = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                            _usergroup = c.String(maxLength: 255, storeType: "nvarchar"),
                            _randomcode = c.String(maxLength: 255, storeType: "nvarchar"),
                            _updatetime = c.DateTime(precision: 0),
                            _registerstatus = c.Int(),
                            _sync = c.Int(),
                        })
                    .PrimaryKey(t => t._user);

                CreateTable(
                    "upgrade",
                    c => new
                        {
                            _id = c.Int(nullable: false, identity: true),
                            _filepath = c.String(maxLength: 250, storeType: "nvarchar"),
                            _ver = c.String(maxLength: 50, storeType: "nvarchar"),
                            _ustype = c.Int(),
                            _type = c.Int(),
                            _time = c.DateTime(precision: 0),
                            _def = c.Int(),
                        })
                    .PrimaryKey(t => t._id);

                CreateTable(
                    "user",
                    c => new
                        {
                            _id = c.Int(nullable: false, identity: true),
                            _userno = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                            _username = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                            _powerid = c.Int(),
                            _password = c.String(maxLength: 20, storeType: "nvarchar"),
                        })
                    .PrimaryKey(t => t._id);

                Sql("INSERT INTO `user` (`_id`, `_userno`, `_username`, `_powerid`, `_password`) VALUE(1, '000000', 'admin', 0, '123456')");
            }

            SqlFile("0.initial.sql");
        }

        public override void Down()
        {
            DropTable("user");
            DropTable("upgrade");
            DropTable("sipaccount");
            DropTable("publishinfo");
            DropTable("photograph");
            DropTable("leaveword");
            DropTable("icmap");
            DropTable("iccard");
            DropTable("holderinfo");
            DropTable("heartbeatlog");
            DropTable("fs_province");
            DropTable("fs_district");
            DropTable("fs_city");
            DropTable("eventwarn");
            DropTable("eventopendoor");
            DropTable("eventcommon");
            DropTable("eventcallout");
            DropTable("doorbellpassword");
            DropTable("Device");
            DropTable("buildproperty");
            DropTable("authority");
            DropTable("advertisement");
            DropTable("addrassociate");
        }
    }
}
