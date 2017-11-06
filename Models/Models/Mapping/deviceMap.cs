using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class DeviceMap : EntityTypeConfiguration<Device>
    {
        public DeviceMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.ip)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.roomid)
                .HasMaxLength(50);

            this.Property(t => t.Alias)
                .HasMaxLength(50);

            this.Property(t => t.group)
                .HasMaxLength(20);

            this.Property(t => t.mac)
                .HasMaxLength(20);

            this.Property(t => t.sm)
                .HasMaxLength(20);

            this.Property(t => t.gw)
                .HasMaxLength(20);

            this.Property(t => t.cameraid)
                .HasMaxLength(20);

            this.Property(t => t.camerapw)
                .HasMaxLength(20);

            this.Property(t => t.AVer)
                .HasMaxLength(20);

            this.Property(t => t.cVer)
                .HasMaxLength(20);

            this.Property(t => t.fVer)
                .HasMaxLength(20);

            this.Property(t => t.laVer)
                .HasMaxLength(20);

            this.Property(t => t.lcVer)
                .HasMaxLength(20);

            this.Property(t => t.lfVer)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Device", "icmdb");
            this.Property(t => t.id).HasColumnName("_id");
            this.Property(t => t.ip).HasColumnName("_ip");
            this.Property(t => t.roomid).HasColumnName("_roomid");
            this.Property(t => t.Alias).HasColumnName("_alias");
            this.Property(t => t.group).HasColumnName("_group");
            this.Property(t => t.mac).HasColumnName("_mac");
            this.Property(t => t.online).HasColumnName("_status");
            this.Property(t => t.type).HasColumnName("_type");
            this.Property(t => t.sm).HasColumnName("_sm");
            this.Property(t => t.gw).HasColumnName("_gw");
            this.Property(t => t.cameraid).HasColumnName("_cameraid");
            this.Property(t => t.camerapw).HasColumnName("_camerapw");
            this.Property(t => t.sd).HasColumnName("_sd");
            this.Property(t => t.AVer).HasColumnName("_aVer");
            this.Property(t => t.cVer).HasColumnName("_cVer");
            this.Property(t => t.fVer).HasColumnName("_fVer");
            this.Property(t => t.laVer).HasColumnName("_laVer");
            this.Property(t => t.lcVer).HasColumnName("_lcVer");
            this.Property(t => t.lfVer).HasColumnName("_lfVer");
        }
    }
}
