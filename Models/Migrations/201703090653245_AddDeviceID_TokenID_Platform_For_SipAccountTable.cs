namespace ICMServer.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeviceID_TokenID_Platform_For_SipAccountTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("sipaccount", "Platform", c => c.String(unicode: false, maxLength: 255, storeType: "nvarchar"));
            AddColumn("sipaccount", "DeviceID", c => c.String(unicode: false, maxLength: 255, storeType: "nvarchar"));
            AddColumn("sipaccount", "TokenID", c => c.String(unicode: false, maxLength: 255, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("sipaccount", "TokenID");
            DropColumn("sipaccount", "DeviceID");
            DropColumn("sipaccount", "Platform");
        }
    }
}
