namespace ICMServer.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameAndModifyThePublishInfoTableRelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "AnnouncementRoom",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AnnouncementID = c.Int(nullable: false),
                        RoomID = c.String(nullable: false, maxLength: 14, fixedLength: true, storeType: "nchar"),
                        HasRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Announcement", t => t.AnnouncementID, cascadeDelete: true)
                .ForeignKey("Room", t => t.RoomID, cascadeDelete: true)
                .Index(t => new { t.AnnouncementID, t.RoomID }, unique: true, name: "IX_Announcement_Room");
            
            CreateTable(
                "Announcement",
                c => new
                    {
                        _id = c.Int(nullable: false, identity: true),
                        _title = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        _time = c.DateTime(precision: 0),
                        _filepath = c.String(maxLength: 250, storeType: "nvarchar"),
                        _type = c.Int(),
                        _fmt = c.Int(),
                    })
                .PrimaryKey(t => t._id);
            
            CreateTable(
                "Room",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 14, fixedLength: true, storeType: "nchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "UpgradeTask",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DeviceID = c.Int(nullable: false),
                        UpgradeID = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        DeletedByUser = c.Boolean(nullable: false),
                        LastStartTime = c.DateTime(precision: 0),
                        LastUpdateTime = c.DateTime(precision: 0),
                        SentDataBytes = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                //.ForeignKey("device", t => t.DeviceID, cascadeDelete: true)
                //.ForeignKey("upgrade", t => t.UpgradeID, cascadeDelete: true)
                .Index(t => t.DeviceID, name: "IX_Device_ID")
                .Index(t => t.UpgradeID, name: "IX_Upgrade_ID");
            AddForeignKey("UpgradeTask", "DeviceID", "device", "_id", cascadeDelete: true);
            AddForeignKey("UpgradeTask", "UpgradeID", "upgrade", "_id", cascadeDelete: true);
            
            AddColumn("upgrade", "FileSize", c => c.Long(nullable: false));

            Sql("ALTER TABLE `iccard` DROP PRIMARY KEY, ADD PRIMARY KEY(`_icid`)");

            Sql("INSERT INTO `Room` (`ID`) SELECT DISTINCT SUBSTR(D._roomid, 1, 14) FROM `device` AS D");
            Sql("INSERT INTO `Announcement` (`_id`, `_title`, `_time`, `_filepath`, `_type`, `_fmt`) "
              + "SELECT MIN(P._id), P._title, P._time, P._filepath, P._type, P._fmt "
              + "FROM `publishinfo` AS P "
              + "GROUP BY P._time");
            Sql("INSERT INTO `AnnouncementRoom` (`AnnouncementID`, `RoomID`, `HasRead`) "
              + "SELECT ID, SUBSTR(P._dstaddr, 1, 14) AS RoomAddress, `_isread` FROM `publishinfo` AS P "
              + "JOIN(SELECT MIN(P._id) AS ID, P._time FROM `publishinfo` AS P  GROUP BY P._time) T ON T._time = P._time "
              + "GROUP BY ID, RoomAddress");
            DropTable("publishinfo");

            ReinitWeatherRelatedTables();
        }
        
        public override void Down()
        {
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
            Sql("INSERT INTO `publishinfo` (`_title`, `_dstaddr`, `_time`, `_filepath`, `_type`, `_fmt`, `_isread`) "
              + "SELECT A._title, AR.RoomID, A._time, A._filepath, A._type, A._fmt, AR.HasRead "
              + "FROM `Announcement` AS A, `AnnouncementRoom` AS AR WHERE A._id = AR.AnnouncementID");
            
            DropForeignKey("UpgradeTask", "UpgradeID", "upgrade");
            DropForeignKey("UpgradeTask", "DeviceID", "device");
            DropForeignKey("AnnouncementRoom", "RoomID", "Room");
            DropForeignKey("AnnouncementRoom", "AnnouncementID", "Announcement");
            DropIndex("UpgradeTask", "IX_Upgrade_ID");
            DropIndex("UpgradeTask", "IX_Device_ID");
            DropIndex("AnnouncementRoom", "IX_Announcement_Room");
            DropColumn("upgrade", "FileSize");
            DropTable("UpgradeTask");
            DropTable("Room");
            DropTable("Announcement");
            DropTable("AnnouncementRoom");

            Sql("ALTER TABLE `iccard` DROP PRIMARY KEY, ADD PRIMARY KEY(`_icid`, `_roomid`)");

            ReinitWeatherRelatedTables();
        }

        void ReinitWeatherRelatedTables()
        {
            // reinit fs_city, fs_district and fs_province tables
            DropTable("fs_city");
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

            DropTable("fs_district");
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

            DropTable("fs_province");
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

            SqlFile("0.initial.sql");
        }
    }
}
