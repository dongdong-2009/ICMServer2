using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class userMap : EntityTypeConfiguration<user>
    {
        public userMap()
        {
            // Primary Key
            this.HasKey(t => t.C_id);

            // Properties
            this.Property(t => t.C_userno)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.C_username)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.C_password)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("user", "icmdb");
            this.Property(t => t.C_id).HasColumnName("_id");
            this.Property(t => t.C_userno).HasColumnName("_userno");
            this.Property(t => t.C_username).HasColumnName("_username");
            this.Property(t => t.C_powerid).HasColumnName("_powerid");
            this.Property(t => t.C_password).HasColumnName("_password");
        }
    }
}
