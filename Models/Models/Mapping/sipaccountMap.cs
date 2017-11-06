using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class sipaccountMap : EntityTypeConfiguration<sipaccount>
    {
        public sipaccountMap()
        {
            // Primary Key
            this.HasKey(t => t.C_user);

            // Properties
            this.Property(t => t.C_user)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.C_password)
                .HasMaxLength(255);

            this.Property(t => t.C_room)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.C_usergroup)
                .HasMaxLength(255);

            this.Property(t => t.C_randomcode)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("sipaccount", "icmdb");
            this.Property(t => t.C_user).HasColumnName("_user");
            this.Property(t => t.C_password).HasColumnName("_password");
            this.Property(t => t.C_room).HasColumnName("_room");
            this.Property(t => t.C_usergroup).HasColumnName("_usergroup");
            this.Property(t => t.C_randomcode).HasColumnName("_randomcode");
            this.Property(t => t.C_updatetime).HasColumnName("_updatetime");
            this.Property(t => t.C_registerstatus).HasColumnName("_registerstatus");
            this.Property(t => t.C_sync).HasColumnName("_sync");
        }
    }
}
