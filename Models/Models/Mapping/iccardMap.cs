using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class iccardMap : EntityTypeConfiguration<Iccard>
    {
        public iccardMap()
        {
            // Primary Key
            this.HasKey(t => new { t.C_icid, t.C_roomid });

            // Properties
            this.Property(t => t.C_icid)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.C_icno)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.C_roomid)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.C_username)
                .HasMaxLength(50);

            this.Property(t => t.C_icpassword)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("iccard", "icmdb");
            this.Property(t => t.C_icid).HasColumnName("_icid");
            this.Property(t => t.C_icno).HasColumnName("_icno");
            this.Property(t => t.C_roomid).HasColumnName("_roomid");
            this.Property(t => t.C_username).HasColumnName("_username");
            this.Property(t => t.C_ictype).HasColumnName("_ictype");
            this.Property(t => t.C_icpassword).HasColumnName("_icpassword");
            this.Property(t => t.C_available).HasColumnName("_available");
            this.Property(t => t.C_time).HasColumnName("_time");
            this.Property(t => t.C_uptime).HasColumnName("_uptime");
            this.Property(t => t.C_downtime).HasColumnName("_downtime");
        }
    }
}
