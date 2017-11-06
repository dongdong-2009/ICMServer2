using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class fs_districtMap : EntityTypeConfiguration<fs_district>
    {
        public fs_districtMap()
        {
            // Primary Key
            this.HasKey(t => t.DistrictID);

            // Properties
            this.Property(t => t.DistrictID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DistrictName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("fs_district", "icmdb");
            this.Property(t => t.DistrictID).HasColumnName("DistrictID");
            this.Property(t => t.DistrictName).HasColumnName("DistrictName");
            this.Property(t => t.CityID).HasColumnName("CityID");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.DateUpdated).HasColumnName("DateUpdated");
        }
    }
}
