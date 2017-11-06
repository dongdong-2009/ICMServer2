using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class icmapMap : EntityTypeConfiguration<icmap>
    {
        public icmapMap()
        {
            // Primary Key
            this.HasKey(t => t.C_id);

            // Properties
            this.Property(t => t.C_icno)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.C_entrancedoor)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("icmap", "icmdb");
            this.Property(t => t.C_id).HasColumnName("_id");
            this.Property(t => t.C_icno).HasColumnName("_icno");
            this.Property(t => t.C_entrancedoor).HasColumnName("_entrancedoor");
        }
    }
}
