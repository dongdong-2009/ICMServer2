using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class eventcalloutMap : EntityTypeConfiguration<eventcallout>
    {
        public eventcalloutMap()
        {
            // Primary Key
            this.HasKey(t => new { t.from, t.time });

            // Properties
            this.Property(t => t.from)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.to)
                .HasMaxLength(20);

            this.Property(t => t.owner)
                .HasMaxLength(50);

            this.Property(t => t.action)
                .HasMaxLength(250);

            this.Property(t => t.img)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("eventcallout", "icmdb");
            this.Property(t => t.from).HasColumnName("_from");
            this.Property(t => t.to).HasColumnName("_to");
            this.Property(t => t.owner).HasColumnName("_ower");
            this.Property(t => t.time).HasColumnName("_time");
            this.Property(t => t.type).HasColumnName("_type");
            this.Property(t => t.action).HasColumnName("_action");
            this.Property(t => t.img).HasColumnName("_img");
        }
    }
}
