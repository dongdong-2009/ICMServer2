using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class upgradeMap : EntityTypeConfiguration<upgrade>
    {
        public upgradeMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.filepath)
                .HasMaxLength(250);

            this.Property(t => t.version)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("upgrade", "icmdb");
            this.Property(t => t.id).HasColumnName("_id");
            this.Property(t => t.filepath).HasColumnName("_filepath");
            this.Property(t => t.version).HasColumnName("_ver");
            this.Property(t => t.filetype).HasColumnName("_ustype");
            this.Property(t => t.Device_type).HasColumnName("_type");
            this.Property(t => t.time).HasColumnName("_time");
            this.Property(t => t.is_default).HasColumnName("_def");
        }
    }
}
