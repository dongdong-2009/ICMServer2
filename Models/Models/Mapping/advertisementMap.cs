using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class advertisementMap : EntityTypeConfiguration<advertisement>
    {
        public advertisementMap()
        {
            // Primary Key
            this.HasKey(t => t.C_id);

            // Properties
            this.Property(t => t.C_title)
                .HasMaxLength(255);

            this.Property(t => t.C_path)
                .HasMaxLength(255);

            this.Property(t => t.C_checksum)
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("advertisement", "icmdb");
            this.Property(t => t.C_id).HasColumnName("_id");
            this.Property(t => t.C_no).HasColumnName("_no");
            this.Property(t => t.C_title).HasColumnName("_title");
            this.Property(t => t.C_time).HasColumnName("_time");
            this.Property(t => t.C_path).HasColumnName("_path");
            this.Property(t => t.C_checksum).HasColumnName("_checksum");
        }
    }
}
