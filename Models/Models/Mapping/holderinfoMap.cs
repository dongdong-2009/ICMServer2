using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class holderinfoMap : EntityTypeConfiguration<holderinfo>
    {
        public holderinfoMap()
        {
            // Primary Key
            this.HasKey(t => t.C_id);

            // Properties
            this.Property(t => t.C_name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.C_phoneno)
                .HasMaxLength(50);

            this.Property(t => t.C_roomid)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("holderinfo", "icmdb");
            this.Property(t => t.C_id).HasColumnName("_id");
            this.Property(t => t.C_name).HasColumnName("_name");
            this.Property(t => t.C_sex).HasColumnName("_sex");
            this.Property(t => t.C_phoneno).HasColumnName("_phoneno");
            this.Property(t => t.C_roomid).HasColumnName("_roomid");
            this.Property(t => t.C_isholder).HasColumnName("_isholder");
        }
    }
}
