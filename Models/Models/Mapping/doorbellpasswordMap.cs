using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class doorbellpasswordMap : EntityTypeConfiguration<doorbellpassword>
    {
        public doorbellpasswordMap()
        {
            // Primary Key
            this.HasKey(t => t.C_id);

            // Properties
            this.Property(t => t.C_roomid)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.C_password)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("doorbellpassword", "icmdb");
            this.Property(t => t.C_id).HasColumnName("_id");
            this.Property(t => t.C_roomid).HasColumnName("_roomid");
            this.Property(t => t.C_password).HasColumnName("_password");
            this.Property(t => t.C_time).HasColumnName("_time");
        }
    }
}
