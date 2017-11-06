using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class publishinfoMap : EntityTypeConfiguration<publishinfo>
    {
        public publishinfoMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.title)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.dstaddr)
                .HasMaxLength(50);

            this.Property(t => t.filepath)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("publishinfo", "icmdb");
            this.Property(t => t.id).HasColumnName("_id");
            this.Property(t => t.title).HasColumnName("_title");
            this.Property(t => t.dstaddr).HasColumnName("_dstaddr");
            this.Property(t => t.time).HasColumnName("_time");
            this.Property(t => t.filepath).HasColumnName("_filepath");
            this.Property(t => t.type).HasColumnName("_type");
            this.Property(t => t.fmt).HasColumnName("_fmt");
            this.Property(t => t.isread).HasColumnName("_isread");
        }
    }
}
