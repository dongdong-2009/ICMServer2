using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class fs_cityMap : EntityTypeConfiguration<fs_city>
    {
        public fs_cityMap()
        {
            // Primary Key
            this.HasKey(t => t.CityID);

            // Properties
            this.Property(t => t.Country)
                .HasMaxLength(10);

            this.Property(t => t.CityID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CityName)
                .HasMaxLength(50);

            this.Property(t => t.ZipCode)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("fs_city", "icmdb");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.CityID).HasColumnName("CityID");
            this.Property(t => t.CityName).HasColumnName("CityName");
            this.Property(t => t.ZipCode).HasColumnName("ZipCode");
            this.Property(t => t.ProvinceID).HasColumnName("ProvinceID");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.DateUpdated).HasColumnName("DateUpdated");
        }
    }
}
