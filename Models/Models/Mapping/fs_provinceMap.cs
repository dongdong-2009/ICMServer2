using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class fs_provinceMap : EntityTypeConfiguration<fs_province>
    {
        public fs_provinceMap()
        {
            // Primary Key
            this.HasKey(t => t.ProvinceID);

            // Properties
            this.Property(t => t.ProvinceID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ProvinceName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("fs_province", "icmdb");
            this.Property(t => t.ProvinceID).HasColumnName("ProvinceID");
            this.Property(t => t.ProvinceName).HasColumnName("ProvinceName");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.DateUpdated).HasColumnName("DateUpdated");
        }
    }
}
