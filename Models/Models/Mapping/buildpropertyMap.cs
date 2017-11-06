using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class buildpropertyMap : EntityTypeConfiguration<buildproperty>
    {
        public buildpropertyMap()
        {
            // Primary Key
            this.HasKey(t => new { t.C_qu, t.C_dong });

            // Properties
            this.Property(t => t.C_qu)
                .IsRequired()
                .HasMaxLength(5);

            this.Property(t => t.C_dong)
                .IsRequired()
                .HasMaxLength(5);

            // Table & Column Mappings
            this.ToTable("buildproperty", "icmdb");
            this.Property(t => t.C_qu).HasColumnName("_qu");
            this.Property(t => t.C_dong).HasColumnName("_dong");
            this.Property(t => t.C_type).HasColumnName("_type");
        }
    }
}
