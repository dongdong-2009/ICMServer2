using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class authorityMap : EntityTypeConfiguration<authority>
    {
        public authorityMap()
        {
            // Primary Key
            this.HasKey(t => t.C_id);

            // Properties
            this.Property(t => t.C_name)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("authority", "icmdb");
            this.Property(t => t.C_id).HasColumnName("_id");
            this.Property(t => t.C_name).HasColumnName("_name");
            this.Property(t => t.C_authority).HasColumnName("_authority");
        }
    }
}
