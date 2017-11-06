using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ICMServer.Models.Mapping
{
    public class addrassociateMap : EntityTypeConfiguration<addrassociate>
    {
        public addrassociateMap()
        {
            // Primary Key
            this.HasKey(t => t.C_id);

            // Properties
            this.Property(t => t.C_addrA)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.C_addrB)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.C_des)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("addrassociate", "icmdb");
            this.Property(t => t.C_id).HasColumnName("_id");
            this.Property(t => t.C_addrA).HasColumnName("_addrA");
            this.Property(t => t.C_addrB).HasColumnName("_addrB");
            this.Property(t => t.C_typeA).HasColumnName("_typeA");
            this.Property(t => t.C_typeB).HasColumnName("_typeB");
            this.Property(t => t.C_des).HasColumnName("_des");
        }
    }
}
