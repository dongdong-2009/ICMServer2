using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class eventopendoorMap : EntityTypeConfiguration<eventopendoor>
    {
        public eventopendoorMap()
        {
            // Primary Key
            this.HasKey(t => new { t.C_from, t.C_time });

            // Properties
            this.Property(t => t.C_from)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.C_mode)
                .HasMaxLength(20);

            this.Property(t => t.C_open_object)
                .HasMaxLength(50);

            this.Property(t => t.C_img)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("eventopendoor", "icmdb");
            this.Property(t => t.C_from).HasColumnName("_from");
            this.Property(t => t.C_mode).HasColumnName("_mode");
            this.Property(t => t.C_open_object).HasColumnName("_open_object");
            this.Property(t => t.C_time).HasColumnName("_time");
            this.Property(t => t.C_verified).HasColumnName("_verified");
            this.Property(t => t.C_img).HasColumnName("_img");
        }
    }
}
