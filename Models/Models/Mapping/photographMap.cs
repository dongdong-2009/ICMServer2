using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class photographMap : EntityTypeConfiguration<photograph>
    {
        public photographMap()
        {
            // Primary Key
            this.HasKey(t => t.C_id);

            // Properties
            this.Property(t => t.C_srcaddr)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("photograph", "icmdb");
            this.Property(t => t.C_id).HasColumnName("_id");
            this.Property(t => t.C_srcaddr).HasColumnName("_srcaddr");
            this.Property(t => t.C_time).HasColumnName("_time");
            this.Property(t => t.C_img).HasColumnName("_img");
        }
    }
}
